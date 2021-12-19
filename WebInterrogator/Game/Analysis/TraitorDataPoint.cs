using System.Net.Mime;
using Newtonsoft.Json;

namespace Interrogator.Game.Analysis;

public struct TraitorDataPoint
{
    public TraitorDataPoint(string prisoner, int moveType, int count, int percentage)
    {
        this.Prisoner = prisoner;
        this.Count = count;
        this.MoveType = moveType;
        this.Percentage = percentage;
    }
    
    [JsonProperty("x")] public string Prisoner { get; set; }

    [JsonProperty("y")] public int Count { get; set; }

    [JsonProperty("c")] public int MoveType { get; set; }
    
    [JsonProperty("percentage")] public int Percentage { get; set; }
}
