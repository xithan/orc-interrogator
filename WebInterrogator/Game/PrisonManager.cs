
using Interrogator.Game.Setup;

namespace Interrogator.Game;

public class PrisonManager
{
    public Prison ActivePrison { get; set; }
    
    public List<Round> Rounds { get; set; }
    
    public int CurrentRoundIndex;

    public int RoundTotalCount => this.Rounds?.Count ?? 0;

    public Round ActiveRound => Rounds[this.CurrentRoundIndex];
    
    public async Task InitializeNewGame(Prison prison)
    {
        this.ActivePrison = prison;
        this.Rounds = this.ActivePrison.CreateRounds();
        this.CurrentRoundIndex = 0;
    }
    
    public async Task PrepareNextRound()
    {
        this.ActivePrison.ResetStrategies();
        this.CurrentRoundIndex++;
    }
    
    public void FinishRound()
    {
        foreach (var duel in this.ActiveRound.Duels)
        {
            var (p1, p2) = duel.Participants;
            p1.Prisoner.TotalSentence += p1.Sentence;
            p2.Prisoner.TotalSentence += p2.Sentence;
            if (p1.Sentence == p2.Sentence)
            {
                p1.Prisoner.DuelWins += 0.5;
                p2.Prisoner.DuelWins += 0.5;
            }
            else
            {
                var winner = (p1.Sentence > p2.Sentence)
                    ? p2.Prisoner
                    : p1.Prisoner;
                winner.DuelWins += 1;
            }
        }
    }
    
    
}