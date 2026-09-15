using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using AshVP;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [Header("Lap UI")]
    [SerializeField] private Text lapText = null;
    [SerializeField] private Text[] lapTexts = null;

    [Header("Countdown and Timer")]
    [SerializeField] private Text countdownText = null;
    [SerializeField] private Text timerText = null;
    [SerializeField] private int countStartTime = 5;

    [Header("Players and Input")]
    [SerializeField] private PlayerController[] players = null;
    [SerializeField] private InputManager_AshVP[] carInputManagers = null;

    [Header("Finish UI")]
    [SerializeField] private GameObject button = null;
    [SerializeField] private GameObject resultPanel = null;
    [SerializeField] private Text[] resultTexts = null; // result panel texts (順位のみ)

    [Header("Rank Texts Top Down")]
    [Tooltip("1位が element 0 に対応するように順番を揃えてください")]
    [SerializeField] private Text[] rankTexts = null;    // 1位〜N位 を上から並べる Text 配列
    [SerializeField] private Color finishedColor = new Color(0.2f, 0.2f, 0.2f); // ゴール時の文字色

    public enum PlayState { None, Ready, Play, Finish }
    public PlayState CurrentState = PlayState.None;

    private float currentCountDown = 0f;
    private float timer = 0f;

    private int goalCount = 0;
    private float[] finishTimes;
    private List<int> finishOrder = new List<int>();

    private Color[] originalLapTextColors;

    void Awake()
    {
        int maxPlayers = (players != null) ? players.Length : 0;
        finishTimes = new float[maxPlayers];
        for (int i = 0; i < finishTimes.Length; i++) finishTimes[i] = -1f;

        if (resultPanel != null) resultPanel.SetActive(false);
        if (button != null) button.SetActive(false);

        // hide rank texts initially and clear text
        if (rankTexts != null)
        {
            for (int i = 0; i < rankTexts.Length; i++)
            {
                if (rankTexts[i] != null)
                {
                    rankTexts[i].gameObject.SetActive(false);
                    rankTexts[i].text = "";
                }
            }
        }

        // cache original lap text colors
        if (lapTexts != null)
        {
            originalLapTextColors = new Color[lapTexts.Length];
            for (int i = 0; i < lapTexts.Length; i++)
            {
                originalLapTextColors[i] = (lapTexts[i] != null) ? lapTexts[i].color : Color.white;
            }
        }
        else
        {
            originalLapTextColors = new Color[0];
        }
    }

    void Start()
    {
        CountDownStart();

        if (players != null)
        {
            for (int i = 0; i < players.Length; i++)
            {
                var p = players[i];
                if (p == null) continue;
                PlayerController playerRef = p;
                p.LapEvent.RemoveAllListeners();
                p.GoalEvent.RemoveAllListeners();
                p.LapEvent.AddListener(() => OnLap(playerRef));
                p.GoalEvent.AddListener(() => OnGoal(playerRef));
            }
        }

        if (timerText != null) timerText.text = "Time : 000.000 s";

        if (lapTexts != null && players != null)
        {
            int len = Mathf.Min(lapTexts.Length, players.Length);
            for (int i = 0; i < len; i++)
            {
                if (lapTexts[i] != null && players[i] != null)
                    lapTexts[i].text = players[i].LapCount + "/" + players[i].GoalLap;
            }
        }
        else if (lapText != null && players != null && players.Length > 0)
        {
            lapText.text = players[0].LapCount + "/" + players[0].GoalLap;
        }
    }

    void Update()
    {
        if (CurrentState == PlayState.Ready)
        {
            currentCountDown -= Time.deltaTime;
            if (currentCountDown > 0f)
            {
                int intNum = Mathf.CeilToInt(currentCountDown);
                if (countdownText != null) countdownText.text = intNum.ToString();
            }
            else
            {
                StartPlay();
                if (countdownText != null) countdownText.text = "START";
                StartCoroutine(WaitErase());
            }
        }
        else if (CurrentState == PlayState.Play)
        {
            timer += Time.deltaTime;
            if (timerText != null) timerText.text = "Time : " + timer.ToString("000.000") + " s";
        }
        else
        {
            if (timerText != null) timerText.text = "Time : " + timer.ToString("000.000") + " s";
        }
    }

    void CountDownStart()
    {
        currentCountDown = countStartTime;
        SetPlayState(PlayState.Ready);
        if (countdownText != null) countdownText.gameObject.SetActive(true);

        if (carInputManagers != null)
        {
            foreach (var input in carInputManagers)
            {
                if (input != null) input.enabled = false;
            }
        }
    }

    void StartPlay()
    {
        SetPlayState(PlayState.Play);
        if (carInputManagers != null)
        {
            foreach (var input in carInputManagers)
            {
                if (input != null) input.enabled = true;
            }
        }
    }

    IEnumerator WaitErase()
    {
        yield return new WaitForSeconds(2f);
        if (countdownText != null) countdownText.gameObject.SetActive(false);
    }

    void SetPlayState(PlayState state)
    {
        CurrentState = state;
        if (players != null)
        {
            foreach (var p in players)
            {
                if (p != null) p.CurrentState = state;
            }
        }
    }

    void OnLap(PlayerController p)
    {
        if (p == null) return;
        int index = System.Array.IndexOf(players, p);
        if (index < 0) return;

        if (index < finishTimes.Length && finishTimes[index] >= 0f) return;

        UpdateLapUIForPlayer(index);
    }

    void UpdateLapUIForPlayer(int playerIndex)
    {
        if (players == null) return;
        if (playerIndex < 0 || playerIndex >= players.Length) return;

        var p = players[playerIndex];
        if (p == null) return;

        int displayLap = Mathf.Clamp(p.LapCount, 0, p.GoalLap);

        if (lapTexts != null && playerIndex < lapTexts.Length && lapTexts[playerIndex] != null)
        {
            lapTexts[playerIndex].text = displayLap + "/" + p.GoalLap;
        }
        else if (lapText != null && playerIndex == 0)
        {
            lapText.text = displayLap + "/" + p.GoalLap;
        }
    }

    void OnGoal(PlayerController p)
    {
        if (p == null) return;
        int index = System.Array.IndexOf(players, p);
        if (index < 0) return;

        if (index < finishTimes.Length && finishTimes[index] >= 0f) return;

        if (index < finishTimes.Length) finishTimes[index] = timer;
        finishOrder.Add(index);

        if (carInputManagers != null && index < carInputManagers.Length)
        {
            var input = carInputManagers[index];
            if (input != null)
            {
                input.enabled = false;
                if (input.carController != null)
                {
                    input.carController.ProvideInputs(0f, 0f, 0f);
                }
            }
        }

        int rank = finishOrder.Count; // 1 = first

        // Darken lap text color and ensure final display shows GoalLap
        if (lapTexts != null && index < lapTexts.Length && lapTexts[index] != null)
        {
            lapTexts[index].color = finishedColor;
            lapTexts[index].text = Mathf.Clamp(players[index].LapCount, 0, players[index].GoalLap) + "/" + players[index].GoalLap;
        }
        else if (lapText != null && index == 0)
        {
            lapText.color = finishedColor;
            lapText.text = Mathf.Clamp(players[index].LapCount, 0, players[index].GoalLap) + "/" + players[index].GoalLap;
        }

        // ゴールしたプレイヤー自身の下に順位を表示
        if (rankTexts != null && index >= 0 && index < rankTexts.Length)
        {
            var rt = rankTexts[index];

            if (rt != null)
            {
                rt.gameObject.SetActive(true);

                if (rank == 1)
                    rt.text = "1st";
                else if (rank == 2)
                    rt.text = "2nd";
                else if (rank == 3)
                    rt.text = "3rd";
                else
                    rt.text = rank + "th";
            }
        }

        goalCount++;

        if (players != null && goalCount >= players.Length)
        {
            AllPlayersGoal();
        }
    }

    void AllPlayersGoal()
    {
        SetPlayState(PlayState.Finish);

        if (countdownText != null)
        {
            countdownText.text = "GOAL";
            countdownText.gameObject.SetActive(true);
        }

        if (carInputManagers != null)
        {
            foreach (var input in carInputManagers)
            {
                if (input != null)
                {
                    input.enabled = false;
                    if (input.carController != null) input.carController.ProvideInputs(0f, 0f, 0f);
                }
            }
        }

        if (button != null) button.SetActive(true);
    }

    void ShowResults()
    {
        if (resultPanel == null || resultTexts == null)
        {
            Debug.LogWarning("Result panel or result texts not assigned.");
            return;
        }

        resultPanel.SetActive(true);

        for (int i = 0; i < resultTexts.Length; i++)
        {
            if (resultTexts[i] == null) continue;

            if (i < finishOrder.Count)
            {
                // i番目にゴールしたプレイヤー
                int playerIndex = finishOrder[i];

                resultTexts[i].text =
                    $"{i + 1}位 : Player {playerIndex + 1}";
            }
            else
            {
                resultTexts[i].text = $"{i + 1}位 : -";
            }
        }
    }

    public void OnResultSceneButton()
    {
        Debug.Log("Result scene button pressed.");
    }

    public void Retry()
    {
        if (rankTexts != null)
        {
            for (int i = 0; i < rankTexts.Length; i++)
            {
                if (rankTexts[i] != null)
                {
                    rankTexts[i].gameObject.SetActive(false);
                    rankTexts[i].text = "";
                }
            }
        }

        if (lapTexts != null)
        {
            for (int i = 0; i < lapTexts.Length; i++)
            {
                if (lapTexts[i] != null)
                {
                    Color orig = (i < originalLapTextColors.Length) ? originalLapTextColors[i] : Color.white;
                    lapTexts[i].color = orig;
                    if (players != null && i < players.Length && players[i] != null)
                        lapTexts[i].text = players[i].LapCount + "/" + players[i].GoalLap;
                }
            }
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
