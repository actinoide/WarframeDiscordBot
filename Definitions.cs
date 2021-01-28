using System;
using System.Collections.Generic;
using System.Text;

namespace WarframeDiscordBot
{
    class Definitions
    {
    }
    public class VoidFissureDefinition//definition for a void fissure
    {
        public string node { get; set; }
        public string missionType { get; set; }
        public string enemy { get; set; }
        public string tier { get; set; }
        public string eta { get; set; }
    }
    public class InvasionDefinition//part definition for a single invasion
    {
        public string node { get; set; }
        public attackerReward attackerReward { get; set; }
        public defenderReward defenderReward { get; set; }
        public string attackingFaction { get; set; }
        public string defendingFaction { get; set; }

    }
    public class attackerReward//part of invasiondefinition
    {
        public string asString { get; set; }
    }
    public class defenderReward//part of invasiondefinition
    {
        public string asString { get; set; }
    }
    public class eventdefinition//definition for a single event
    {
        public string description { get; set; }
        public rewards[] rewards { get; set; }
        public int maximumScore { get; set; }
        public interimSteps[] interimSteps { get; set; }
    }
    public class rewards//part of eventdefinition
    {
        public string asString { get; set; }
    }
    
    public class interimSteps//part of eventdefinition
    {
        public int goal { get; set; }
        public reward reward { get; set; }
    }
    public class reward//part of eventdefinition
    {
        public string asString { get; set; }
    }
}