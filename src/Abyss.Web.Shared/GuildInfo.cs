﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Abyss.Web.Shared
{
    public class GuildInfo
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string IconUrl { get; set; }
        public string Owner { get; set; }
        public int MemberCount { get; set; }
        public string HighestRoleName { get; set; }
    }
}
