using System.Net.Mime;

namespace Interrogator.Game.Analysis;

public struct SentenceDataPoint
{
    public SentenceDataPoint(string prisoner, int position, string opponent, int sentence)
    {
        this.Prisoner = prisoner;
        this.Position = position;
        this.Opponent = opponent;
        this.Sentence = sentence;
    }

    public int Sentence { get; set; }

    public string Opponent { get; set; }

    public int Position { get; set; }

    public string Prisoner { get; set; }
}