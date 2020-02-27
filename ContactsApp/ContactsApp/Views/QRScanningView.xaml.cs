using ContactsApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing.Net.Mobile.Forms;

namespace ContactsApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class QRScanningView : ZXingScannerPage
    {
        public ObservableCollection<Contact> Contacts { get; set; }
        public Contact Contact { get; set; } = new Contact();
        public bool IsScanningContact { get; set; } = true;
        public QRScanningView(ObservableCollection<Contact> contacts)
        {
            InitializeComponent();
            Contacts = contacts;
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

        private void ZXingScannerPage_OnScanResult(ZXing.Result result)
        {
            IsScanningContact = false;
            string[] contactInfo = result.Text.Split('*');
            Contact.FirstName = contactInfo[0];
            Contact.LastName = contactInfo[1];
            Contact.CellNumber = contactInfo[2];
            Contact.Image = contactInfo[0][0].ToString();
            Contact.ImageColor = GetImageColor();

            Device.BeginInvokeOnMainThread(async () =>
            {
                if(Contact.Id == 0)
                {
                    Contact.Id = await App.ContactDatabase.SaveContact(Contact);
                    Contacts.Add(Contact);
                }

                await Navigation.PopToRootAsync();
                await DisplayAlert("Scanned Result", $"Contact succesfully created: {Contact.Name}", "Ok");
            });
        }
    }
}