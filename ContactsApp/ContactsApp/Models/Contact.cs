using System.ComponentModel;

namespace ContactsApp.Models
{
    public class Contact : INotifyPropertyChanged
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Company { get; set; }
        public string Name => $"{FirstName} {LastName}";
        public string CellNumber { get; set; }
        public string NumberTag { get; set; }
        public string Email { get; set; }
        public string EmailTag { get; set; }
        public string Image { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
