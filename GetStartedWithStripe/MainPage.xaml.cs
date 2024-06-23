using Stripe.PaymentSheets;
using System.Diagnostics;

namespace GetStartedWithStripe
{
    public partial class MainPage : ContentPage
    {
        private readonly IPaymentSheet paymentSheet;

        public MainPage(IPaymentSheet paymentSheet)
        {
            InitializeComponent();
            this.paymentSheet = paymentSheet;
        }

        private string paymentIntentClientSecret;
        protected override async void OnAppearing()
        {
            base.OnAppearing();

            await QuickStart.Dotnet.Stripe.ClientHelper.FetchPaymentIntent()
                .ContinueWith(t =>
                {
                    var (ok, msg) = t.Result;
                    if (!ok)
                    {
                        MainThread.BeginInvokeOnMainThread(() => DisplayAlert("API Error", msg, "OK"));
                        return;
                    }

                    paymentIntentClientSecret = msg;

                    MainThread.BeginInvokeOnMainThread(() => PayNowButton.IsEnabled = true);
                })
                .ConfigureAwait(false);
        }

        private async void OnPayNow(object sender, EventArgs e)
        {
            var configuration = new Configuration("TuyenTV.dev Co.Ltd.");

            var (result, msg) = await paymentSheet.PresentWithPaymentIntentAsync(paymentIntentClientSecret, configuration)
                .ContinueWith(t =>
                {
                    if (t.IsCompletedSuccessfully) return (t.Result, null);

                    return (PaymentSheetResult.Canceled, "Cannot pay for a reason...");
                });

            switch (result)
            {
                case PaymentSheetResult.Completed:
                    await DisplayAlert("Payment Result", "Payment completed successfully!", "OK");
                    break;
                case PaymentSheetResult.Canceled:
                    if (!string.IsNullOrWhiteSpace(msg))
                    {
                        await DisplayAlert("Payment Result", "Payment completed failed!", "Try again");
                    }
                    break;
            }

            Debug.WriteLine("Payment complete.");
        }
    }
}
