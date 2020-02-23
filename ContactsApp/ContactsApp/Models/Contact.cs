using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

namespace ContactsApp.Models
{
    public class Contact : INotifyPropertyChanged
    {
        public string Name { get; set; }
        public string CellNumber { get; set; }
        public ImageSource Image { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
