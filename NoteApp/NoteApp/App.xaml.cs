using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NoteApp
{
    public partial class App : Application
    {
        public static readonly string DBPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Notes.db3");

        public static new MainPage MainPage { get; set; }

        public App()
        {
            InitializeComponent();
            
            // DB
            Notes.Connect(DBPath);

            // Page
            //MainPage = new MainPage();
            MainPage = new MainPage();
            base.MainPage = new NavigationPage(MainPage);
        }

        protected override void OnStart()
        {            
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
