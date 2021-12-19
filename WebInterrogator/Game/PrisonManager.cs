
using Interrogator.Game.Setup;

namespace Interrogator.Game;

public class PrisonManager
{
    public static readonly Dictionary<string, string> Prisons = new() {
        { "christmas-2021", "Christmas 2021" },
        { "full-prison", "Full Prison" },
        { "test-prison", "Test Prison" },
        { "slow-prison", "Slow Prison" }
    };
    
    public delegate void PrisonChangedEventHandler(object sender);
    public event PrisonChangedEventHandler PrisonChanged;
    public Prison ActivePrison { get; private set; }
    
    public List<Round> Rounds { get; private set; }
    
    public int CurrentRoundIndex;
    
    public int TotalRoundCount => this.Rounds?.Count ?? 0;
    
    public Round CurrentRound =>
        this.CurrentRoundIndex < this.TotalRoundCount ? this.Rounds[this.CurrentRoundIndex] : null;
    
    public bool IsRoundFinished => this._currentTurn > this.ActivePrison.NumberOfTurns;

    public bool IsGameFinished =>  TotalRoundCount > 0 && this.CurrentRoundIndex >= this.TotalRoundCount;
    
    private int _currentTurn;
    
    private IList<Participant> _allParticipants;

    public void InitializeNewGame(Prison prison)
    {
        this.ActivePrison = prison;
        PrisonChanged.Invoke(this);
        PrepareGame();
    }

    private void PrepareGame()
    {
        this.Rounds = this.ActivePrison.CreateRounds();
        this._allParticipants = this.Rounds.SelectMany(r => r.Duels)
            .SelectMany(d => new[] { d.Participants.Item1, d.Participants.Item2} ).ToList();
        this.CurrentRoundIndex = 0;
        this._currentTurn = 1;
    }

    public void PrepareNextRound()
    {
        this.ActivePrison.ResetStrategies();
        this.CurrentRoundIndex++;
        this._currentTurn = 1;
    }

    public void FastRun()
    {
        while(!IsGameFinished)
        {
            RunRoundInterrogations();
            FinishRound();
            PrepareNextRound();
        }
    }

    private void RunRoundInterrogations()
    {
        for(var i = 0; i < this.ActivePrison.NumberOfTurns; i++)
        {
            RunTurnInterrogations();
        }
    }

    public void RunTurnBatch()
    {
        for (var x = 0; x < this.ActivePrison.TurnsPerMs; x++)
        {
            RunTurnInterrogations();
        }
       
    }
    
    private void RunTurnInterrogations()
    {
        foreach (var duel in this.CurrentRound.Duels)
        {
            duel.Interrogate(this._currentTurn, this.ActivePrison.PayoffCalculator);
        }
        this._currentTurn += 1;
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

    public int GetMaxSentence()
    {
        return this.GetAllParticipants().Max(p => p.Sentence);
    }
    
    public int GetMinSentence()
    {
        return this.GetAllParticipants().Min(p => p.Sentence);
    }
    
    public IEnumerable<Participant> GetAllParticipants()
    {
        return this._allParticipants;
    }
}
