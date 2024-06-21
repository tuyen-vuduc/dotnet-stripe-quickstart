using System;
using System.Diagnostics;
using CoreFoundation;
using QuickStart.Dotnet.Shared;
using StripeCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

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

        //var configuration = PaymentSheet.Configuration()
        //configuration.merchantDisplayName = "Example, Inc."


        //let paymentSheet = PaymentSheet(
        //    paymentIntentClientSecret: paymentIntentClientSecret,
        //    configuration: configuration)

        //paymentSheet.present(from: self) {
        //    [weak self] (paymentResult) in
        //    switch paymentResult {
        //        case .completed:
        //            self?.displayAlert(title: "Payment complete!")
        //    case .canceled:
        //            print("Payment canceled!")
        //    case .failed(let error):
        //            self?.displayAlert(title: "Payment failed", message: error.localizedDescription)
        //    }
        //}
    }
}