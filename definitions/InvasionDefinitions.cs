using System;
using System.Collections.Generic;
using System.Text;

namespace WarframeDiscordBot.definitions
{
    class InvasionDefinitions//defines a single fissure
    {
        public string node { get; set; }
        public AttackerReward attackerReward { get; set; }
        public DefenderReward defenderReward { get; set; }
        public string attackingFaction { get; set; }
        public string defendingFaction { get; set; }

    }
    public class AttackerReward
    {
        public string asString { get; set; }
    }
    public class DefenderReward
    {
        public string asString { get; set; }
    }
}

