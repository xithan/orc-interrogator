using System.Net.Mime;
using Newtonsoft.Json;

namespace Interrogator.Game.Analysis;

public struct Node
{
    public Node(string prisoner, int index)
    {
        this.Prisoner = prisoner;
        this.Group = 0;
        this.Index = index;
    }

    [JsonProperty("name")] public string Prisoner { get; set; }

    [JsonProperty("group")] public int Group { get; set; }

    [JsonProperty("index")] public int Index { get; set; }
}
