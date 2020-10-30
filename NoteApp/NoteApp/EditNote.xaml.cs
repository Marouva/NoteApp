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
    public partial class EditNote : ContentPage
    {
        private Note EditedNote { get; set; }

        public EditNote(Note note)
        {
            EditedNote = note;

            InitializeComponent();

            Name.Text = EditedNote.Name;
            Text.Text = EditedNote.Text;
        }

        private void Submit_Clicked(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(Name.Text) ||
                String.IsNullOrWhiteSpace(Text.Text))
                return;

            EditedNote.Name       = Name.Text;
            EditedNote.Text       = Text.Text;
            EditedNote.ModifyDate = DateTime.Now;

            // Update
            Notes.SaveNote(EditedNote).Wait();

            Navigation.PopAsync();
            App.MainPage.Update();
        }
    }
}