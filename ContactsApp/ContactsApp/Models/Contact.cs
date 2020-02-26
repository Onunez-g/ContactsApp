using SQLite;
using System.ComponentModel;

namespace ContactsApp.Models
{
    public class Contact : INotifyPropertyChanged
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Company { get; set; }
        public string Name => $"{FirstName} {LastName}";
        public string CellNumber { get; set; }
        public string NumberTag { get; set; }
        public string Email { get; set; }
        public string EmailTag { get; set; }
        public string Image { get; set; }
        public string ImageColor { get; set; }
        public bool HasImage { get; set; }
        #pragma warning disable 67
        public event PropertyChangedEventHandler PropertyChanged;
        #pragma warning restore 67
    }
}
