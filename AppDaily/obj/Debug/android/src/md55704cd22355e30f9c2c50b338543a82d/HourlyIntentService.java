package md55704cd22355e30f9c2c50b338543a82d;


public class HourlyIntentService
	extends mono.android.app.IntentService
	implements
		mono.android.IGCUserPeer
{
	static final String __md_methods;
	static {
		__md_methods = 
			"n_onHandleIntent:(Landroid/content/Intent;)V:GetOnHandleIntent_Landroid_content_Intent_Handler\n" +
			"n_onCreate:()V:GetOnCreateHandler\n" +
			"";
		mono.android.Runtime.register ("AppDaily.HourlyIntentService, AppDaily, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", HourlyIntentService.class, __md_methods);
	}


	public HourlyIntentService (java.lang.String p0) throws java.lang.Throwable
	{
		super (p0);
		if (getClass () == HourlyIntentService.class)
			mono.android.TypeManager.Activate ("AppDaily.HourlyIntentService, AppDaily, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "System.String, mscorlib, Version=2.0.5.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e", this, new java.lang.Object[] { p0 });
	}


	public HourlyIntentService () throws java.lang.Throwable
	{
		super ();
		if (getClass () == HourlyIntentService.class)
			mono.android.TypeManager.Activate ("AppDaily.HourlyIntentService, AppDaily, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onHandleIntent (android.content.Intent p0)
	{
		n_onHandleIntent (p0);
	}

	private native void n_onHandleIntent (android.content.Intent p0);


	public void onCreate ()
	{
		n_onCreate ();
	}

	private native void n_onCreate ();

	java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
