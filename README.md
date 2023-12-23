# ORC Interrogator

Blazor Web App for visualizing and analyzing [Prisoner's dilemma](https://en.wikipedia.org/wiki/Prisoner%27s_dilemma) battles

# Setup for new battle

1. Add stragy classes, that implement the IStrategy interface, to `Strategies`. They have to be defined in the namespace "Strategy". You can use sub namespaces.
2. Add a json file to `wwwroot/data/prisoners/` with a list of the strategies.

Example:
For strategy classes Strategy.Year2023.Bear  and Strategy.Tiger
a file "animals.json"
```json
[
  {
    "name": "Balu",
    "strategy": "Year2023.Bear"
  },
  {
    "name": "Shere Khan",
    "strategy": "Tiger"
  }
]
```

3. Add a json file to `wwwroot/data/prisons/` to configure the battle parameters and refer to the prisoners json file.

Example:
`zoo.json`

```json
{
  "name": "Zoo",
  "prisoners": "animals",  # the prisoners json file without extension
  "bothCooperate": 2,
  "bothBetrayed": 4,
  "betraying": 1,
  "betrayed": 6,
  "turns":50,              # 0 for random
  "msPerTurn": 0.5
}
```

4. Add your new prison in `Game/PrionManager.cs` to the `Prisons` dictionary:

```csharp
  public static readonly Dictionary<string, string> Prisons = new() {
        { "zoo", "The zoo fight" } # the prions file without extension
    };
```
