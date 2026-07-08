using UnityEngine;

public class ColorfulGate : MonoBehaviour
{
    Color32[] oa = new Color32[7];
    public Light rai;
    public Light gateLight1, gateLight2, gateLight3, gateLight4, gateLight5, gateLight6;
    int i = 0;

    float delt = 0;

    void Start()
    {
        oa[0] = Color.red;
        oa[1] = Color.orange;
        oa[2] = Color.yellow;
        oa[3] = Color.green;
        oa[4] = Color.blue;
        oa[5] = Color.purple;
        oa[6] = Color.magenta;
    }

    void Update()
    {
        delt += Time.deltaTime;

        if (delt >= 0.1)
        {
            rai.color = oa[i];

            if (i >= 6) i = 0;
            else i++;

            delt -= 0.1f;
        }

        gateLight1.color = oa[i];
        gateLight4.color = gateLight1.color;

        if (i == 6)
        {
            gateLight2.color = oa[0];
            gateLight5.color = gateLight2.color;

            gateLight3.color = oa[1];
            gateLight6.color = gateLight3.color;
        }
        else if (i == 5)
        {
            gateLight2.color = oa[i+1];
            gateLight5.color = gateLight2.color;

            gateLight3.color = oa[0];
            gateLight6.color = gateLight3.color;
        }
        else
        {
            gateLight2.color = oa[i+1];
            gateLight5.color = gateLight2.color;

            gateLight3.color = oa[i+2];
            gateLight6.color = gateLight3.color;
        }


    }
}