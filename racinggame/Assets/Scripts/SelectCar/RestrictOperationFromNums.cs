using UnityEngine;
using SD;

public class RestrictOperationFromNums : MonoBehaviour
{
    int pNum;
    bool doit;

    [SerializeField] SaveData SD;
    [SerializeField] PlayerDataManager PDM;

    public GameObject cursor3P, cursor4P,
                      car3P, car4P;


    void Start()
    {
        pNum = PDM.playerCount;
        doit = false;
    }

    void Update()
    {
        if (!doit)
        {
            switch (pNum)
            {
                case 2: 
                    cursor3P.SetActive(false);
                    car3P.SetActive(false);
                    cursor4P.SetActive(false);
                    car4P.SetActive(false);
                    break;
                case 3:
                    cursor4P.SetActive(false);
                    car4P.SetActive(false);
                    break;
            }

            doit = true;
        }


    }
}