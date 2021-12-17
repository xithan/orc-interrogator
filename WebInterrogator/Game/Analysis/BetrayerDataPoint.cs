using System.Net.Mime;
using Newtonsoft.Json;

namespace Interrogator.Game.Analysis;

public struct BetrayerDataPoint
{
    public BetrayerDataPoint(string prisoner, Move move, int count)
    {
        this.Prisoner = prisoner;
        this.Count = count;
        this._move = move;
    }

    [JsonProperty("x")] public string Prisoner { get; set; }

    [JsonProperty("y")] public int Count { get; set; }

    [JsonProperty("c")] public int MoveType => this._move == Move.Betray ? 0 : 1;

    [System.Text.Json.Serialization.JsonIgnore] private Move _move;
}
