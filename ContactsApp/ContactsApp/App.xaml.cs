using ContactsApp.Data;
using ContactsApp.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ContactsApp
{
    public partial class App : Application
    {
        static ContactDatabaseController contactDatabase;
        public static ContactDatabaseController ContactDatabase
        {
            get
            {
                if (contactDatabase == null)
                    contactDatabase = new ContactDatabaseController();
                return contactDatabase;
            }
        }
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new ContactsView());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
