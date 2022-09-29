using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eggScript : MonoBehaviour
{
    public Transform top;
    public Transform bot;
    public Transform bee_trans;
    public Animator bee_anim, chr_anim;
    float bee_position;

    public float power_up, power_down, min_time, max_time, min_speed, max_speed;
    public float step_up0, step_down0;
    private float step_up, step_down, speed, cd_change, progressValue;

    public GameObject bee, huevo, nido, door;
    public float step;

    private bool bee_top, bee_bot, obj_top, obj_bot, key, progress, win;
    private int dir;
    private Rigidbody2D bee_rb, obj_rb;
    // Start is called before the first frame update
    void Start()
    {
        bee_rb = bee.GetComponent<Rigidbody2D>();
        obj_rb = GetComponent<Rigidbody2D>();
        dir = Random.Range(0, 2) == 0 ? 1 : -1;
        cd_change = -1;
        progress = false;
        win = false;
        huevo.SetActive(false);
    }
    void setBeeTop(bool b)
    {
        bee_top = b;
    }

    void setBeeBot(bool b)
    {
        bee_bot = b;
    }

    void beeUpdate()
    {
        if (Input.GetKey(KeyCode.B) && !bee_top)
        {
            key = true;

        }
        else if (!bee_bot)
        {
            key = false;
        }
    }


    void beeFixedUpdate()
    {
        if (key)
        {
            if (step_down != step_down0)
            {
                step_down = step_down0;
            }
            bee_rb.position += new Vector2(0, step_up);
            step_up += power_up;
        }
        else if (!bee_bot)
        {
            if (step_up0 != step_up)
            {
                step_up = step_up0;
            }

            bee_rb.position -= new Vector2(0, step_down);
            step_down += power_down;
        }
    }

    void progressUpdate()
    {
        if(progress)
        {
            progressValue += Time.deltaTime * 1.2f; //1.2
        }
        else
        {
            if (progressValue > 0) progressValue -= Time.deltaTime * 2.2f; //baja mas rapido de lo que sube
            else progressValue = 0; //si es mas pequeno que 0 se lia el animator
        }
        if (progressValue > 10)
        {
            win = true;
        }
        bee_anim.Play("bee.bee_nest_wings", 0, progressValue / 10);
        bee_anim.SetFloat("speed", 0f);

        Debug.Log(progressValue);
    }
    // Update is called once per frame
    void Update()
    {
        
        if (!win)
        {
            beeUpdate();
            progressUpdate();
        }
        else
        {
            //bee_anim.Play("bee.bee_nest_wings", 0, progressValue / 10);
            bee_anim.SetTrigger("exit");
            chr_anim.SetTrigger("build");
            huevo.SetActive(true);

            //update door and stage
            ControllerScript.stage = "egg";
            door.SendMessage("updateDoors");
        }
        if(cd_change < 0)
        {
            speed = Random.Range(min_speed, max_speed);
            cd_change = Random.Range(min_time, max_time);
            dir *= -1;
        }
        else
        {
            cd_change -= Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        if (!win)
        {
            beeFixedUpdate();
            if (obj_top) dir = -1;
            if (obj_bot) dir = 1;
            obj_rb.position += new Vector2(0, speed) * dir;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name.Equals("top"))
        {
            obj_top = true;
        }
        else if (collision.name.Equals("bot"))
        {
            obj_bot = true;
        }
        else if (collision.name.Equals("puntero"))
        {
            progress = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name.Equals("top"))
        {
            obj_top = false;
        }
        else if (collision.name.Equals("bot"))
        {
            obj_bot = false;
        }
        else if (collision.name.Equals("puntero"))
        {
            progress = false;
        }
    }
}
