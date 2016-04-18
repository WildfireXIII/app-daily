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

namespace AppDaily
{
	[Service]
	class HourlyService : Service
	{
		HourlyAlarm m_alarm = new HourlyAlarm();

		public override void OnCreate() { base.OnCreate(); }

		public override StartCommandResult OnStartCommand(Intent intent, StartCommandFlags flags, int startId)
		{
			m_alarm.SetAlarm(this);
			return StartCommandResult.Sticky;
		}

		public override IBinder OnBind(Intent intent)
		{
			return null;
		}
	}
}