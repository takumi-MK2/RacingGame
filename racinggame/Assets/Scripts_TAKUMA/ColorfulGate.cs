using UnityEngine;

public class ColorfulGate : MonoBehaviour
{
    Color32[] oa = new Color32[11];
    public Light rai;
    //public Light gateLight1,gateLight2,gateLight3
    int i = 0;

    float delt = 0;

    void Start()
    {
        oa[0] = Color.red;
        oa[1] = Color.orange;
        oa[2] = Color.yellow;
        oa[3] = Color.lightGreen;
        oa[4] = Color.green;
        oa[5] = Color.limeGreen;
        oa[6] = Color.lightBlue;
        oa[7] = Color.blue;
        oa[8] = Color.purple;
        oa[9] = Color.magenta;
        oa[10] = Color.pink;
    }

    void Update()
    {
        delt += Time.deltaTime;

        if (delt >= 0.1)
        {
            rai.color = oa[i];

            if (i >= 10) i = 0;
            else i++;

            delt -= 0.1f;
        }
    }
}