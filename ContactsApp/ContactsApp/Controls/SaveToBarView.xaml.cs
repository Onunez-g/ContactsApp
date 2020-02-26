using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ContactsApp.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SaveToBarView : ContentView
    {
        public SaveToBarView()
        {
            InitializeComponent();
        }
    }
}