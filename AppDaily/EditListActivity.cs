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

namespace AppDaily
{
	[Activity(Label = "EditListActivity")]
	public class EditListActivity : AppCompatActivity
	{
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

			toolbar.Title = "Edit " + listName;
			SetSupportActionBar(toolbar);

			List<string> m_items = new List<string>();
			m_items.Add("thing1");
			m_items.Add("thing2");
			m_items.Add("thing3 and more stuff");
			
			ListView lv = FindViewById<ListView>(Resource.Id.elItemList);
			ItemCustomAdapater adapter = new ItemCustomAdapater(this, Resource.Layout.ItemRow, m_items);
			lv.Adapter = adapter;

			// handle the add button
			Button addButton = FindViewById<Button>(Resource.Id.btnAddItem);
			addButton.Click += delegate
			{
				// get edittext content
				EditText txt = FindViewById<EditText>(Resource.Id.txtNewItem);

				string content = txt.Text;
				/*adapter.Add(content);
				adapter.NotifyDataSetChanged();*/
				adapter.addItem(content);
			};
		}

		public override bool OnOptionsItemSelected(IMenuItem item)
		{
			switch (item.ItemId)
			{
				case Android.Resource.Id.Home:
					Console.WriteLine("Specifically the home/up button");

					// todo: save current list state here (method in adapter?)
					
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