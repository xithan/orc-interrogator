using System.Net.Http.Json;

namespace Interrogator.Game.Setup;

public class Loader
{
    public Loader(HttpClient http)
    {
        this.Http = http;
    }

    private HttpClient Http { get; }
    
    public async Task<Prison> LoadPrison(string jsonFileName = "test-prison")
    {
        if (jsonFileName == null)
        {
            jsonFileName = "test-prison";
        }
        var prisonConfig = await Http.GetFromJsonAsync<PrisonConfig>($"data/prisons/{jsonFileName}.json");
        var prisoners = await LoadPrisoners(prisonConfig.Prisoners);
        return new Prison(prisonConfig, prisoners);
    }
    
    public async Task<IEnumerable<Prisoner>> LoadPrisoners(string jsonFileName)
    {
        var prisonerConfig = await Http.GetFromJsonAsync<PrisonerConfig[]>($"data/prisoners/{jsonFileName}.json");
        return prisonerConfig.Select(c => new Prisoner(c.Name, c.Strategy));
    }
    
}