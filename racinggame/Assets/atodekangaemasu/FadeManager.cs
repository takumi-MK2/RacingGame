using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeManager : MonoBehaviour
{
    private static FadeManager instance;

    public static FadeManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindAnyObjectByType<FadeManager>();

                if (instance == null)
                {
                    // テストプレイ用にシーン内に無ければ自動生成する
                    GameObject canvasObj = new GameObject("AutoFadeCanvas");
                    Canvas canvas = canvasObj.AddComponent<Canvas>();
                    canvas.renderMode = RenderMode.ScreenSpaceOverlay;
                    canvas.sortingOrder = 999;
                    canvasObj.AddComponent<UnityEngine.UI.CanvasScaler>();
                    canvasObj.AddComponent<UnityEngine.UI.GraphicRaycaster>();

                    GameObject imageObj = new GameObject("FadeImage");
                    imageObj.transform.SetParent(canvasObj.transform);
                    Image img = imageObj.AddComponent<Image>();
                    img.color = new Color(0, 0, 0, 0); // 最初は透明

                    RectTransform rect = img.GetComponent<RectTransform>();
                    rect.anchorMin = Vector2.zero;
                    rect.anchorMax = Vector2.one;
                    rect.sizeDelta = Vector2.zero;

                    instance = canvasObj.AddComponent<FadeManager>();
                    instance.fadeImage = img;

                    DontDestroyOnLoad(canvasObj);
                }
            }
            return instance;
        }
    }

    [Header("フェード用の画像")]
    [SerializeField] private Image fadeImage;

    [Header("フェードにかかる時間（秒）")]
    [SerializeField] private float fadeDuration = 0.5f;

    // 現在フェード中かどうかを判定するフラグ
    private bool isFading = false;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    // ★重要：シーンが読み込まれたイベントを登録します
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // シーンが新しく切り替わったら、毎回必ず自動でフェードイン（明るくする）
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        StartCoroutine(FadeInRoutine());
    }

    public void ChangeScene(string sceneName)
    {
        // すでにフェード中なら多重起動を防ぐ
        if (isFading) return;
        StartCoroutine(FadeOutAndLoadScene(sceneName));
    }

    // 画面をだんだん暗くしてシーンを切り替える
    private IEnumerator FadeOutAndLoadScene(string sceneName)
    {
        isFading = true;
        float timer = 0f;
        Color color = fadeImage.color;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            color.a = Mathf.Lerp(0f, 1f, timer / fadeDuration);
            fadeImage.color = color;
            yield return null;
        }

        color.a = 1f;
        fadeImage.color = color;

        // シーン切り替え（この後、自動的に OnSceneLoaded が呼ばれます）
        SceneManager.LoadScene(sceneName);
    }

    // 画面をだんだん明るくする
    private IEnumerator FadeInRoutine()
    {
        isFading = true;
        float timer = 0f;
        Color color = fadeImage.color;

        // 真っ黒からスタート
        color.a = 1f;
        fadeImage.color = color;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            color.a = Mathf.Lerp(1f, 0f, timer / fadeDuration);
            fadeImage.color = color;
            yield return null;
        }

        color.a = 0f;
        fadeImage.color = color;
        isFading = false;
    }
}