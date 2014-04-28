using UnityEngine;
using System.Collections;

public class Spin : MonoBehaviour
{
    public float speed;
    float rot;

    void Start()
    {
        rot = transform.localRotation.eulerAngles.z;
    }

    void Update()
    {
        rot += speed * Time.deltaTime;
        transform.localRotation = Quaternion.Euler(0, 0, rot);
    }
}
