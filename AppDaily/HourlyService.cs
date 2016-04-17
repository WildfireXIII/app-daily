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

		public override void OnCreate() { Console.WriteLine("Hourly service created"); base.OnCreate(); }

		public override StartCommandResult OnStartCommand(Intent intent, StartCommandFlags flags, int startId)
		{
			//return base.OnStartCommand(intent, flags, startId);
			Console.WriteLine("Starting hourly service");
			m_alarm.SetAlarm(this);
			//this.con
			//return StartCommandResult.Sticky;
			return StartCommandResult.Sticky;
		}

		/*public override void OnStart(Intent intent, int startId)
		{
			Console.WriteLine("Starting hourly service");
			m_alarm.SetAlarm(this);
		}*/ 

		public override IBinder OnBind(Intent intent)
		{
			return null;
		}
	}
}