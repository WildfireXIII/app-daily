using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Support.V4.App;
using Android.Support.V7;
using Android.Support.V7.App;
using Android.Support.V4.Widget;

using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace AppDaily
{
	[Activity(Label = "AppDaily", MainLauncher = true, Icon = "@drawable/icon")]
	//public class MainActivity : Activity
	public class MainActivity : AppCompatActivity
	{
		private string[] m_navTitles;
		private DrawerLayout m_drawerLayout;
		private ListView m_drawerList;

		public void randomizeAllCurrent()
		{
			List<string> projects = File.ReadAllLines("Projects.dat").ToList();
			List<string> studies = File.ReadAllLines("Studies.dat").ToList();
			List<string> extraStudies = File.ReadAllLines("ExtraStudies.dat").ToList();
			List<string> activities = File.ReadAllLines("Activities.dat").ToList();
			List<string> quotes = File.ReadAllLines("Quotes.dat").ToList();
			List<string> facts = File.ReadAllLines("Facts.dat").ToList();

			List<string> current = new List<string>();
			Random r = new Random();

			// init list to nulls
			for (int i = 0; i < 6; i++) { current.Add(null); }

			if (projects.Count > 0) { current[0] = projects[r.Next(0, projects.Count)]; }
			if (studies.Count > 0) { current[1] = studies[r.Next(0, studies.Count)]; }
			if (extraStudies.Count > 0) { current[2] = extraStudies[r.Next(0, extraStudies.Count)]; }
			if (activities.Count > 0) { current[3] = activities[r.Next(0, activities.Count)]; }
			if (quotes.Count > 0) { current[4] = quotes[r.Next(0, quotes.Count)]; }
			if (facts.Count > 0) { current[5] = facts[r.Next(0, facts.Count)]; }

			File.WriteAllLines("Current.dat", current);
		}

		public List<string> getCurrent()
		{
			List<string> current = File.ReadAllLines("Current.dat").ToList();
			//if (current.Count < 6) { while (current.Count < 6) { current.Add("NO ITEMS"); } }
			for (int i = 0; i < current.Count; i++)
			{
				if (current[i] == null || current[i] == "") { current[i] = "NO ITEMS"; }
			}

			return current;
		}

		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);

			// Set our view from the "main" layout resource
			SetContentView(Resource.Layout.Main);

			// set the main toolbar of the app
			Android.Support.V7.Widget.Toolbar toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.appToolbar);
			SetSupportActionBar(toolbar);

			// initialize the drawer list variables
			m_navTitles = new string[] { "Projects", "Studies", "Extra Studies", "Activities", "Quotes", "Facts" };
			m_drawerLayout = FindViewById<DrawerLayout>(Resource.Id.appDrawerLayout);
			m_drawerList = FindViewById<ListView>(Resource.Id.appDrawerList);

			m_drawerList.Adapter = new DrawerItemCustomAdapter(this, Resource.Layout.ListViewItemRow, m_navTitles);
			m_drawerList.ItemClick += (object sender, Android.Widget.AdapterView.ItemClickEventArgs e) =>
			{
				int choice = e.Position;
				Intent intent = new Intent(this, (new EditListActivity()).Class);
				switch (choice)
				{
					case 0:
						intent.PutExtra("ListName", "Projects");
						StartActivity(intent);
						break;
					case 1:
						intent.PutExtra("ListName", "Studies");
						StartActivity(intent);
						break;
					case 2:
						intent.PutExtra("ListName", "Extra Studies");
						StartActivity(intent);
						break;
					case 3:
						intent.PutExtra("ListName", "Activities");
						StartActivity(intent);
						break;
					case 4:
						intent.PutExtra("ListName", "Quotes");
						StartActivity(intent);
						break;
					case 5:
						intent.PutExtra("ListName", "Facts");
						StartActivity(intent);
						break;
					default:
						break;
				}

				m_drawerLayout.CloseDrawer(m_drawerList);
			};
			TextView tv = FindViewById<TextView>(Resource.Id.txtProject);
			tv.Text = "Project: AppDaily";

			// first set up any data files that are missing
			string documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
			Directory.SetCurrentDirectory(documentsPath);
			verify();

			// set up current data (at some point check for when this has been done (run in background?)
			randomizeAllCurrent();
			List<string> current = getCurrent();
			TextView tvProj = FindViewById<TextView>(Resource.Id.txtProject);
			tvProj.Text = "Project: " + current[0];
			TextView tvStudies = FindViewById<TextView>(Resource.Id.txtStudy);
			tvStudies.Text = "Study: " + current[1];
			TextView tvExtraStudies = FindViewById<TextView>(Resource.Id.txtExtraStudy);
			tvExtraStudies.Text = "Extra Study: " + current[2];
			TextView tvActivities = FindViewById<TextView>(Resource.Id.txtActivity);
			tvActivities.Text = "Activity: " + current[3];
			TextView tvQuotes = FindViewById<TextView>(Resource.Id.txtQuote);
			tvQuotes.Text = "Quote: " + current[4];
			TextView tvFacts = FindViewById<TextView>(Resource.Id.txtFact);
			tvFacts.Text = "Fact: " + current[5];
		}

		private void verify()
		{
			if (!File.Exists("CurrentData.dat")) { File.Create("CurrentData.dat"); }
			if (!File.Exists("Projects.dat")) { File.Create("Projects.dat"); }
			if (!File.Exists("Studies.dat")) { File.Create("Studies.dat"); }
			if (!File.Exists("ExtraStudies.dat")) { File.Create("ExtraStudies.dat"); }
			if (!File.Exists("Activities.dat")) { File.Create("Activities.dat"); }
			if (!File.Exists("Quotes.dat")) { File.Create("Quotes.dat"); }
			if (!File.Exists("Facts.dat")) { File.Create("Facts.dat"); }
		}
	}
}

