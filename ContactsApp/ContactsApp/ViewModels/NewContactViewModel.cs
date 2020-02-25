using ContactsApp.Models;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace ContactsApp.ViewModels
{
    class NewContactViewModel : INotifyPropertyChanged
    {
        public Contact Contact { get; set; } = new Contact() {Image = "AddPhoto", NumberTag = "Celular", EmailTag = "Particular"};
        public bool IsEdit { get; set; }
        public string Title { get; set; } = "New contact";
        public FrameImage FrameImage { get; set; } = new FrameImage();
        public ICommand AddContactCommand { get; set; }
        public ICommand AddContactPhotoCommand { get; set; }
        public List<string> NumberTags { get; set; }
        public List<string> EmailTags { get; set; }
        public NewContactViewModel(ObservableCollection<Contact> contacts, Contact contact = null)
        {
            if (contact != null)
            {
                Contact = contact;
                FrameImage = FrameImage = new FrameImage()
                {
                    BackgroundColor = "Transparent",
                    CornerRadius = "0",
                    HeightRequest = "100",
                    WidthRequest = "200"
                };
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
            AddContactPhotoCommand = new Command(async () =>
            {
                string option = await App.Current.MainPage.DisplayActionSheet("Change photo", "Cancel", "", "Take photo", "Pick photo");
                switch (option)
                {
                    case "Take photo":
                        TakePhoto();
                        break;
                    case "Pick photo":
                        PickPhoto();
                        break;
                }
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
        private async void TakePhoto()
        {
            await CrossMedia.Current.Initialize();
            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await App.Current.MainPage.DisplayAlert("No Camera", "No camera available.", "OK");
                return;
            }
            var file = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions());
            if (file == null)
                return;
            Contact.Image = file.Path;
            FrameImage = new FrameImage()
            {
                BackgroundColor = "Transparent",
                CornerRadius = "0",
                HeightRequest = "100",
                WidthRequest = "200"
            };
        }
        private async void PickPhoto()
        {
            await CrossMedia.Current.Initialize();
            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await App.Current.MainPage.DisplayAlert("Photos Not Supported", "Permission not granted to photos.", "OK");
                return;
            }
            var file = await CrossMedia.Current.PickPhotoAsync().ConfigureAwait(true);

            if (file == null)
                return;
            Contact.Image = file.Path;
            file.Dispose();
            FrameImage = new FrameImage()
            {
                BackgroundColor = "Transparent",
                CornerRadius = "0",
                HeightRequest = "100",
                WidthRequest = "200"
            };
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
