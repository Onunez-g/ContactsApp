using ContactsApp.Models;
using ContactsApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ContactsApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewContactView : ContentPage
    {
        public NewContactView(ObservableCollection<Contact> contacts, Contact contact = null)
        {
            InitializeComponent();
            BindingContext = new NewContactViewModel(contacts, contact);
        }
    }
}