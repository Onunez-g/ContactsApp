using ContactsApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace ContactsApp.ViewModels
{
    class NewContactViewModel : INotifyPropertyChanged
    {
        public Contact Contact { get; set; } = new Contact();
        public bool IsEdit { get; set; }
        public string Title { get; set; } = "New contact";
        public string ButtonText { get; set; } = "Add";
        public ICommand AddContactCommand { get; set; }
        public NewContactViewModel(ObservableCollection<Contact> contacts, Contact contact = null)
        {
            if (contact != null)
            {
                Contact = contact;
                IsEdit = true;
                Title = "Edit contact";
                ButtonText = "Edit";
            }
            AddContactCommand = new Command<bool>(async (isEdit) =>
            {
                if(string.IsNullOrEmpty(Contact.Name) || string.IsNullOrEmpty(Contact.CellNumber)) 
                {
                    await App.Current.MainPage.DisplayAlert("Error", "All fields are required", "Ok");
                    return;
                }
                Contact.Image = Contact.Image ?? $"{Contact.Name[0]}";
                if(!isEdit)
                    contacts.Add(Contact);
                await App.Current.MainPage.Navigation.PopAsync();
            });
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
