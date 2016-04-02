using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

using System.IO;

namespace AppDaily
{
	[Activity(Label = "AppDaily", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{
		int count = 1;

		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);

			// Set our view from the "main" layout resource
			SetContentView(Resource.Layout.Main);

			// Get our button from the layout resource,
			// and attach an event to it
			//Button button = FindViewById<Button>(Resource.Id.MyButton);

			//button.Click += delegate { button.Text = string.Format("{0} clicks!", count++); };

			TextView tv = FindViewById<TextView>(Resource.Id.txtProject);
			tv.Text = "Project: AppDaily";

			// first set up any data files that are missing
			string documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
			Directory.SetCurrentDirectory(documentsPath);

			// TODO: this really belongs wherever the files are actually called.
			/*if (!File.Exists("CurrentData.dat")) { File.Create("CurrentData.dat"); }
			if (!File.Exists("Projects.dat")) { File.Create("Projects.dat"); }
			if (!File.Exists("Studies.dat")) { File.Create("Studies.dat"); }
			if (!File.Exists("ExtraStudies.dat")) { File.Create("ExtraStudies.dat"); }
			if (!File.Exists("Activities.dat")) { File.Create("Activities.dat"); }
			if (!File.Exists("Quotes.dat")) { File.Create("Quotes.dat"); }
			if (!File.Exists("Facts.dat")) { File.Create("Facts.dat"); }*/
		}
	}
}

