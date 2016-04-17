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
	class RandomizationServiceHourly : IntentService
	{
		//Intent m_thisIntent = new Intent(this, typeof(RandomizationServiceHourly));
		
		public RandomizationServiceHourly() : base("RandomizationServiceHourly")
		{
			//m_thisIntent = new Intent(this, typeof(RandomizationServiceHourly));
		}

		protected override void OnHandleIntent(Intent intent)
		{
			Console.WriteLine("The service is running!");
			doThing();
			//SendOrderedBroadcast(new Intent("hello world!"), null);
			
			
			/*if (!isAlarmSet()) 
			{
				Console.WriteLine("Alarm wasn't set!");
				scheduleAlarmThing(); 
			}*/
		}

		private void doThing()
		{
			Notification.Builder notifBuilder = new Notification.Builder(this);
			notifBuilder.SetContentTitle("Service msg");
			notifBuilder.SetContentText("This is a service msg");
			notifBuilder.SetSmallIcon(Resource.Drawable.Icon);
			notifBuilder.SetVibrate(new long[] { 500, 50, 500, 100 });
			notifBuilder.SetTicker("HEY YOU! YOU HAVE A NOTIFICATION!");
			notifBuilder.SetVisibility(NotificationVisibility.Public);
			notifBuilder.SetPriority((int)NotificationPriority.Default);
			notifBuilder.SetContentIntent(PendingIntent.GetActivity(this, 0, new Intent(this, typeof(MainActivity)), 0));

			Notification notif = notifBuilder.Build();
			NotificationManager notifManager = (NotificationManager)GetSystemService(Context.NotificationService);
			notifManager.Notify(0, notif);
		}

		public override void OnCreate()
		{
			Console.WriteLine("Creating command!");
			scheduleAlarmThing();
			base.OnCreate();
		}

		/*public override StartCommandResult OnStartCommand(Intent intent, [GeneratedEnum] StartCommandFlags flags, int startId)
		{
			Console.WriteLine("Starting service command");
			scheduleAlarmThing();
			m_thisIntent = new Intent(this, typeof(RandomizationServiceHourly));
			return base.OnStartCommand(intent, flags, startId);
		}*/


		private void scheduleAlarmThing()
		{
			//if not already set


			//Intent intent = new Intent("")
			if (!isAlarmSet())
			{
				Console.WriteLine("Alarm was NOT set");
				AlarmManager alarm = (AlarmManager)GetSystemService(Context.AlarmService);
				PendingIntent pendingServiceIntent = PendingIntent.GetService(this, 0, new Intent(this, typeof(RandomizationServiceHourly)), PendingIntentFlags.UpdateCurrent);
				//alarm.SetInexactRepeating(AlarmType.RtcWakeup, 0, 20000, pendingServiceIntent);
				alarm.SetRepeating(AlarmType.RtcWakeup, 0, 20000, pendingServiceIntent);
			}



			//PendingIntent pendingServiceIntent = PendingIntent.GetService(this, 0, m_thisIntent, PendingIntentFlags.CancelCurrent);
			//alarm.SetRepeating(AlarmType.Rtc, 0, 1000, pendingServiceIntent);

			//alarm.SetRepeating(AlarmType.RtcWakeup, 0, 1000, pendingServiceIntent);
		}

		public bool isAlarmSet()
		{
			Console.WriteLine("Checking if alarm already set");
			return PendingIntent.GetBroadcast(this, 0, new Intent(this, typeof(RandomizationServiceHourly)), PendingIntentFlags.NoCreate) != null;
		}
	}
}