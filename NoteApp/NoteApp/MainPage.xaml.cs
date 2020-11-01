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
                // Element container
                Frame noteFrame = new Frame
                {
                    Padding = 16
                };

                StackLayout noteLayout = new StackLayout
                {
                    Orientation = StackOrientation.Vertical,
                    Spacing     = 8
                };

                // Name + Edit button
                Grid noteNameLayout = new Grid
                {
                    ColumnDefinitions =
                    {
                        new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                        new ColumnDefinition { Width = new GridLength(32) }
                    }
                };

                Label noteNameLabel = new Label
                {
                    Text            = note.Name,
                    FontSize        = 24,
                    FontAttributes  = FontAttributes.Bold,
                    TextColor       = Color.Black,
                    LineBreakMode   = LineBreakMode.WordWrap,
                    VerticalOptions = LayoutOptions.Center
                };

                Button noteButton = new Button
                {
                    Text            = "⋮",
                    FontSize        = 24,
                    FontAttributes  = FontAttributes.Bold,
                    BackgroundColor = Color.Transparent,
                    VerticalOptions = LayoutOptions.Center
                };

                noteButton.Clicked += (object sender, EventArgs e) => { EditNoteMenuAsync(note); };

                noteNameLayout.Children.Add(noteNameLabel, 0, 0);
                noteNameLayout.Children.Add(noteButton,    1, 0);

                noteLayout.Children.Add(noteNameLayout);

                // Date
                noteLayout.Children.Add(new Label
                {
                    Text           = note.CreateDate.ToString("dd.MM.yyyy, hh:mm") +
                                     (note.ModifyDate == note.CreateDate ? "" : (" (úprava " + note.ModifyDate.ToString("dd.MM.yyyy, hh:mm") + ")")),
                    FontSize       = 16,
                    FontAttributes = FontAttributes.Italic,
                    TextColor      = Color.Gray,
                    LineBreakMode  = LineBreakMode.WordWrap
                });

                // Separator
                noteLayout.Children.Add(new BoxView
                {
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    HeightRequest     = 1,
                    Color             = Color.Gray
                });

                // Text
                noteLayout.Children.Add(new Label
                {
                    Text          = note.Text,
                    FontSize      = 18,
                    TextColor     = Color.Black,
                    LineBreakMode = LineBreakMode.WordWrap
                });

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
            string action = await DisplayActionSheet("Vyberte akci", "Zrušit", "Smazat", "Upravit");
            
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
