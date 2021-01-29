using System;
using System.Collections.Generic;
using System.Text;

namespace WarframeDiscordBot.definitions
{
    class EventDefinitions//defines a single event
    {
        public string description { get; set; }
        public Rewards[] rewards { get; set; }
        public int maximumScore { get; set; }
        public InterimSteps[] interimSteps { get; set; }
    }
    public class Rewards
    {
        public string asString { get; set; }
    }

    public class InterimSteps
    {
        public int goal { get; set; }
        public EventReward reward { get; set; }
    }
    public class EventReward
    {
        public string asString { get; set; }
    }
}
