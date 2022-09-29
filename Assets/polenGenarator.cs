using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class polenGenarator : MonoBehaviour
{
   
    public GameObject circle, bee;
    public float radium, speed, min_rad, max_rad, speed_rad;
    public bool move_radium;


    private Rigidbody2D circle_rb;
    private float x, y, angle;
    private int dir, dir_radium;
    private bool move;
    
    void setMove(bool m)
    {
        move = m;
    }

    void setSpeed(float s)
    {
        speed = s;
    }

    void setMovRad(bool r)
    {
        move_radium = r;
    }
    void setMinRadium (float min)
    {
        min_rad = min;
    }

    void setRadium (float r)
    {
        radium = r;
    }
    void setMaxRadium( float max)
    {
        max_rad = max;
    }
    void setSpeedRadium(float sp)
    {
        speed_rad = sp;
    }


    void recalculatePositionCircle()
    {
        angle = Random.Range(0, 360);
        y = Mathf.Sin(angle * Mathf.Deg2Rad) * radium;
        x = Mathf.Cos(angle * Mathf.Deg2Rad) * radium;
        dir *= -1;
        circle.transform.position = new Vector3(x + bee.transform.position.x, y + bee.transform.position.y, circle.transform.position.z);
    }


    // Start is called before the first frame update
    void Start()
    {
        dir = 1;
        dir_radium = -1;
        move = false;
        circle_rb = circle.GetComponent<Rigidbody2D>();
        recalculatePositionCircle();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            //recalculatePositionCircle();
        }

    }

    private void FixedUpdate()
    {
        if(move)
        {
            angle += speed * dir;
            
            y = Mathf.Sin(angle * Mathf.Deg2Rad) * radium;
            x = Mathf.Cos(angle * Mathf.Deg2Rad) * radium;

            circle.transform.position = new Vector3(x + bee.transform.position.x, y + bee.transform.position.y, circle.transform.position.z);
        }
        if(move_radium)
        {
            radium += speed_rad * dir_radium;
            if (radium > max_rad || radium < min_rad) dir_radium *= -1;
        }
    }

}
