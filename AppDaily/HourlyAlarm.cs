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
	// thanks to http://stackoverflow.com/questions/4459058/alarm-manager-example
	[BroadcastReceiver]
	class HourlyAlarm : BroadcastReceiver
	{
		public override void OnReceive(Context context, Intent intent)
		{
			PowerManager pm = (PowerManager)context.GetSystemService(Context.PowerService);
			PowerManager.WakeLock wl = pm.NewWakeLock(WakeLockFlags.Partial, "HOURLY_ALARM");

			wl.Acquire();
			
			
			
			Console.WriteLine("Running hourly alarm!");
			//Toast.MakeText(context, "Alarm !!!!!!!!!!",ToastLength.Long).Show(); // For example

			Notification.Builder notifBuilder = new Notification.Builder(context);
			notifBuilder.SetContentTitle("Service msg");
			notifBuilder.SetContentText("Here's your hourly reminder!");
			notifBuilder.SetSmallIcon(Resource.Drawable.Icon);
			notifBuilder.SetVibrate(new long[] { 500, 50, 500, 100 });
			notifBuilder.SetTicker("HEY YOU! YOU HAVE A NOTIFICATION!");
			notifBuilder.SetVisibility(NotificationVisibility.Public);
			notifBuilder.SetPriority((int)NotificationPriority.Default);
			notifBuilder.SetContentIntent(PendingIntent.GetActivity(context, 0, new Intent(context, typeof(MainActivity)), 0));

			Notification notif = notifBuilder.Build();
			NotificationManager notifManager = (NotificationManager)context.GetSystemService(Context.NotificationService);
			notifManager.Notify((int)Java.Lang.JavaSystem.CurrentTimeMillis(), notif); // using time to make a different ID every time, so doesn't replace old notification 

			wl.Release();
		}

		public void SetAlarm(Context context)
		{
			Console.WriteLine("Setting alarm");
			AlarmManager am = (AlarmManager)context.GetSystemService(Context.AlarmService);
			Intent intent = new Intent(context, typeof(HourlyAlarm));
			//PendingIntent pendingIntent = PendingIntent.GetBroadcast(context, 0, intent, PendingIntentFlags.UpdateCurrent);
			
			
			
			PendingIntent pendingIntent = PendingIntent.GetBroadcast(context, 0, intent, 0);

			// THIS REPEATING IS WORKING!!
			//am.SetRepeating(AlarmType.RtcWakeup, Java.Lang.JavaSystem.CurrentTimeMillis(), 1000 * 60, pendingIntent);

			DateTime futureDateTime = DateTime.Now;
			futureDateTime = futureDateTime.AddMinutes(1);
			DateTime betterDT = new DateTime(futureDateTime.Year, futureDateTime.Month, futureDateTime.Day, futureDateTime.Hour, futureDateTime.Minute, 0, 0);
			//futureDateTime = futureDateTime.AddHours(1);
			//DateTime betterDT = new DateTime(futureDateTime.Year, futureDateTime.Month, futureDateTime.Day, futureDateTime.Hour, 0, 0, 0);

			long ms = getMS(betterDT);
			long prev = getMS(DateTime.Now);
			long dif = ms - prev;
			Console.WriteLine("Waiting " + dif + " ms");


			Console.WriteLine("DateTime: " + getMS(DateTime.Now));
			Console.WriteLine("JavaTime: " + Java.Lang.JavaSystem.CurrentTimeMillis());

			//am.Set(AlarmType.RtcWakeup, ms, pendingIntent);

			am.SetRepeating(AlarmType.RtcWakeup, ms, 1000 * 60, pendingIntent);
			
			
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