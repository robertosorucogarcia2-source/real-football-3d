using UnityEngine;

public enum FootballGameMode
{
    Exhibition,
    Tournament,
    Career
}

public class GameModeManager : MonoBehaviour
{
    public FootballGameMode SelectedMode { get; private set; } = FootballGameMode.Exhibition;
    public string HomeTeam { get; private set; } = "Real Madrid";
    public string AwayTeam { get; private set; } = "Barcelona";

    public void SelectExhibition() => SelectedMode = FootballGameMode.Exhibition;
    public void SelectTournament() => SelectedMode = FootballGameMode.Tournament;
    public void SelectCareer() => SelectedMode = FootballGameMode.Career;

    public void SetTeams(string home, string away)
    {
        if (string.IsNullOrWhiteSpace(home) || string.IsNullOrWhiteSpace(away) || home == away)
        {
            Debug.LogWarning("Choose two different valid teams.");
            return;
        }

        HomeTeam = home;
        AwayTeam = away;
    }

    public void StartSelectedMode()
    {
        Debug.Log($"Starting {SelectedMode}: {HomeTeam} vs {AwayTeam}");
        // Connect this method to your menu Play button.
        // Next milestone: load the match scene and initialize the selected mode.
    }
}
