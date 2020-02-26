using ContactsApp.Models;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace ContactsApp.ViewModels
{
    class NewContactViewModel : INotifyPropertyChanged
    {
        public Contact Contact { get; set; } = new Contact() {Image = "AddPhoto", NumberTag = "Celular", EmailTag = "Particular"};
        public bool IsEdit { get; set; }
        public string Title { get; set; } = "Create contact";
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
                FrameImage = new FrameImage()
                {
                    IsFrameVisible = false,
                    IsImageVisible = true
                };
                IsEdit = true;
                Title = "Edit contact";
            }
            AddContactCommand = new Command<bool>(async (isEdit) =>
            {
                if(string.IsNullOrEmpty(Contact.FirstName) || string.IsNullOrEmpty(Contact.LastName) || string.IsNullOrEmpty(Contact.CellNumber)) 
                {
                    await App.Current.MainPage.DisplayAlert("Error", "Names and Number fields are required", "Ok");
                    return;
                }
                Contact.ImageColor = Contact.HasImage ? null : GetImageColor();
                Contact.Image = Contact.HasImage ? Contact.Image : $"{Contact.Name[0]}";
                Contact.Id = await App.ContactDatabase.SaveContact(Contact);
                if (!isEdit)
                    contacts.Add(Contact);
                await App.Current.MainPage.Navigation.PopAsync();
            });
            AddContactPhotoCommand = new Command(async () =>
            {
                string option = await App.Current.MainPage.DisplayActionSheet("Change photo", "Cancel", "", "Take photo", "Choose photo");
                switch (option)
                {
                    case "Take photo":
                        TakePhoto();
                        break;
                    case "Choose photo":
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
            Contact.HasImage = true;
            FrameImage = new FrameImage() { IsFrameVisible = false, IsImageVisible = true };
        }
        private string GetImageColor()
        {
            var colors = new List<string>
            {
                "#EB6858",
                "#ADCEE0",
                "#AD5CF3",
                "#F06AB0",
                "#F68F41",
                "#63B47F",
                "#FCC731"
            };
            Random rnd = new Random();
            return colors.OrderBy(x => rnd.Next()).First();
        }
        private async void PickPhoto()
        {
            await CrossMedia.Current.Initialize();
            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await App.Current.MainPage.DisplayAlert("Photos Not Supported", "Permission not granted to photos.", "OK");
                return;
            }
            var file = await CrossMedia.Current.PickPhotoAsync();

            if (file == null)
                return;
            Contact.Image = file.Path;
            Contact.HasImage = true;
            file.Dispose();
            FrameImage = new FrameImage() { IsFrameVisible = false, IsImageVisible = true };
        }
        #pragma warning disable 67
        public event PropertyChangedEventHandler PropertyChanged;
        #pragma warning restore 67
    }
}
