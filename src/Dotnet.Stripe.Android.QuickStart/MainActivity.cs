using Com.Stripe.Android.Paymentsheet.Addresselement;
using Com.Stripe.Android.Paymentsheet;
using Android.Util;
using Android.Content;
using AndroidX.AppCompat.App;
using Java.Lang;
using AlertDialog = AndroidX.AppCompat.App.AlertDialog;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Net.Http.Json;

namespace Dotnet.Stripe.Android.QuickStart;

[Activity(Label = "@string/app_name", MainLauncher = true, Theme = "@style/Theme.AppCompat.Light.DarkActionBar")]
public partial class MainActivity : AppCompatActivity
{
    private const string TAG = "CheckoutActivity";

    private string paymentIntentClientSecret = "{FILL_VIA_.HTTP_CALL}";

    private PaymentSheet paymentSheet;

    private Button payButton;

    private AddressLauncher addressLauncher;

    private AddressDetails shippingDetails;

    private Button addressButton;

    private AddressLauncher.Configuration configuration;

    protected override void OnCreate(Bundle? savedInstanceState)
    {
        base.OnCreate(savedInstanceState);

        // Set our view from the "main" layout resource
        SetContentView(Resource.Layout.activity_main);

        configuration = new AddressLauncher.Configuration.Builder()
                    .AdditionalFields(
                            new AddressLauncher.AdditionalFieldsConfiguration(
                                AddressLauncher.AdditionalFieldsConfiguration.FieldConfiguration.Required
                            )
                    )
                    .AllowedCountries(["US", "CA", "GB"])
                    .Title("Shipping Address")
                    //.GooglePlacesApiKey("(optional) YOUR KEY HERE")
                    .Build();

        // Hook up the pay button
        payButton = FindViewById<Button>(Resource.Id.pay_button);
        payButton.Click += onPayClicked;
        // TODO: Use .HTTP to call for clientSecret
        payButton.Enabled = false;

        paymentSheet = new PaymentSheet(this, this);

        // Hook up the address button
        addressButton = FindViewById<Button>(Resource.Id.address_button);
        addressButton.Click += onAddressClicked;
        addressLauncher = new AddressLauncher(this, this);

        // TODO CANNOT DO for now: https://github.com/xamarin/xamarin-android/issues/8542
        fetchPaymentIntent()
            .ContinueWith(t =>
            {
                Console.WriteLine("DONE");
            })
            .ConfigureAwait(false);
    }

    private async Task fetchPaymentIntent()
    {
        var request = new PaymentIntentCreateRequest
        {
            Items = [
                new Item {
                    Id = Guid.NewGuid().ToString(),
                    Amount = (new Random().NextDouble() * 100).ToString(),
                }
              ],
        };
        var httpClient = new HttpClient()
        {
            BaseAddress = new Uri(ExampleApplication.BACKEND_URL),
        };

        var response = await httpClient.PostAsJsonAsync("/create-payment-intent", request);
        
        if (!response.IsSuccessStatusCode)
        {
            showAlert("API Error", $"API returned status code: {response.StatusCode.ToString()} {(int)response.StatusCode}");
            return;
        }

        var dataInJSON = await response.Content.ReadAsStringAsync();
        var data = JsonSerializer.Deserialize<PaymentIntentCreateResposne>(dataInJSON);

        paymentIntentClientSecret = data.ClientSecret;
        RunOnUiThread(()=> payButton.Enabled = true);
        Log.Info(TAG, "Retrieved PaymentIntent");
    }

    private void showAlert(string title, string message)
    {
        RunOnUiThread(() => {
            AlertDialog dialog = new AlertDialog.Builder(this)
                .SetTitle(title)
                .SetMessage(message)
                .SetPositiveButton("Ok", (IDialogInterfaceOnClickListener)null)
                .Create();
            dialog.Show();
        });
    }

    private void showToast(string message)
    {
        RunOnUiThread(() => Toast.MakeText(this, message, ToastLength.Long).Show());
    }

    private void onPayClicked(object sender, EventArgs e)
    {
        PaymentSheet.Configuration configuration = new PaymentSheet.Configuration("Example, Inc.");

        // Present Payment Sheet
        paymentSheet.PresentWithPaymentIntent(paymentIntentClientSecret, configuration);
    }

    private void onAddressClicked(object sender, EventArgs e)
    {
        addressLauncher.Present(
          ExampleApplication.PUBLISHABLE_KEY,
          configuration
        );
    }
}

partial class MainActivity : IPaymentSheetResultCallback
{
    public void OnPaymentSheetResult(PaymentSheetResult paymentSheetResult)
    {
        if (paymentSheetResult is PaymentSheetResult.Completed) {
            showToast("Payment complete!");
        } else if (paymentSheetResult is PaymentSheetResult.Canceled) {
            Log.Info(TAG, "Payment canceled!");
        } else if (paymentSheetResult is PaymentSheetResult.Failed failedResult) {
            Throwable error = failedResult.Error;
            showAlert("Payment failed", error.LocalizedMessage);
        }
    }
}

partial class MainActivity : IAddressLauncherResultCallback
{
    public void OnAddressLauncherResult(AddressLauncherResult addressLauncherResult)
    {
        // TODO: Handle result and update your UI
        if (addressLauncherResult is AddressLauncherResult.Succeeded succeededResult) {
            shippingDetails = succeededResult.Address;
        } else if (addressLauncherResult is AddressLauncherResult.Canceled) {
            // TODO: Handle cancel
        }
    }
}

public record Item
{
    [JsonPropertyName("id")]
    public string Id { get; set; }
    [JsonPropertyName("Amount")]
    public string Amount { get; set; }
}

public record PaymentIntentCreateRequest
{
    [JsonPropertyName("items")]
    public Item[] Items { get; set; }
}

public record PaymentIntentCreateResposne
{
    [JsonPropertyName("clientSecret")]
    public string ClientSecret { get; set; }
}