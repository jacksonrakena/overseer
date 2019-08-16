using System;
using Discord;
using Discord.Commands;

namespace Abyss.Core.Entities
{
    public enum CooldownType
    {
        Server,
        Channel,
        User,
        Global
    }

    public static class CooldownExtensions
    {
        public static string GetFriendlyName(this CooldownType type)
        {
            return type switch
                {
                CooldownType.Server => "This server",
                CooldownType.Channel => "This channel",
                CooldownType.User => "You",
                CooldownType.Global => "Everybody",
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, null),
                };
        }

        public static string GetPerName(this CooldownType type)
        {
            return type switch
                {
                CooldownType.Server => "Per server",
                CooldownType.Channel => "Per channel",
                CooldownType.User => "Per user",
                CooldownType.Global => "Global",
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, null),
                };
        }
    }
}