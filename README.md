# Stripe QuickStart for .NET

## Prerequisites

- .NET 8
- Visual Studio or Visual Studio Code
- .NET Android and .NET iOS workloads

## Server

### Steps

1/ Create your `Program.dev.cs`

1.a/ File location

```
|--src
    |-- Dotnet.Stripe.QuickStart.Host
        |-- Program.cs
        |-- Program.dev.cs        <------      YOUR_FILE_HERE
```

1.b/ File content

```cs
partial class Program
{
    static Program()
    {
        StripeConfiguration.ApiKey = "YOUR_API_KEY";
    }
}
```

2/ Run up the web server without debugging

![Run up the web server without debugging](./assets/start-webserver-without-debugging.png)

## Android

The quick start is the get-started guide how to use Stripe Android SDK binding libraries created by [tuyen-vuduc](https://github.com/tuyen-vuduc). The source of the binding libraries can be found [here](https://github.com/tuyen-vuduc/dotnet-binding-utils). This is based on [the official quick start guide](https://docs.stripe.com/payments/quickstart?client=java) from Stripe.

To understand all functionalities of Stripe Android SDK, please check out [the official document](https://docs.stripe.com/libraries/android) and [the official repository](https://github.com/stripe/stripe-android).

### Preparation

1/ Create your `ExampleApplication.dev.cs` with below details

1.a/ File location

```
|--src
    |-- Dotnet.Stripe.Android.QuickStart
        |-- ExampleApplication.cs
        |-- ExampleApplication.dev.cs        <------      YOUR_FILE_HERE
```

1.b/ File content

```cs
partial class ExampleApplication
{
    public static string PUBLISHABLE_KEY = "YOUR_PUBLISHABLE_KEY";
    public static string BACKEND_URL = "YOUR_BACKEND_URL";
}
```

2/ Execute one .HTTP call in `src\Dotnet.Stripe.QuickStart.Host\Dotnet.Stripe.QuickStart.Host.http`

3/ Copy the clientSecret and assign to below variable in `MainActivity.cs`

```cs
    ...
    private string paymentIntentClientSecret = "{FILL_VIA_.HTTP_CALL}";
    ...
```

4/ Run up the sample app

## iOS

TBC

## MAINTAINER

This project is maintained by [tuyen-vuduc](https://github.com/tuyen-vuduc) in his spare time.<br>

If you find this project is useful, please become a sponsor of the project and/or buy him a coffee.

[!["Buy Me A Coffee"](https://www.buymeacoffee.com/assets/img/custom_images/orange_img.png)](https://www.buymeacoffee.com/tuyen.vuduc)

OR

[![](https://img.shields.io/static/v1?label=Sponsor&message=%E2%9D%A4&logo=GitHub&color=%23fe8e86)](https://github.com/sponsors/tuyen-vuduc)

[:heart: Sponsor](https://github.com/sponsors/yourGitHubUserName)

## LICENSE

The 3rd libraries will follow their associated licenses. This project itself is license under MIT license.

Copyright 2024 Tuyen Vu

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.