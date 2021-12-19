namespace Interrogator.Game.Setup;

public class PrisonConfig
{
    public string Name { get; set; }
    public int Betrayed { get; set; }
    
    public int Betraying { get; set; }
    
    public int BothCooperate { get; set; }
    
    public int BothBetrayed { get; set; }
       
    /// <summary>
    /// json file name  
    /// </summary>
    public string Prisoners { get; set; }
    
    /// <summary>
    /// Number of turns per round
    /// </summary>
    public int? Turns { get; set; }
        
    /// <summary>
    /// Number of milliseconds to wait for each turn
    /// </summary>
    public double MsPerTurn { get; set; }
}