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
        public Contact Contact { get; set; } = new Contact() { NumberTag = "Celular", EmailTag = "Particular"};
        public bool IsEdit { get; set; }
        public string Title { get; set; } = "New contact";
        public ICommand AddContactCommand { get; set; }
        public List<string> NumberTags { get; set; }
        public List<string> EmailTags { get; set; }
        public NewContactViewModel(ObservableCollection<Contact> contacts, Contact contact = null)
        {
            if (contact != null)
            {
                Contact = contact;
                IsEdit = true;
                Title = "Edit contact";
            }
            AddContactCommand = new Command<bool>(async (isEdit) =>
            {
                if(string.IsNullOrEmpty(Contact.FirstName) || string.IsNullOrEmpty(Contact.LastName) || string.IsNullOrEmpty(Contact.CellNumber)) 
                {
                    await App.Current.MainPage.DisplayAlert("Error", "All fields are required", "Ok");
                    return;
                }
                Contact.Image = Contact.Image ?? $"{Contact.Name[0]}";
                if(!isEdit)
                    contacts.Add(Contact);
                await App.Current.MainPage.Navigation.PopAsync();
            });
            NumberTags = new List<string>()
            {
                "Celular",
                "Laboral",
                "Particular",
                "Principal"
            };
            EmailTags = new List<string>()
            {
                "Particular",
                "Laboral",
                "Otro"
            };
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
