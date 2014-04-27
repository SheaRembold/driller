using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    public int index;
    KeyCode[][] keys = { new KeyCode[]{ KeyCode.A, KeyCode.D, KeyCode.S, KeyCode.W }, new KeyCode[]{ KeyCode.LeftArrow, KeyCode.RightArrow, KeyCode.DownArrow, KeyCode.UpArrow } };
    float rot = 0;
    float speed = 0;
    public float accel;
    public float brake;
    public float turn;
    public float maxSpeed;
    private bool _hasDrill;
    public bool hasDrill
    {
        get
        {
            return _hasDrill;
        }
        set
        {
            _hasDrill = value;
            drill.SetActive(_hasDrill);
        }
    }
    public GameObject drill;

    void Start()
    {
    }

    void Update()
    {
        if (Input.GetKey(keys[index][0]))
        {
            rot += turn * Time.deltaTime;
            if (rot > 360)
                rot -= 360;
        }
        if (Input.GetKey(keys[index][1]))
        {
            rot -= turn * Time.deltaTime;
            if (rot < 0)
                rot += 360;
        }
        if (Input.GetKey(keys[index][2]))
        {
            speed -= brake * Time.deltaTime;
            if (speed < 0)
                speed = 0;
        }
        if (Input.GetKey(keys[index][3]))
        {
            speed += accel * Time.deltaTime;
            if (speed > maxSpeed)
                speed = maxSpeed;
        }

        transform.rotation = Quaternion.Euler(0, 0, rot);
        transform.position += transform.up * speed * Time.deltaTime;
        rigidbody.velocity = Vector3.zero;
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Block" && hasDrill)
        {
            GameObject.Destroy(col.gameObject);
        }
    }
}
