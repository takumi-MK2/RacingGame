using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class StageSelect_Arrows : MonoBehaviour
{
    [System.Serializable]
    public class PlayerCursor
    {
        [Header("プレイヤーごとの設定")]
        public RectTransform arrowTransform;
        public string horizontalAxis;
        public string submitButton;
        public string cancelButton;
        public int playerNumber;

        [HideInInspector] public int currentIndex = 0;
        [HideInInspector] public bool inputReset = true;
        [HideInInspector] public bool isReady = false;
    }

    [Header("ステージボタン（左から順）")]
    public RectTransform[] stageButtons;

    [Header("矢印を表示するオフセット値")]
    public float yOffset = -100f;
    public float xSpacing = 40f;

    [Header("通常のユラユラ設定 (待機時)")]
    public float waveSpeed = 3.5f;
    public float waveHeight = 35f;

    [Header("次のシーンの名前")]
    public string nextSceneName = "GameMain";

    [Header("プレイヤーデータ (最大4人)")]
    public PlayerCursor[] players;

    private int[] buttonSelectorsCount;

    // --- 🎬 演出用の管理変数 ---
    private enum SelectState { Selecting, DrumRoll, ShowWinner }
    private SelectState currentState = SelectState.Selecting;

    private float stateTimer = 0f;
    private int finalChosenStageIndex = -1;
    private List<int> winnersPlayerIndices = new List<int>(); // 当選ステージを選んでいたプレイヤーたちの番号

    void Start()
    {
        buttonSelectorsCount = new int[stageButtons.Length];
        string[] connectedControllers = Input.GetJoystickNames();

        // 💡 実際にコントローラーが認識されている数（空の文字列を除外）
        int controllerCount = 0;
        foreach (var name in connectedControllers)
        {
            if (!string.IsNullOrEmpty(name)) controllerCount++;
        }

        for (int i = 0; i < players.Length; i++)
        {
            players[i].currentIndex = 0;
            players[i].inputReset = true;
            players[i].isReady = false;

            // 💡 1P (i == 0) の場合は、コントローラーがなくてもマウス/キーボード用に必ず有効化する
            if (i > 0 && i >= controllerCount)
            {
                if (players[i].arrowTransform != null)
                {
                    players[i].arrowTransform.gameObject.SetActive(false);
                }
            }
            else
            {
                // 有効なプレイヤーの矢印は確実に表示する
                if (players[i].arrowTransform != null)
                {
                    players[i].arrowTransform.gameObject.SetActive(true);
                }
            }
        }
        currentState = SelectState.Selecting;
    }

    void Update()
    {
        System.Array.Clear(buttonSelectorsCount, 0, buttonSelectorsCount.Length);

        // 状態ごとのタイマー処理
        if (currentState != SelectState.Selecting)
        {
            stateTimer -= Time.deltaTime;
        }

        // 1. 各状態の処理
        switch (currentState)
        {
            case SelectState.Selecting:
                // 💡 マウスによる1Pの選択処理を先に行う
                HandleMouseInput();

                // 通常の入力受付
                for (int i = 0; i < players.Length; i++)
                {
                    HandlePlayerInput(players[i]);
                    if (IsPlayerActive(players[i]))
                    {
                        buttonSelectorsCount[players[i].currentIndex]++;
                    }
                }
                // 全員決定したかチェック
                CheckAllPlayersReady();
                break;

            case SelectState.DrumRoll:
                // ドラムロール中：全員のボタン位置をそのままキープしてカウント
                for (int i = 0; i < players.Length; i++)
                {
                    if (IsPlayerActive(players[i]))
                    {
                        buttonSelectorsCount[players[i].currentIndex]++;
                    }
                }
                // 時間切れで当選者発表へ
                if (stateTimer <= 0f)
                {
                    StartWinnerPresentation();
                }
                break;

            case SelectState.ShowWinner:
                // 当選発表中：位置をキープ
                for (int i = 0; i < players.Length; i++)
                {
                    if (IsPlayerActive(players[i]))
                    {
                        buttonSelectorsCount[players[i].currentIndex]++;
                    }
                }
                // 2秒経ったらシーン移動
                if (stateTimer <= 0f)
                {
                    MoveToNextScene();
                }
                break;
        }

        // 2. 矢印の移動と演出アニメーションの適用
        int[] currentButtonProcessCount = new int[stageButtons.Length];
        for (int i = 0; i < players.Length; i++)
        {
            int btnIndex = players[i].currentIndex;
            // 💡 配列の範囲外対策（念のためバグ防止）
            btnIndex = Mathf.Clamp(btnIndex, 0, stageButtons.Length - 1);

            int totalSelectors = buttonSelectorsCount[btnIndex];
            int myOrder = currentButtonProcessCount[btnIndex];

            PositionArrowAnimate(players[i], i, btnIndex, myOrder, totalSelectors);

            currentButtonProcessCount[btnIndex]++;
        }
    }

    // 💡 マウスでコースを変更・決定するための新規メソッド
    void HandleMouseInput()
    {
        // 1Pがすでに決定(Ready)状態ならマウス操作を受け付けない
        if (players.Length > 0 && players[0].isReady) return;

        // マウスの左クリックを検知
        if (Input.GetMouseButtonDown(0))
        {
            for (int i = 0; i < stageButtons.Length; i++)
            {
                // クリックされた位置が、ステージボタン（UI）の範囲内かどうかを判定
                if (RectTransformUtility.RectangleContainsScreenPoint(stageButtons[i], Input.mousePosition))
                {
                    // 1Pのターゲットインデックスをクリックしたボタンの番号にする
                    players[0].currentIndex = i;

                    // クリックした瞬間にそのまま選択決定(Ready)にする
                    players[0].isReady = true;
                    Debug.Log($"マウス操作：プレイヤー 1 が ステージ {i + 1} を選択決定しました！");
                    break;
                }
            }
        }
    }

    void HandlePlayerInput(PlayerCursor p)
    {
        if (!IsPlayerActive(p)) return;

        if (p.isReady)
        {
            if (Input.GetButtonDown(p.cancelButton))
            {
                p.isReady = false;
                Debug.Log($"プレイヤー {p.playerNumber} が選択をキャンセルしました。");
            }
            return;
        }

        // 💡 軸の名前やボタン名がインスペクターで未設定（空っぽ）の場合は処理をスキップ（エラー防止）
        if (string.IsNullOrEmpty(p.horizontalAxis) || string.IsNullOrEmpty(p.submitButton)) return;

        float axis = Input.GetAxisRaw(p.horizontalAxis);
        if (axis == 0)
        {
            p.inputReset = true;
        }
        else if (p.inputReset)
        {
            if (axis > 0.5f)
            {
                p.currentIndex = Mathf.Clamp(p.currentIndex + 1, 0, stageButtons.Length - 1);
                p.inputReset = false;
            }
            else if (axis < -0.5f)
            {
                p.currentIndex = Mathf.Clamp(p.currentIndex - 1, 0, stageButtons.Length - 1);
                p.inputReset = false;
            }
        }

        if (Input.GetButtonDown(p.submitButton))
        {
            p.isReady = true;
            Debug.Log($"プレイヤー {p.playerNumber} が ステージ {p.currentIndex + 1} を選択！");
        }
    }

    void CheckAllPlayersReady()
    {
        int activePlayerCount = 0;
        int readyCount = 0;

        for (int i = 0; i < players.Length; i++)
        {
            if (IsPlayerActive(players[i]))
            {
                activePlayerCount++;
                if (players[i].isReady) readyCount++;
            }
        }

        // 全員決定したら、即移動せず「ドラムロール状態」へ移行
        if (activePlayerCount > 0 && readyCount == activePlayerCount)
        {
            Debug.Log("全員決定！ドラムロール開始！");
            currentState = SelectState.DrumRoll;
            stateTimer = 1.5f; // 💡 ドラムロールの長さ（1.5秒間カタカタ動く）
        }
    }

    // ドラムロールが終了した瞬間に1回だけ呼ばれる（内部で抽選）
    void StartWinnerPresentation()
    {
        currentState = SelectState.ShowWinner;
        stateTimer = 2.0f; // 💡 当選者がアピールする時間（2秒間）

        // 投票をリストに集める
        List<int> votedStageIndices = new List<int>();
        for (int i = 0; i < players.Length; i++)
        {
            if (IsPlayerActive(players[i]))
            {
                votedStageIndices.Add(players[i].currentIndex);
            }
        }

        // ランダム抽選
        int randomVoteIndex = Random.Range(0, votedStageIndices.Count);
        finalChosenStageIndex = votedStageIndices[randomVoteIndex];

        Debug.Log($"抽選完了！当選ステージは: ステージ {finalChosenStageIndex + 1}");

        // この当選ステージを選んでいたラッキーなプレイヤーが誰かを調べてリスト化する
        winnersPlayerIndices.Clear();
        for (int i = 0; i < players.Length; i++)
        {
            if (IsPlayerActive(players[i]) && players[i].currentIndex == finalChosenStageIndex)
            {
                winnersPlayerIndices.Add(i); // プレイヤーの配列インデックス(0=1P, 1=2P...)を記録
            }
        }
    }

    // すべての演出が終わったら次のシーンへデータを渡して移動
    void MoveToNextScene()
    {
        // 💡 最終的に移動するシーン名を決める変数（初期値はインスペクターで設定したデフォルト値）
        string actualNextScene = nextSceneName;

        if (PlayerDataManager.instance != null)
        {
            List<int> votedStageIndices = new List<int>();
            int activeCount = 0;
            for (int i = 0; i < players.Length; i++)
            {
                if (IsPlayerActive(players[i]))
                {
                    votedStageIndices.Add(players[i].currentIndex);
                    activeCount++;
                }
            }
            PlayerDataManager.instance.selectedStageIndices = new List<int>(votedStageIndices);
            PlayerDataManager.instance.playerCount = activeCount;

            if (finalChosenStageIndex >= 0 && finalChosenStageIndex < stageButtons.Length)
            {
                PlayerDataManager.instance.finalStageName = stageButtons[finalChosenStageIndex].gameObject.name;
            }
        }

        // ★【選ばれたステージ番号によって行くシーンを切り替える】
        switch (finalChosenStageIndex)
        {
            case 0:
                actualNextScene = "Course01"; // 💡 ステージ1が選ばれたときの実際のシーン名
                break;
            case 1:
                actualNextScene = "Course02"; // 💡 ステージ2が選ばれたときの実際のシーン名
                break;
            case 2:
                actualNextScene = "Course03"; // 💡 ステージ3が選ばれたときの実際のシーン名
                break;
        }

        Debug.Log($"次のシーンに移動します: {actualNextScene}");

        // 決定した実際のシーン名でロードする
        SceneManager.LoadScene(actualNextScene);
    }

    // 💡 状態に合わせて矢印の動き（アニメーション）を細かく計算するメソッド
    void PositionArrowAnimate(PlayerCursor p, int playerIndex, int buttonIndex, int myOrder, int totalSelectors)
    {
        if (buttonIndex < 0 || buttonIndex >= stageButtons.Length || p.arrowTransform == null) return;

        Vector2 targetPos = stageButtons[buttonIndex].anchoredPosition;
        float totalOffsetWidth = (totalSelectors - 1) * xSpacing;
        float startX = -totalOffsetWidth / 2f;
        float finalXOffset = startX + (myOrder * xSpacing);

        // 最終的な矢印の現在のX座標を計算
        float arrowXPosition = targetPos.x + finalXOffset;
        float currentYOffset = yOffset;

        // ★【ここが魔法の隠し味】
        float waveDelay = arrowXPosition * 0.005f;

        // 【状態ごとの見た目の変化】
        if (currentState == SelectState.Selecting)
        {
            if (p.isReady)
            {
                currentYOffset += Mathf.Sin(Time.time * waveSpeed) * waveHeight;
            }
        }
        else if (currentState == SelectState.DrumRoll)
        {
            // ★ドラムロールをウェーブ化！
            currentYOffset += Mathf.Sin((Time.time - waveDelay) * 20f) * 25f;
        }
        else if (currentState == SelectState.ShowWinner)
        {
            // ★当選発表もウェーブしながら大ジャンプ！
            if (winnersPlayerIndices.Contains(playerIndex))
            {
                currentYOffset += Mathf.Abs(Mathf.Sin((Time.time - waveDelay) * 12f)) * 50f;
            }
            else
            {
                currentYOffset += -10f;
            }
        }

        p.arrowTransform.anchoredPosition = new Vector2(arrowXPosition, targetPos.y + currentYOffset);
    }

    bool IsPlayerActive(PlayerCursor p)
    {
        return p.arrowTransform != null && p.arrowTransform.gameObject.activeSelf;
    }
}