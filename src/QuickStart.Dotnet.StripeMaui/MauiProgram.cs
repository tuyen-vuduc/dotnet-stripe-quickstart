﻿using Microsoft.Extensions.Logging;
using Stripe;

namespace QuickStart.Dotnet.StripeMaui;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.UseStripe(QuickStart.Dotnet.Stripe.ClientHelper.DEFAULT_PUBLISHABLE_KEY)
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

#if DEBUG
		builder.Logging.AddDebug();
#endif
		builder.Services.AddScoped<MainPage>();

		return builder.Build();
	}
}

