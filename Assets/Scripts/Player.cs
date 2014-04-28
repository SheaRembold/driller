using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    public int index;
    float rot = 0;
    float speed = 0;
    public float accel;
    public float brake;
    public float turn;
    float maxSpeed;
    public float maxSpeedChase;
    public float maxSpeedDrill;
    public float headStart;
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
            if (_hasDrill)
                maxSpeed = maxSpeedDrill;
            else
                maxSpeed = maxSpeedChase;
        }
    }
    public GameObject drill;
    public GUIStuff stuff;
    public MapManager map;

    bool canMove;
    float untilMove;

    Vector3 startPos;

    void Start()
    {
        hasDrill = index == 0;
        startPos = transform.position;

        reset();
    }

    public void reset()
    {
        speed = 0;
        rot = 0;
        transform.position = startPos;
        transform.rotation = Quaternion.Euler(0, 0, rot);

        canMove = hasDrill;
        untilMove = headStart;
        if (!canMove)
            stuff.startWait(index);
    }

    void Update()
    {
        if (canMove)
        {
            if (Input.GetAxis("Horizontal" + index) < 0)
            {
                rot += turn * Time.deltaTime;
                if (rot > 360)
                    rot -= 360;
            }
            else if (Input.GetAxis("Horizontal" + index) > 0)
            {
                rot -= turn * Time.deltaTime;
                if (rot < 0)
                    rot += 360;
            }
            if (Input.GetAxis("Vertical" + index) < 0)
            {
                speed -= brake * Time.deltaTime;
                if (speed < 0)
                    speed = 0;
            }
            else if (Input.GetAxis("Vertical" + index) > 0)
            {
                speed += accel * Time.deltaTime;
                if (speed > maxSpeed)
                    speed = maxSpeed;
            }

            transform.rotation = Quaternion.Euler(0, 0, rot);
            transform.position += transform.up * speed * Time.deltaTime;
            rigidbody.velocity = Vector3.zero;
            rigidbody.angularVelocity = Vector3.zero;
        }
        else
        {
            untilMove -= Time.deltaTime;
            if (untilMove <= 0)
            {
                canMove = true;
                stuff.endWait();
            }
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Block" && hasDrill)
        {
            map.Pool.Despawn(col.gameObject.transform);
        }
        else if (col.gameObject.tag == "Player" && !hasDrill)
        {
            stuff.Winner(index);
        }
    }
}
