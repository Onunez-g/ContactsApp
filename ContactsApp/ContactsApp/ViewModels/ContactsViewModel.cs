using ContactsApp.Models;
using ContactsApp.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ContactsApp.ViewModels
{
    public class ContactsViewModel
    {
        public ObservableCollection<Contact> Contacts { get; set; }
        public ICommand DeleteContactCommand { get; set; }
        public ICommand GoToAddViewCommand { get; set; }
        public ICommand DisplayMoreCommand { get; set; }
        public ContactsViewModel()
        {
            GoToAddViewCommand = new Command(async () =>
            {
                await App.Current.MainPage.Navigation.PushAsync(new NewContactView(Contacts));
            });
            DeleteContactCommand = new Command<Contact>((contact) => 
            {
                Contacts.Remove(contact);
            });
            DisplayMoreCommand = new Command<Contact>(async (contact) =>
            {
                var option = await App.Current.MainPage.DisplayActionSheet("More", "Cancel", "Close", $"Call {contact.CellNumber}", "Edit");
                switch (option)
                {
                    case "Edit":
                        await App.Current.MainPage.Navigation.PushAsync(new NewContactView(Contacts, contact));
                        break;
                    default:
                        PhoneDialer.Open(contact.CellNumber);
                        break;
                }
            });
            Contacts = new ObservableCollection<Contact>
            {
                new Contact() { Name = "Orlando Núñez", CellNumber = "8094889993", Image = "O" },
                new Contact() { Name = "Miranda Núñez", CellNumber = "8093995196", Image = "M" }
            };
        }
    }
}
