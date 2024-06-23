using System;
using System.Diagnostics;
using CoreFoundation;
using QuickStart.Dotnet.Shared;
using StripeCore;
using TVStripePaymentSheet;

namespace QuickStart.Dotnet.StripeIos;

public partial class CheckoutViewController : UIViewController
{
    UIButton payButton;
    string paymentIntentClientSecret;

    public override void ViewDidLoad()
    {
        base.ViewDidLoad();

        View.BackgroundColor = UIColor.SystemBackground;

        payButton = new UIButton(UIButtonType.Custom);
        payButton.SetTitle("Pay now", UIControlState.Normal);
        payButton.BackgroundColor = UIColor.SystemIndigo;
        payButton.Layer.CornerRadius = 5;
        payButton.ContentEdgeInsets = new(top: 12, left: 12, bottom: 12, right: 12);

        payButton.TouchDragInside += Pay;
        payButton.TranslatesAutoresizingMaskIntoConstraints = false;
        payButton.Enabled = false;

        View.AddSubview(payButton);

        NSLayoutConstraint.ActivateConstraints(new [] {
            payButton.LeadingAnchor.ConstraintEqualTo(View.SafeAreaLayoutGuide.LeadingAnchor, constant: 16),
            payButton.TrailingAnchor.ConstraintEqualTo(View.SafeAreaLayoutGuide.TrailingAnchor, constant: -16),
            payButton.BottomAnchor.ConstraintEqualTo(View.SafeAreaLayoutGuide.BottomAnchor, constant: -16),
        });

        ClientHelper.FetchPaymentIntent()
            .ContinueWith(t =>
            {
                var (ok, msg) = t.Result;
                if (!ok)
                {
                    showAlert("API Error", msg);
                    return;
                }

                paymentIntentClientSecret = msg;

                InvokeOnMainThread(() => payButton.Enabled = true);
                Debug.WriteLine("Retrieved PaymentIntent");
            })
        .ConfigureAwait(false);
    }

    void showAlert(string title, string message)
    {
        DispatchQueue.MainQueue.DispatchAsync(() => {
            var alertController = UIAlertController.Create(title, message, UIAlertControllerStyle.Alert);
            alertController.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, null));
            PresentViewController(alertController, true, null);
        });
    }

    private void Pay(object? sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(paymentIntentClientSecret))
        {
            return;
        }

        var configuration = new TSPSConfiguration();
        configuration.MerchantDisplayName = "Example, Inc.";

        var paymentSheet = new TSPSPaymentSheet(
            paymentIntentClientSecret: paymentIntentClientSecret,
            configuration: configuration);

        paymentSheet.PresentFrom(this, (paymentResult, error) => {
            switch (paymentResult) {
                case TSPSPaymentSheetResult.Completed:
                    DisplayAlert("Payment complete!");
                    break;
                case TSPSPaymentSheetResult.Canceled:
                    Debug.WriteLine("Payment canceled!");
                    break;
                default:
                    DisplayAlert("Payment failed", error?.LocalizedDescription);
                    break;
            }
        });
    }

    private void DisplayAlert(string title, string? message = default)
    {
        InvokeOnMainThread(() =>
        {
            var alert = UIAlertController.Create(title, message, UIAlertControllerStyle.Alert);
            alert.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, null));

            PresentViewController(alert, true, null);
        });
    }
}