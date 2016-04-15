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
using Android.Support.V7.App;
using Android.Support.V4.App;

namespace AppDaily
{
	class ItemCustomAdapater : ArrayAdapter<string>
	{
		private Context m_context;
		private int m_layoutResourceId;
		private List<string> m_data;
		
		public ItemCustomAdapater(Context context, int layoutResourceId, List<string> data) : base(context, layoutResourceId, data)
		{
			SetNotifyOnChange(true);
			m_context = context;
			m_layoutResourceId = layoutResourceId;
			m_data = data;
		}

		public override View GetView(int position, View convertView, ViewGroup parent)
		{
			View listItem = convertView;

			LayoutInflater inflater = ((Activity)m_context).LayoutInflater;
			listItem = inflater.Inflate(m_layoutResourceId, parent, false);

			TextView tvText = listItem.FindViewById<TextView>(Resource.Id.rowText);
			Button btnDel = listItem.FindViewById<Button>(Resource.Id.btnDeleteRow);
			
			string labelText = m_data[position];
			
			tvText.SetText(labelText, TextView.BufferType.Normal);
			
			btnDel.Click += delegate { removeItem(position); };
			

			return listItem;
		}

		public void removeItem(int pos)
		{
			Remove(m_data[pos]);
			m_data.RemoveAt(pos);
			NotifyDataSetChanged();
		}

		public void addItem(string content)
		{
			Add(content);
			m_data.Add(content);
			NotifyDataSetChanged();
		}
	}
}