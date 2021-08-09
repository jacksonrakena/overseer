using System;
using System.Linq;
using System.Threading.Tasks;
using Abyss.Extensions;
using Abyss.Persistence.Relational;
using Disqord;
using Disqord.Bot;
using Qmmands;

namespace Abyss.Modules
{
    [Name("Server")]
    public class ServerModule : AbyssModuleBase
    {
        [Group("prefixes", "prefix", "pfix", "prefixs")]
        [Description("Shows all prefixes.")]
        public class PrefixSubmodule : AbyssGuildModuleBase
        {
            public PrefixSubmodule(AbyssPersistenceContext database)
            {
                _database = database;
            }
            private readonly AbyssPersistenceContext _database;

            [Command]
            public async Task<DiscordCommandResult> PrefixesAsync()
            {
                Console.WriteLine("sus");
                var gsr = await _database.GetGuildConfigAsync(Context.GuildId);
                return Reply(
                    new LocalEmbed()
                        .WithColor(GetColor())
                        .WithAuthor("Prefixes for " + Context.Guild.Name, Context.Guild.GetIconUrl())
                        .WithDescription(string.Join("\n", gsr.Prefixes.Select(p => $"- `{p}`")))
                );
            }

            [Command("add")]
            [Description("Adds a prefix.")]
            [RequireAuthorGuildPermissions(Permission.ManageGuild, Group = "owner_override")]
            [RequireBotOwner(Group = "owner_override")]
            public async Task<DiscordCommandResult> AddPrefixAsync([Name("Prefix")] [Description("The prefix to add.")] string prefix)
            {
                var gsr = await _database.ModifyJsonObjectAsync(d => d.GuildConfigurations, Context.Guild.Id, p =>
                {
                    p.Prefixes.Add(prefix);
                });
                return Reply( 
                    new LocalEmbed()
                        .WithColor(GetColor())
                        .WithDescription($"Added `{prefix}` to the prefixes for {Context.Guild.Name}.")
                );
            }

            [Command("delete", "remove", "rm", "del")]
            [Description("Deletes a prefix.")]
            [RequireAuthorGuildPermissions(Permission.ManageGuild, Group = "owner_override")]
            [RequireBotOwner(Group = "owner_override")]
            public async Task<DiscordCommandResult> DeletePrefixAsync([Name("Prefix")] [Description("The prefix to delete.")] string prefix)
            {
                var gsr = await _database.GetGuildConfigAsync(Context.Guild.Id);
                if (!gsr.Prefixes.Contains(prefix))
                {
                    return Reply(new LocalEmbed().WithColor(GetColor()).WithDescription("That isn't a prefix."));
                }
                var gs = await _database.ModifyJsonObjectAsync(d => d.GuildConfigurations, Context.Guild.Id, d =>
                {
                    d.Prefixes.Remove(prefix);
                });
                return Reply(
                    new LocalEmbed()
                        .WithColor(GetColor())
                        .WithDescription($"Removed `{prefix}` from the prefixes for {Context.Guild.Name}.")
                );
            }
        }
    }
}