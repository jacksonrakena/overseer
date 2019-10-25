﻿using System;
using Microsoft.Extensions.DependencyInjection;
using AbyssalSpotify;
using System.Net.Http;
using Discord.WebSocket;
using Discord;
using Qmmands;
using Microsoft.Extensions.Hosting;

namespace Abyss
{
    public class AbyssConfigOptions
    {
        /// <summary>
        ///     The data root for this Abyss instance. Should contain the abyss configuration file, and any plugins.
        /// </summary>
        public string DataRoot { get; set; }
    }

    public static class ServiceCollectionExtensions
    {
        public static void ConfigureAbyss(this IServiceCollection serviceCollection, Action<AbyssConfigOptions> acoAction)
        {
            var aco = new AbyssConfigOptions();
            acoAction(aco);
            if (string.IsNullOrWhiteSpace(aco.DataRoot)) throw new InvalidOperationException($"A data root must be set.");
            serviceCollection.AddSingleton(prov =>
            {
                return new DataService(aco.DataRoot, prov.GetRequiredService<IHostEnvironment>(), prov.GetRequiredService<DiscordSocketClient>(),
                    prov.GetRequiredService<MessageReceiver>(), prov.GetRequiredService<ICommandService>(), prov.GetRequiredService<IServiceCollection>()); ;
            });
            serviceCollection.AddHostedService<AbyssHostedService>();
            serviceCollection.AddSingleton<HelpService>();
            serviceCollection.AddSingleton<MessageReceiver>();
            serviceCollection.AddSingleton<ScriptingService>();
            serviceCollection.AddSingleton(provider =>
            {
                var configurationModel = provider.GetRequiredService<AbyssConfig>();
                return SpotifyClient.FromClientCredentials(configurationModel.Connections.Spotify.ClientId, configurationModel.Connections.Spotify.ClientSecret);
            });
            serviceCollection.AddTransient<Random>();
            serviceCollection.AddSingleton<HttpClient>();
            serviceCollection.AddSingleton<NotificationsService>();
            serviceCollection.AddSingleton<MarketingService>();

            serviceCollection.AddSingleton(new DiscordSocketClient(new DiscordSocketConfig
            {
                MessageCacheSize = 100,
                LogLevel = LogSeverity.Debug,
                DefaultRetryMode = RetryMode.RetryTimeouts,
                AlwaysDownloadUsers = true,
                ExclusiveBulkDelete = false
            }));

            var commandService = new CommandService(new CommandServiceConfiguration
            {
                StringComparison = StringComparison.OrdinalIgnoreCase,
                DefaultRunMode = RunMode.Sequential,
                IgnoresExtraArguments = true,
                CooldownBucketKeyGenerator = AbyssCooldownBucketKeyGenerators.Default,
                DefaultArgumentParser = DefaultArgumentParser.Instance
            });
            commandService.AddArgumentParser(UnixArgumentParser.Instance);
            serviceCollection.AddSingleton<ICommandService>(commandService);
            serviceCollection.AddSingleton(serviceCollection);
        }
    }
}