
using Interrogator.Game.Setup;

namespace Interrogator.Game;

public class PrisonManager
{
    public Prison ActivePrison { get; private set; }
    
    public List<Round> Rounds { get; private set; }
    
    public int CurrentRoundIndex;
    
    public int TotalRoundCount => this.Rounds?.Count ?? 0;
    
    public Round CurrentRound => this.Rounds[this.CurrentRoundIndex];
    
    public bool IsRoundFinished => this._currentTurn > this.ActivePrison.NumberOfTurns;
    
    private int _currentTurn;

    public void InitializeNewGame(Prison prison)
    {
        this.ActivePrison = prison;
        this.Rounds = this.ActivePrison.CreateRounds();
        this.CurrentRoundIndex = 0;
        this._currentTurn = 1;
    }
    
    public void PrepareNextRound()
    {
        this.ActivePrison.ResetStrategies();
        this.CurrentRoundIndex++;
        this._currentTurn = 1;
    }

    public void RunTurnBatch()
    {
        for (var x = 0; x < this.ActivePrison.TurnsPerMs; x++)
        {
            foreach (var duel in this.CurrentRound.Duels)
            {
                duel.Interrogate(this._currentTurn, this.ActivePrison.PayoffCalculator);
            }
            this._currentTurn += 1;
        }
       
    }
    
    public void FinishRound()
    {
        foreach (var duel in this.CurrentRound.Duels)
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

    public IEnumerable<Duel> GetAllDuels()
    {
        return this.Rounds.SelectMany(r => r.Duels);
    }
}
