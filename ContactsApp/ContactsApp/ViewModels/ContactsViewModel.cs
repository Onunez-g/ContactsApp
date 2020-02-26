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
    public class ContactsViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Contact> Contacts { get; set; } = new ObservableCollection<Contact>();
        public bool IsRefreshingList { get; set; }
        public ICommand DeleteContactCommand { get; set; }
        public ICommand GoToAddViewCommand { get; set; }
        public ICommand DisplayMoreCommand { get; set; }
        public ICommand RefreshListCommand { get; set; }
        public ContactsViewModel()
        {
            GetContacts();
            GoToAddViewCommand = new Command(async () =>
            {
                await App.Current.MainPage.Navigation.PushAsync(new NewContactView(Contacts));
            });
            DeleteContactCommand = new Command<Contact>(async (contact) => 
            {
                await App.ContactDatabase.DeleteContact(contact.Id);
                Contacts.Remove(contact);
            });
            RefreshListCommand = new Command(async () =>
            {
                Contacts = new ObservableCollection<Contact>(await App.ContactDatabase.GetContactsAsync());
                IsRefreshingList = false;
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
        }
        #pragma warning disable 67
        public event PropertyChangedEventHandler PropertyChanged;
        #pragma warning restore 67
        public async void GetContacts()
        {
            var contacts = await App.ContactDatabase.GetContactsAsync();
            Contacts = new ObservableCollection<Contact>(contacts);
            IsRefreshingList = false;
        }
    }
}
