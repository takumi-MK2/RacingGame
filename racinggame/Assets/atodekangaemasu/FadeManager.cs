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
                    CreateFadeInstance();
                }
            }
            return instance;
        }
    }

    // ゲーム開始時（どのシーンから始めても）自動で FadeManager を生成する
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Initialize()
    {
        if (instance == null)
        {
            CreateFadeInstance();
        }
    }

    private static void CreateFadeInstance()
    {
        GameObject managerObj = new GameObject("FadeManagerSystem");
        instance = managerObj.AddComponent<FadeManager>();
        instance.CreateFadeUI(managerObj);
        DontDestroyOnLoad(managerObj);
    }

    [Header("フェード用の画像")]
    [SerializeField] private Image fadeImage;

    [Header("フェードにかかる時間（秒）")]
    [SerializeField] private float fadeDuration = 0.5f;

    private bool isFading = false;

    // 全画面UIを正しく生成する処理
    private void CreateFadeUI(GameObject parent)
    {
        Canvas canvas = parent.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.sortingOrder = 32767; // UIの一番最前面に表示

        // 💡 画面サイズに合わせて可変で全画面に広げる設定
        CanvasScaler scaler = parent.AddComponent<CanvasScaler>();
        scaler.uiScaleMode = CanvasScaler.ScaleMode.ConstantPixelSize;

        parent.AddComponent<GraphicRaycaster>();

        GameObject imageObj = new GameObject("FadeImage");
        imageObj.transform.SetParent(parent.transform, false);

        fadeImage = imageObj.AddComponent<Image>();
        fadeImage.color = new Color(0, 0, 0, 0); // 初期状態は透明
        fadeImage.raycastTarget = false; // クリックを邪魔しないようにする（必要に応じて）

        // 💡 画面全体に強制ピッタリ張り合わせる（全方向アンカー伸長）
        RectTransform rect = fadeImage.GetComponent<RectTransform>();
        rect.anchorMin = Vector2.zero; // (0, 0) 左下
        rect.anchorMax = Vector2.one;  // (1, 1) 右上
        rect.pivot = new Vector2(0.5f, 0.5f);
        rect.offsetMin = Vector2.zero; // 余白0
        rect.offsetMax = Vector2.zero; // 余白0
        rect.localScale = Vector3.one;  // スケールを1に強制リセット
    }

    private void Awake()
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

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        StartCoroutine(FadeInRoutine());
    }

    public void ChangeScene(string sceneName)
    {
        if (isFading) return;
        StartCoroutine(FadeOutAndLoadScene(sceneName));
    }

    private IEnumerator FadeOutAndLoadScene(string sceneName)
    {
        isFading = true;
        float timer = 0f;

        if (fadeImage != null)
        {
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
        }

        SceneManager.LoadScene(sceneName);
    }

    private IEnumerator FadeInRoutine()
    {
        isFading = true;
        float timer = 0f;

        if (fadeImage != null)
        {
            Color color = fadeImage.color;
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
        }

        isFading = false;
    }
}