using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Support.V7;
using Android.Support.V4.App;
using Android.Support.V7.App;
using Android.Support.V4.Widget;
using Android.Support.V7.AppCompat;

using System.IO;

namespace AppDaily
{
	[Activity(Label = "EditListActivity")]
	public class EditListActivity : AppCompatActivity
	{
		private string m_listType = "";
		private string m_listFileURL = "";
		private ItemCustomAdapater m_adapter;
		
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			SetContentView(Resource.Layout.EditListLayout);

			Android.Support.V7.Widget.Toolbar toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.elToolbar);
			SetSupportActionBar(toolbar);
			this.SupportActionBar.SetDisplayHomeAsUpEnabled(true);

			// Create your application here
			Intent intent = this.Intent;
			string listName = intent.GetStringExtra("ListName");
			m_listType = listName;

			toolbar.Title = "Edit " + listName;
			SetSupportActionBar(toolbar);

			List<string> items = loadExistingList();
			
			ListView lv = FindViewById<ListView>(Resource.Id.elItemList);
			m_adapter = new ItemCustomAdapater(this, Resource.Layout.ItemRow, items);
			lv.Adapter = m_adapter;

			// handle the add button
			Button addButton = FindViewById<Button>(Resource.Id.btnAddItem);
			addButton.Click += delegate
			{
				// get edittext content
				EditText txt = FindViewById<EditText>(Resource.Id.txtNewItem);

				string content = txt.Text;
				m_adapter.addItem(content);
			};
		}

		private List<string> loadExistingList()
		{
			List<string> itemsList = new List<string>();

			string documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
			Directory.SetCurrentDirectory(documentsPath);
			
			if (m_listType == "Projects") { m_listFileURL = "Projects.dat"; }
			else if (m_listType == "Studies") { m_listFileURL = "Studies.dat"; }
			else if (m_listType == "Extra Studies") { m_listFileURL = "ExtraStudies.dat"; }
			else if (m_listType == "Activities") { m_listFileURL = "Activities.dat"; }
			else if (m_listType == "Quotes") { m_listFileURL = "Quotes.dat"; }
			else if (m_listType == "Facts") { m_listFileURL = "Facts.dat"; }

			// make sure the file exists
			if (!File.Exists(m_listFileURL)) { File.Create(m_listFileURL); }

			itemsList = File.ReadAllLines(m_listFileURL).ToList();
			return itemsList;
		} 

		public override bool OnOptionsItemSelected(IMenuItem item)
		{
			switch (item.ItemId)
			{
				case Android.Resource.Id.Home:
					Console.WriteLine("Specifically the home/up button");

					// todo: save current list state here (method in adapter?)

					File.WriteAllLines(m_listFileURL, m_adapter.getList());
					this.Finish();	
					return true;
			}
			/*if (item.ItemId == Resource.Id.home)
			{
				NavUtils.NavigateUpFromSameTask(this);
				return true;
			}*/
			return base.OnOptionsItemSelected(item);
		}
	}
}