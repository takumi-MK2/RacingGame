using UnityEngine;
using SD;

namespace ROFN
{
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
            SD = FindAnyObjectByType<SaveData>();
            PDM = FindAnyObjectByType<PlayerDataManager>();

            pNum = PDM.playerCount;
            doit = false;
            Debug.Log("うごいてるわよ");
        }

        void LateUpdate()
        {
            DOIT();

        }

        public void DOIT()
        {
            //if (!doit)
            //{
                switch (pNum)
                {
                    case 2:
                        cursor3P.SetActive(false);
                        car3P.SetActive(false);
                        SD.carChoice3P = -999;
                        cursor4P.SetActive(false);
                        car4P.SetActive(false);
                        SD.carChoice4P = -999;
                        Debug.Log($"{SD.carChoice3P},{SD.carChoice4P}");
                        break;
                    case 3:
                        cursor4P.SetActive(false);
                        car4P.SetActive(false);
                        SD.carChoice4P = -999;
                        Debug.Log($"{SD.carChoice4P}");

                        break;
                }

            //    doit = true;
            //}
        }

    }
}