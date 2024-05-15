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

3/ Expose your local webserver with [ngrok](https://ngrok.com)

```
ngrok http https://localhost:4243
```

## Android

 This quick start is based on [the official quick start guide](https://docs.stripe.com/payments/quickstart?client=java) from Stripe. It will guide you how to use Stripe Android SDK binding libraries created by [tuyen-vuduc](https://github.com/tuyen-vuduc). The source of the binding libraries can be found [here](https://github.com/tuyen-vuduc/dotnet-binding-utils).

> To understand all functionalities of Stripe Android SDK, please check out [the official document](https://docs.stripe.com/libraries/android) and [the official repository](https://github.com/stripe/stripe-android).

### Steps to run the sample app

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
    public static string BACKEND_URL = "YOUR_BACKEND_URL"; // Please use above generated ngrok URL here
}
```

2/ Run up and try out the sample app

### Steps to use in your app

1/ Add the following NuGet feed to your `nuget.config`

> Stripe Android SDK depends on some libraries which have the binding ones created by Xamarin, but not yet updated. I created my own for the mising version and uploaded to [MyGet](https://myget.org) as the workaround in the meanwhile.

```
<add key="Tuyen - MyGet" value="https://www.myget.org/F/tuyen-vuduc/api/v3/index.json" />
```

2/ Add required NuGet packages

> We need `Xamarin.KotlinX.Serialization.Core` added directly because it wasn't detected as an indirect dependency for Stripe Android SDK via [my dotnet-binding-util](https://github.com/tuyen-vuduc/dotnet-binding-utils) when creating the binding library.

```
<PackageReference Include="Com.Stripe.StripeAndroid" Version="20.40.4" />
<PackageReference Include="Xamarin.KotlinX.Serialization.Core" Version="1.6.3.1" />
```

## iOS

TBD

## MAINTAINER

This project is maintained by [tuyen-vuduc](https://github.com/tuyen-vuduc) in his spare time.<br>

If you find this project is useful, please become a sponsor of the project and/or buy him a coffee.

[!["Buy Me A Coffee"](https://www.buymeacoffee.com/assets/img/custom_images/orange_img.png)](https://www.buymeacoffee.com/tuyen.vuduc)

OR

[![](https://img.shields.io/static/v1?label=Sponsor&message=%E2%9D%A4&logo=GitHub&color=%23fe8e86)](https://github.com/sponsors/tuyen-vuduc)

## LICENSE

The 3rd libraries will follow their associated licenses. This project itself is license under MIT license.

Copyright 2024 tuyen-vuduc

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.