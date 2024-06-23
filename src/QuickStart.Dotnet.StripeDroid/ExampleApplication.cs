using Android.OS;
using Com.Stripe.Android;
using Android.Runtime;
using QuickStart.Dotnet.Stripe;

namespace QuickStart.Dotnet.StripeDroid;

/// <summary>
/// https://github.com/stripe/stripe-android/blob/master/example/src/main/java/com/stripe/example/ExampleApplication.kt
/// </summary>
[Application]
public partial class ExampleApplication : Application
{
    public static bool IS_PENALTY_DEATH_ENABLED = false;

    public ExampleApplication(nint javaReference, JniHandleOwnership transfer)
        : base(javaReference, transfer)
    {
        
    }

    public override void OnCreate()
    {
        PaymentConfiguration.Init(this, ClientHelper.DEFAULT_PUBLISHABLE_KEY);

        var threadPolicyBuilder =
            new StrictMode.ThreadPolicy.Builder()
                .DetectDiskReads()
                .DetectDiskWrites()
                .DetectAll()
                .PenaltyLog();
        if (IS_PENALTY_DEATH_ENABLED)
        {
            threadPolicyBuilder.PenaltyDeath();
        }

        StrictMode.SetThreadPolicy(threadPolicyBuilder.Build());

        var vmPolicyBuilder =
            new StrictMode.VmPolicy.Builder()
                .DetectLeakedSqlLiteObjects()
                .PenaltyLog()
                .PenaltyDeath();

        if (Build.VERSION.SdkInt < BuildVersionCodes.R)
        {
            vmPolicyBuilder.DetectLeakedClosableObjects();
        }

        StrictMode.SetVmPolicy(vmPolicyBuilder.Build());

        base.OnCreate();
    }
}