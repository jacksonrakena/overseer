﻿using Abyssal.Common;
using AbyssalSpotify;
using Discord;
using Discord.WebSocket;
using Humanizer;
using Qmmands;
using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.Versioning;
using System.Text;
using System.Threading.Tasks;

namespace Abyss.Commands.Default
{
    [Name("Meta")]
    [Description("Provides commands related to me.")]
    public class MetaModule : AbyssModuleBase
    {
        private readonly ICommandService _commandService;
        private readonly AbyssConfig _config;
        private readonly DiscordSocketClient _client;

        public MetaModule(DiscordSocketClient client, ICommandService commandService, AbyssConfig config)
        {
            _commandService = commandService;
            _config = config;
            _client = client;
        }

        [Command("uptime")]
        [Description("Displays the time that this bot process has been running.")]
        public Task<ActionResult> Command_GetUptimeAsync()
        {
            return Ok($"**Uptime:** {(DateTime.Now - Process.GetCurrentProcess().StartTime).Humanize(20)}");
        }

        [Command("version")]
        [RunMode(RunMode.Parallel)]
        [Description("Provides some detailed information about the current version.")]
        public Task<ActionResult> Command_GetVersionInfoAsync()
        {
            var dotnetVersion =
                Assembly.GetEntryAssembly()?.GetCustomAttribute<TargetFrameworkAttribute>()?.FrameworkName ??
                ".NET Core";

            var fwAssembly = Assembly.GetAssembly(typeof(AbyssHostedService))!;

            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("```");

            stringBuilder.AppendLine($"                 Runtime: {dotnetVersion}");
            stringBuilder.AppendLine($"         Abyss framework: {fwAssembly.GetName().Version!}");
            stringBuilder.AppendLine($"             Command set: {Assembly.GetExecutingAssembly()!.GetName().Version!}");
            stringBuilder.AppendLine($"             Discord.Net: {DiscordConfig.Version}");
            stringBuilder.AppendLine($"                 Qmmands: {Assembly.GetAssembly(typeof(CommandService))!.GetName().Version}");
            stringBuilder.AppendLine($"          AbyssalSpotify: {Assembly.GetAssembly(typeof(SpotifyClient))!.GetName().Version}");
            stringBuilder.AppendLine($"Abyssal Common Libraries: {Assembly.GetAssembly(typeof(HiddenAttribute))!.GetName().Version}");
            stringBuilder.AppendLine($"   Release configuration: {fwAssembly.GetCustomAttribute<AssemblyConfigurationAttribute>()!.Configuration}");
            stringBuilder.AppendLine($"           Total modules: {_commandService.GetAllModules().Count}");
            stringBuilder.AppendLine($"          Total commands: {_commandService.GetAllCommands().Count}");

            stringBuilder.AppendLine("```");
            return Text(stringBuilder.ToString());
        }

        [Command("info", "about")]
        [RunMode(RunMode.Parallel)]
        [Description("Shows some information about me.")]
        public async Task<ActionResult> Command_GetAbyssInfoAsync()
        {
            var app = await Context.Client.GetApplicationInfoAsync().ConfigureAwait(false);
            var response = new EmbedBuilder
            {
                ThumbnailUrl = Context.Client.CurrentUser.GetEffectiveAvatarUrl(),
                Description = string.IsNullOrEmpty(app.Description) ? "None" : app.Description,
                Author = new EmbedAuthorBuilder
                {
                    Name = $"Information about Abyss",
                    IconUrl = Context.Bot.GetEffectiveAvatarUrl()
                }
            };

            response
                .AddField("Uptime", DateTime.Now - Process.GetCurrentProcess().StartTime)
                .AddField("Heartbeat", Context.Client.Latency + "ms", true)
                .AddField("Commands", _commandService.GetAllCommands().Count(), true)
                .AddField("Modules", _commandService.GetAllModules().Count(), true)
                .AddField("Source", $"https://github.com/abyssal/Abyss");

            return Ok(response);
        }

        [Command("feedback")]
        [Description("Sends feedback to the developer.")]
        [AbyssCooldown(1, 24, CooldownMeasure.Hours, CooldownType.User)]
        public Task<ActionResult> Command_SendFeedbackAsync([Remainder] [Range(1, 500)] string feedback)
        {
            if (_config.Notifications.Feedback == null || !(_client.GetChannel(_config.Notifications.Feedback.Value) is SocketTextChannel stc))
                return BadRequest("Feedback has been disabled for this bot.");

            var _ = stc.SendMessageAsync(
                $"Feedback from {Context.Invoker}:\n\"{feedback}\"");

            return Ok();
        }

        [Command("prefix")]
        [Description("Shows the prefix.")]
        public Task<ActionResult> ViewPrefixesAsync()
        {
            return Text($"The prefix is `{Context.GetPrefix()}`, but you can invoke commands by mention as well, such as: \"{Context.BotUser.Mention} help\".");
        }

        [Command("hasperm")]
        [Description("Checks if I have a permission accepted.")]
        public Task<ActionResult> Command_HasPermissionAsync(
            [Name("Permission")] [Remainder] [Description("The permission to check for.")]
            string permission)
        {
            var guildPerms = Context.Guild.CurrentUser.GuildPermissions;
            var props = guildPerms.GetType().GetProperties();

            var boolProps = props.Where(a =>
                a.PropertyType.IsAssignableFrom(typeof(bool))
                && (a.Name.Equals(permission, StringComparison.OrdinalIgnoreCase)
                 || a.Name.Humanize().Equals(permission, StringComparison.OrdinalIgnoreCase))).ToList();
            /* Get a list of all properties of Boolean type and that match either the permission specified, or match it   when humanized */

            if (boolProps.Count == 0) return BadRequest($"Unknown permission `{permission}` :(");

            var perm = boolProps[0];
            var name = perm.Name.Humanize();
            var value = (bool)perm.GetValue(guildPerms)!;

            return Ok(a => a.WithDescription($"I **{(value ? "do" : "do not")}** have permission `{name}`!"));
        }

        [Command("invite")]
        [Description("Creates an invite to add me to another server.")]
        public Task<ActionResult> Command_GetInviteAsync()
        {
            return Ok("You can add me using " + UrlHelper.CreateMarkdownUrl("this link.", $"https://discordapp.com/api/oauth2/authorize?client_id={Context.Client.CurrentUser.Id}&permissions=0&scope=bot"));
        }
    }
}