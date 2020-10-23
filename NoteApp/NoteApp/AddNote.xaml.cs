using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NoteApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddNote : ContentPage
    {
        public AddNote()
        {
            // Info
            Title = "Přidat poznámku";

            InitializeComponent();
        }

        private void Submit_Clicked(object sender, EventArgs e)
        {

        }
    }
}