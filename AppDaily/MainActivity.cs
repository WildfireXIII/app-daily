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

namespace AppDaily
{
	[Activity(Label = "AppDaily", MainLauncher = true, Icon = "@drawable/icon")]
	//public class MainActivity : Activity
	public class MainActivity : AppCompatActivity
	{
		private string[] m_navTitles;
		private DrawerLayout m_drawerLayout;
		private ListView m_drawerList;

	
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
			//m_navTitles = Resources.GetStringArray(Resource.Array.navigation_drawer_items_array);
			m_drawerLayout = FindViewById<DrawerLayout>(Resource.Id.appDrawerLayout);
			m_drawerList = FindViewById<ListView>(Resource.Id.appDrawerList);
			
			//m_drawerList.Adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, m_navTitles);
			m_drawerList.Adapter = new DrawerItemCustomAdapter(this, Resource.Layout.ListViewItemRow, m_navTitles);
			m_drawerList.ItemClick += (object sender, Android.Widget.AdapterView.ItemClickEventArgs e) =>
			{
				int choice = e.Position;
				Intent intent = new Intent(this, (new EditListActivity()).Class);
				switch (choice)
				{
					case 0:
						//toolbar.Title = "Edit Projects";
						//frag = new CreateFragment();
						//SetContentView(Resource.Layout.FragmentProjects);
						intent.PutExtra("ListName", "Projects");
						StartActivity(intent);
						break;
					case 1:
						//toolbar.Title = "Edit Studies";
						intent.PutExtra("ListName", "Studies");
						StartActivity(intent);
						break;
					case 2:
						//toolbar.Title = "Edit Extra Studies";
						intent.PutExtra("ListName", "Extra Studies");
						StartActivity(intent);
						break;
					case 3:
						//toolbar.Title = "Edit Activities";
						intent.PutExtra("ListName", "Activities");
						StartActivity(intent);
						break;
					case 4:
						//toolbar.Title = "Edit Quotes";
						intent.PutExtra("ListName", "Quotes");
						StartActivity(intent);
						break;
					case 5:
						//toolbar.Title = "Edit Facts";
						intent.PutExtra("ListName", "Facts");
						StartActivity(intent);
						break;
					default:
						//toolbar.Title = "AppDaily";
						break;
				}

				/*if (frag != null)
				{
					Android.App.FragmentManager fragmentManager = this.FragmentManager; 

					//fragmentManager.BeginTransaction().Replace(Resource.Id.appFrameContent, frag).Commit();
				}*/
				m_drawerLayout.CloseDrawer(m_drawerList);
			};
			//m_drawerList.OnItemClickListener = new DrawerLayout



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

