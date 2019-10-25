﻿using Discord;
using Discord.WebSocket;
using System;
using System.Threading.Tasks;

namespace Abyss
{
    public class NotificationsService
    {
        private readonly AbyssConfigNotificationsSection? _notifyConfig;
        private readonly DiscordSocketClient _client;

        public NotificationsService(AbyssConfig config, DiscordSocketClient client)
        {
            _notifyConfig = config.Notifications;
            _client = client;
        }

        public async Task NotifyReadyAsync(bool firstTime = false)
        {
            if (_notifyConfig?.Ready == null) return;
            var ch = _client.GetChannel(_notifyConfig.Ready.Value);
            if (!(ch != null && ch is SocketTextChannel stc)) return;


            var embed = new EmbedBuilder()
                .WithAuthor(_client.CurrentUser.ToEmbedAuthorBuilder())
                .WithColor(AbyssHostedService.DefaultEmbedColour)
                .WithCurrentTimestamp()
                .WithThumbnailUrl(_client.CurrentUser.GetEffectiveAvatarUrl(2048));

            await stc.SendMessageAsync(null, false, embed
                .WithDescription($"Abyss instance {(firstTime ? "started and" : "")} ready at " + DateTime.Now.ToString("F") + ". Connected to " + _client.Guilds.Count + " guilds.")
                .Build());
        }

        public async Task NotifyStoppingAsync()
        {
            if (_notifyConfig?.Stopping == null) return;

            var ch = _client.GetChannel(_notifyConfig.Stopping.Value);
            if (!(ch != null && ch is SocketTextChannel stc)) return;

            await stc.SendMessageAsync(null, false, new EmbedBuilder()
                    .WithAuthor(_client.CurrentUser.ToEmbedAuthorBuilder())
                    .WithDescription($"Abyss instance stopping at " + DateTime.Now.ToString("F"))
                    .WithColor(AbyssHostedService.DefaultEmbedColour)
                    .WithCurrentTimestamp()
                    .WithThumbnailUrl(_client.CurrentUser.GetEffectiveAvatarUrl(2048))
                    .Build());
            return;
        }

        public async Task NotifyServerMembershipChangeAsync(SocketGuild arg, bool botIsJoining)
        {
            if (_notifyConfig?.ServerMembershipChange == null) return;
            var updateChannel = _client.GetChannel(_notifyConfig.ServerMembershipChange.Value);
            if (!(updateChannel is SocketTextChannel stc)) return;
            await stc.SendMessageAsync(null, false, new EmbedBuilder()
                 .WithAuthor(_client.CurrentUser.ToEmbedAuthorBuilder())
                 .WithDescription($"{(botIsJoining ? "Joined" : "Left")} {arg.Name} at {DateTime.Now:F}")
                 .AddField("Member count", arg.MemberCount, true)
                 .AddField("Channel count", arg.TextChannels.Count + " text / " + arg.VoiceChannels.Count + " voice", true)
                 .AddField("Owner", $"{arg.Owner} ({arg.OwnerId})", true)
                 .AddField("Total bot guilds", _client.Guilds.Count, true)
                 .WithColor(AbyssHostedService.DefaultEmbedColour)
                 .WithThumbnailUrl(arg.IconUrl)
                 .WithCurrentTimestamp()
                 .Build());
        }
    }
}