using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Fadeout : MonoBehaviour
{
    [Header("表示時間")]
    public float dispTime;
    [Header("フェードアウトにかける時間")]
    public float fadeTime;
    [Header("フェードアウトさせるオブジェクト")]
    public GameObject bg;
    public Image bgImg;
    public GameObject text;
    public Text textTxt;
    public GameObject img;
    public Image imgImg;

    public float timer = 0;

    void Start()
    {
        bg.SetActive(true);
        text.SetActive(true);
        img.SetActive(true);
    }

    void Update()
    {        timer += Time.deltaTime;

        if (timer >= dispTime+fadeTime)
        {
            bg.SetActive(false);
            text.SetActive(false);
            img.SetActive(false);
        }

        if (timer >= dispTime)
        {
            //Color32 alpha = new Color32(255, 255, 255, (byte)(1 - (timer-dispTime)/fadeTime));

            //bgImg.color = alpha;
            //textTxt.color = alpha;
            //imgImg.color = alpha;

            bgImg.DOFade(0, fadeTime);
            textTxt.DOFade(0, fadeTime);
            imgImg.DOFade(0, fadeTime);



        }
    }





}