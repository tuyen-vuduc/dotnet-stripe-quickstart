using QuickStart.Dotnet.Shared;
using StripeCore;

namespace QuickStart.Dotnet.StripeIos;

[Register ("AppDelegate")]
public class AppDelegate : UIApplicationDelegate {
	public override UIWindow? Window {
		get;
		set;
	}

	public override bool FinishedLaunching (UIApplication application, NSDictionary launchOptions)
    {
        StripeAPI.DefaultPublishableKey = ClientHelper.PUBLISHABLE_KEY;

        // create a new window instance based on the screen size
        Window = new UIWindow (UIScreen.MainScreen.Bounds);
		Window.RootViewController = new CheckoutViewController();

		// make the window visible
		Window.MakeKeyAndVisible ();

		return true;
	}
}

