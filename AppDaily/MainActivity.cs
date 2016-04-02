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
			Button button = FindViewById<Button>(Resource.Id.MyButton);

			button.Click += delegate { button.Text = string.Format("{0} clicks!", count++); };

			// first see if we can find the data files 
			//Context context = this.ApplicationContext;

			string documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
			Directory.SetCurrentDirectory(documentsPath);

			//button.Text = Directory.GetCurrentDirectory(); 
			if (!File.Exists("./CurrentData.dat"))
			{
				button.Text = "Nothing found yet";
				File.Create("./CurrentData.dat");
			}
			else
			{
				button.Text = "Found the data!";
			}
		}
	}
}

