package md5a7107fb84d75b41c4deec5807ba26fc9;


public class ViewPageAdapterTest
	extends android.support.v4.view.PagerAdapter
	implements
		mono.android.IGCUserPeer,
		android.media.MediaPlayer.OnCompletionListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_getCount:()I:GetGetCountHandler\n" +
			"n_isViewFromObject:(Landroid/view/View;Ljava/lang/Object;)Z:GetIsViewFromObject_Landroid_view_View_Ljava_lang_Object_Handler\n" +
			"n_instantiateItem:(Landroid/view/ViewGroup;I)Ljava/lang/Object;:GetInstantiateItem_Landroid_view_ViewGroup_IHandler\n" +
			"n_destroyItem:(Landroid/view/View;ILjava/lang/Object;)V:GetDestroyItem_Landroid_view_View_ILjava_lang_Object_Handler\n" +
			"n_onCompletion:(Landroid/media/MediaPlayer;)V:GetOnCompletion_Landroid_media_MediaPlayer_Handler:Android.Media.MediaPlayer/IOnCompletionListenerInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\n" +
			"";
		mono.android.Runtime.register ("xamarin_app.Droid.ViewPageAdapterTest, xamarin_app.Android", ViewPageAdapterTest.class, __md_methods);
	}


	public ViewPageAdapterTest ()
	{
		super ();
		if (getClass () == ViewPageAdapterTest.class)
			mono.android.TypeManager.Activate ("xamarin_app.Droid.ViewPageAdapterTest, xamarin_app.Android", "", this, new java.lang.Object[] {  });
	}


	public int getCount ()
	{
		return n_getCount ();
	}

	private native int n_getCount ();


	public boolean isViewFromObject (android.view.View p0, java.lang.Object p1)
	{
		return n_isViewFromObject (p0, p1);
	}

	private native boolean n_isViewFromObject (android.view.View p0, java.lang.Object p1);


	public java.lang.Object instantiateItem (android.view.ViewGroup p0, int p1)
	{
		return n_instantiateItem (p0, p1);
	}

	private native java.lang.Object n_instantiateItem (android.view.ViewGroup p0, int p1);


	public void destroyItem (android.view.View p0, int p1, java.lang.Object p2)
	{
		n_destroyItem (p0, p1, p2);
	}

	private native void n_destroyItem (android.view.View p0, int p1, java.lang.Object p2);


	public void onCompletion (android.media.MediaPlayer p0)
	{
		n_onCompletion (p0);
	}

	private native void n_onCompletion (android.media.MediaPlayer p0);

	private java.util.ArrayList refList;
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
