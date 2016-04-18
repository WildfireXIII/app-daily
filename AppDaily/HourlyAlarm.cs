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

using System.IO;

namespace AppDaily
{
	// thanks to http://stackoverflow.com/questions/4459058/alarm-manager-example
	[BroadcastReceiver]
	class HourlyAlarm : BroadcastReceiver
	{
		public override void OnReceive(Context context, Intent intent)
		{
			PowerManager pm = (PowerManager)context.GetSystemService(Context.PowerService);
			PowerManager.WakeLock wl = pm.NewWakeLock(WakeLockFlags.Partial, "HOURLY_ALARM");

			wl.Acquire();

			onWakeInitializationProtocol();
			

			/*
			Notification.Builder notifBuilder = new Notification.Builder(context);
			notifBuilder.SetContentTitle("Service msg");
			notifBuilder.SetContentText("Here's your hourly reminder! Oh yeah!");
			notifBuilder.SetSmallIcon(Resource.Drawable.Icon);
			notifBuilder.SetVibrate(new long[] { 500, 50, 500, 100 });
			notifBuilder.SetTicker("HEY YOU! YOU HAVE A NOTIFICATION!");
			notifBuilder.SetVisibility(NotificationVisibility.Public);
			notifBuilder.SetPriority((int)NotificationPriority.Default);
			notifBuilder.SetContentIntent(PendingIntent.GetActivity(context, 0, new Intent(context, typeof(MainActivity)), 0));

			Notification notif = notifBuilder.Build();
			NotificationManager notifManager = (NotificationManager)context.GetSystemService(Context.NotificationService);
			notifManager.Notify((int)Java.Lang.JavaSystem.CurrentTimeMillis(), notif); // using time to make a different ID every time, so doesn't replace old notification 
			*/

			wl.Release();
		}

		private void onWakeInitializationProtocol()
		{
			_initCWD();
			_verify();
		}

		private void randomizeQuoteFact()
		{
			List<string> currentData = getCurrent();

			List<string> quotes = File.ReadAllLines("Quotes.dat").ToList();
			List<string> facts = File.ReadAllLines("Facts.dat").ToList();

			Random r = new Random();
			if (quotes.Count > 0) { currentData[4] = quotes[r.Next(0, quotes.Count)]; }
			if (facts.Count > 0) { currentData[5] = facts[r.Next(0, facts.Count)]; }

			File.WriteAllLines("Current.dat", currentData);
		}

		private void randomizeAllElse()
		{
			List<string> currentData = getCurrent();

			List<string> projects = File.ReadAllLines("Projects.dat").ToList();
			List<string> studies = File.ReadAllLines("Studies.dat").ToList();
			List<string> extraStudies = File.ReadAllLines("ExtraStudies.dat").ToList();
			List<string> activities = File.ReadAllLines("Activities.dat").ToList();

			Random r = new Random();
			if (projects.Count > 0) { currentData[0] = projects[r.Next(0, projects.Count)]; }
			if (studies.Count > 0) { currentData[1] = studies[r.Next(0, studies.Count)]; }
			if (extraStudies.Count > 0) { currentData[2] = extraStudies[r.Next(0, extraStudies.Count)]; }
			if (activities.Count > 0) { currentData[3] = activities[r.Next(0, activities.Count)]; }

			File.WriteAllLines("Current.dat", currentData);
		}

		private List<string> getCurrent()
		{
			List<string> current = File.ReadAllLines("Current.dat").ToList();

			// handle any empty stuff in list
			for (int i = 0; i < current.Count; i++)
			{
				if (current[i] == null || current[i] == "") { current[i] = "NO ITEMS"; }
			}

			return current;
		}

		private void _initCWD()
		{
			string documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
			Directory.SetCurrentDirectory(documentsPath);
		}

		private void _verify()
		{
			if (!File.Exists("CurrentData.dat")) { File.Create("CurrentData.dat"); }
			if (!File.Exists("Projects.dat")) { File.Create("Projects.dat"); }
			if (!File.Exists("Studies.dat")) { File.Create("Studies.dat"); }
			if (!File.Exists("ExtraStudies.dat")) { File.Create("ExtraStudies.dat"); }
			if (!File.Exists("Activities.dat")) { File.Create("Activities.dat"); }
			if (!File.Exists("Quotes.dat")) { File.Create("Quotes.dat"); }
			if (!File.Exists("Facts.dat")) { File.Create("Facts.dat"); }
		}



		// start the hourly part
		public void SetAlarm(Context context)
		{
			Console.WriteLine("Setting alarm");
			AlarmManager am = (AlarmManager)context.GetSystemService(Context.AlarmService);
			Intent intent = new Intent(context, typeof(HourlyAlarm));
			
			PendingIntent pendingIntent = PendingIntent.GetBroadcast(context, 0, intent, 0);

			DateTime futureDateTime = DateTime.Now;
			futureDateTime = futureDateTime.AddMinutes(1);
			DateTime betterDT = new DateTime(futureDateTime.Year, futureDateTime.Month, futureDateTime.Day, futureDateTime.Hour, futureDateTime.Minute, 0, 0);

			long ms = getMS(betterDT);

			am.SetInexactRepeating(AlarmType.RtcWakeup, ms, 1000 * 60, pendingIntent);
		}

		// also converts to utc and based on unix time
		private long getMS(DateTime time)
		{
			DateTime UnixTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);

			long unixTicks = UnixTime.Ticks;
			long timeTicksUTC = time.ToUniversalTime().Ticks;

			long actualTicks = timeTicksUTC - unixTicks;
		
			return actualTicks / TimeSpan.TicksPerMillisecond;
		}

		public void CancelAlarm(Context context)
		{
			/*Console.WriteLine("Canceling alarm");
			Intent intent = new Intent(context, typeof(HourlyAlarm));
			PendingIntent sender = PendingIntent.GetBroadcast(context, 0, intent, 0);
			AlarmManager am = (AlarmManager)context.GetSystemService(Context.AlarmService);
			am.Cancel(sender);*/
		}
	}
}