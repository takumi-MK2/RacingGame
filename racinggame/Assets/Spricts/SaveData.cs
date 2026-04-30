using UnityEngine;

namespace SD
{
    public class SaveData : MonoBehaviour
    {
        public int playNum;
        public int carChoice1P, carChoice2P, carChoice3P, carChoice4P;

        void Start()
        {
            carChoice1P = 0;
            carChoice2P = 0;
            carChoice3P = 0;
            carChoice4P = 0;
        }

    }
}
