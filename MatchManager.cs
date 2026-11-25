using UnityEngine;
using UnityEngine.UI;

public class MatchManager : MonoBehaviour
{
    [Header("Match")]
    [SerializeField] private float matchLengthSeconds = 300f;
    [SerializeField] private Text clockText;
    [SerializeField] private Text homeScoreText;
    [SerializeField] private Text awayScoreText;

    private float elapsed;
    private int homeScore;
    private int awayScore;
    private bool running = true;

    private void Start() => RefreshUI();

    private void Update()
    {
        if (!running) return;
        elapsed += Time.deltaTime;
        if (elapsed >= matchLengthSeconds)
        {
            elapsed = matchLengthSeconds;
            running = false;
        }
        RefreshClock();
    }

    public void AddHomeGoal() { homeScore++; RefreshUI(); }
    public void AddAwayGoal() { awayScore++; RefreshUI(); }
    public void RestartMatch()
    {
        elapsed = 0;
        homeScore = 0;
        awayScore = 0;
        running = true;
        RefreshUI();
    }

    private void RefreshUI()
    {
        if (homeScoreText != null) homeScoreText.text = homeScore.ToString();
        if (awayScoreText != null) awayScoreText.text = awayScore.ToString();
        RefreshClock();
    }

    private void RefreshClock()
    {
        if (clockText == null) return;
        int gameSeconds = Mathf.FloorToInt(elapsed / matchLengthSeconds * 90f);
        clockText.text = $"{gameSeconds / 60:00}:{gameSeconds % 60:00}";
    }
}
