using System;
using System.Collections.Generic;
using System.Text;

namespace WarframeDiscordBot.definitions
{
    public class AlertDefinitions
    {
        public Mission mission { get; set; }
        public string eta { get; set; }
    }
    public class Mission
    {
        public string node { get; set; }
        public string type { get; set; }
        public string faction { get; set; }
        public Reward reward { get; set; }
        public int minEnemyLevel { get; set; }
        public int maxEnemyLevel { get; set; }
    }
    public class Reward
    {
        public string asString { get; set; }
    }
}
