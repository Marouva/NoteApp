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

            Update();
        }

        public void Update()
        {
            // Clear container
            NotesContainer.Children.Clear();

            // Get notes
            Task<List<Note>> notesTask = Notes.GetNotes();
            notesTask.Wait();
            List<Note> notes = notesTask.Result;

            // Fill container
            foreach (Note note in notes)
            {
                Frame noteFrame = new Frame
                {
                    Padding = 16
                };

                StackLayout noteLayout = new StackLayout
                {
                    Orientation = StackOrientation.Horizontal,
                    Spacing     = 16,
                };

                Label noteName = new Label
                {
                    Text              = note.Name,
                    FontSize          = 24,
                    TextColor         = Color.Black,
                    HorizontalOptions = LayoutOptions.FillAndExpand
                };

                Label noteDate = new Label
                {
                    Text              = note.CreateDate.ToString() + (note.ModifyDate == null ? "" : (" (upraveno " + note.ModifyDate.ToString() + ")")),
                    FontSize          = 16,
                    HorizontalOptions = LayoutOptions.FillAndExpand
                };

                Label noteText = new Label
                {
                    Text              = note.Text,
                    FontSize          = 18,
                    TextColor         = Color.Black,
                    HorizontalOptions = LayoutOptions.FillAndExpand
                };

                noteLayout.Children.Add(noteName);
                noteLayout.Children.Add(noteDate);
                noteLayout.Children.Add(noteText);

                noteFrame.Content = noteLayout;

                NotesContainer.Children.Add(noteFrame);
            }
        }

        async private void AddButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddNote());
        }
    }
}
