using UnityEngine;
using System.Collections;

public class Stretch : MonoBehaviour
{
    void Start()
    {

    }

    void Update()
    {
        Vector3 scale = transform.localScale;
        scale.x = 2f * Screen.width / Screen.height;
        transform.localScale = scale;
    }
}