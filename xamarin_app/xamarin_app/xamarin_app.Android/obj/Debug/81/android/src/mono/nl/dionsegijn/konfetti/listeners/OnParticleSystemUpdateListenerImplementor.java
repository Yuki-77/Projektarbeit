package mono.nl.dionsegijn.konfetti.listeners;


public class OnParticleSystemUpdateListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		nl.dionsegijn.konfetti.listeners.OnParticleSystemUpdateListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onParticleSystemEnded:(Lnl/dionsegijn/konfetti/KonfettiView;Lnl/dionsegijn/konfetti/ParticleSystem;I)V:GetOnParticleSystemEnded_Lnl_dionsegijn_konfetti_KonfettiView_Lnl_dionsegijn_konfetti_ParticleSystem_IHandler:NL.DionSegijn.Konfetti.Listeners.IOnParticleSystemUpdateListenerInvoker, DanielMartinusKonfetti\n" +
			"n_onParticleSystemStarted:(Lnl/dionsegijn/konfetti/KonfettiView;Lnl/dionsegijn/konfetti/ParticleSystem;I)V:GetOnParticleSystemStarted_Lnl_dionsegijn_konfetti_KonfettiView_Lnl_dionsegijn_konfetti_ParticleSystem_IHandler:NL.DionSegijn.Konfetti.Listeners.IOnParticleSystemUpdateListenerInvoker, DanielMartinusKonfetti\n" +
			"";
		mono.android.Runtime.register ("NL.DionSegijn.Konfetti.Listeners.IOnParticleSystemUpdateListenerImplementor, DanielMartinusKonfetti", OnParticleSystemUpdateListenerImplementor.class, __md_methods);
	}


	public OnParticleSystemUpdateListenerImplementor ()
	{
		super ();
		if (getClass () == OnParticleSystemUpdateListenerImplementor.class)
			mono.android.TypeManager.Activate ("NL.DionSegijn.Konfetti.Listeners.IOnParticleSystemUpdateListenerImplementor, DanielMartinusKonfetti", "", this, new java.lang.Object[] {  });
	}


	public void onParticleSystemEnded (nl.dionsegijn.konfetti.KonfettiView p0, nl.dionsegijn.konfetti.ParticleSystem p1, int p2)
	{
		n_onParticleSystemEnded (p0, p1, p2);
	}

	private native void n_onParticleSystemEnded (nl.dionsegijn.konfetti.KonfettiView p0, nl.dionsegijn.konfetti.ParticleSystem p1, int p2);


	public void onParticleSystemStarted (nl.dionsegijn.konfetti.KonfettiView p0, nl.dionsegijn.konfetti.ParticleSystem p1, int p2)
	{
		n_onParticleSystemStarted (p0, p1, p2);
	}

	private native void n_onParticleSystemStarted (nl.dionsegijn.konfetti.KonfettiView p0, nl.dionsegijn.konfetti.ParticleSystem p1, int p2);

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
