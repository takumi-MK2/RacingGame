using UnityEngine;

public class GeneCar : MonoBehaviour
{
    public GameObject car;

    void Start()
    {
        Instantiate(car, this.transform);
    }
}