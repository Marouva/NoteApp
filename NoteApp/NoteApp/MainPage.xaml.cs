using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace NoteApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            // Info
            Title = "Poznámky";

            InitializeComponent();
        }

        async private void AddButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddNote
            {
                BindingContext = new Note()
            });
        }
    }
}
