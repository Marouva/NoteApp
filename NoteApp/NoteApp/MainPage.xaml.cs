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
                    Orientation = StackOrientation.Vertical,
                    Spacing     = 8
                };

                Label noteName = new Label
                {
                    Text          = note.Name,
                    FontSize      = 24,
                    TextColor     = Color.Black,
                    LineBreakMode = LineBreakMode.WordWrap
                };

                Label noteDate = new Label
                {
                    Text          = note.CreateDate.ToString("dd. MM. yyyy, hh:mm") +
                                    (note.ModifyDate == note.CreateDate ? "" : ("\n(upraveno " + note.ModifyDate.ToString("dd. MM. yyyy, hh:mm") + ")")),
                    FontSize      = 16,
                    LineBreakMode = LineBreakMode.WordWrap
                };

                Label noteText = new Label
                {
                    Text          = note.Text,
                    FontSize      = 18,
                    TextColor     = Color.Black,
                    LineBreakMode = LineBreakMode.WordWrap
                };

                Button noteButton = new Button
                {
                    Text = "⋮",
                    FontSize = 24
                };

                noteButton.Clicked += (object sender, EventArgs e) => { EditNoteMenuAsync(note); };

                noteLayout.Children.Add(noteName);
                noteLayout.Children.Add(noteDate);
                noteLayout.Children.Add(noteText);
                noteLayout.Children.Add(noteButton);

                noteFrame.Content = noteLayout;

                NotesContainer.Children.Add(noteFrame);
            }
        }

        async private void AddButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddNote());
        }

        private async Task EditNoteMenuAsync(Note note)
        {
            string action = await DisplayActionSheet("Upravit poznámku", "Zrušit", "Smazat", "Upravit");
            
            switch (action)
            {
                case "Smazat":
                    bool answer = await DisplayAlert("Smazat poznámku?", "Opravdu chcete smazat poznámku " + note.Name + "?", "Ano", "Ne");

                    if (answer)
                    {
                        Notes.DeleteNote(note).Wait();

                        // Update
                        App.MainPage.Update();
                    }

                    break;

                case "Upravit":
                    Navigation.PushAsync(new EditNote(note));
                    break;

                default:
                    break;
            }
        }
    }
}
