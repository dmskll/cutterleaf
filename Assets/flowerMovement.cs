using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flowerMovement : MonoBehaviour
{
    public GameObject flower, circle_go, door;
    public Animator flower_anim;
    public circleGenerator circ_gen;
    public GameObject bee;
    public float speed;

    private int dir, stage;
    private float angle, cd_polen;
    private bool circle, polen, intro, end;
    private Rigidbody2D rb;
    private Animator anim;
    
    void setStage()
    {
        if (stage == 0)
        {
            flower.SendMessage("setMove", false);
        }
        else if (stage == 1)
        {
            flower.SendMessage("setMove", true);
            flower.SendMessage("setSpeed", 1.5f);
        
        }
        else if (stage == 2)
        {
            flower.SendMessage("setSpeed", 2.5f);
        }
        else if (stage == 3)
        {
            flower.SendMessage("setSpeed", 5f);
            flower.SendMessage("setMovRad", false);
            flower.SendMessage("setRadium", 3f);
        }
        else if (stage == 4)
        {
            flower.SendMessage("setRadium", 3f);
            flower.SendMessage("setSpeed", 0.1f);
            flower.SendMessage("setMovRad", true);
            flower.SendMessage("setMinRadium", 2f );
            flower.SendMessage("setMaxRadium", 10f);
            flower.SendMessage("setSpeedRadium", 0.2f);
        }
        else if (stage == 5)
        {
            flower.SendMessage("setRadium", 3f);
            flower.SendMessage("setSpeed", 0.6f);
            flower.SendMessage("setMovRad", true);
            flower.SendMessage("setMinRadium", 2f);
            flower.SendMessage("setMaxRadium", 10f);
            flower.SendMessage("setSpeedRadium", 0.15f);
        }
        else if (stage == 6)
        {
            flower.SendMessage("setRadium", 3f);
            flower.SendMessage("setSpeed", 0.7f);
            flower.SendMessage("setMovRad", true);
            flower.SendMessage("setMinRadium", -7f);
            flower.SendMessage("setMaxRadium", 7f);
            flower.SendMessage("setSpeedRadium", 0.15f);
        }
        else if (stage == 7)
        {
            flower.SendMessage("setRadium", 3f);
            flower.SendMessage("setSpeed", 1.5f);
            flower.SendMessage("setMovRad", true);
            flower.SendMessage("setMinRadium", -3f);
            flower.SendMessage("setMaxRadium", 7f);
            flower.SendMessage("setSpeedRadium", 0.15f);

        }
        else if (stage == 8)
        {
            flower.SendMessage("setRadium", 3f);
            flower.SendMessage("setSpeed", 0.01f);
            flower.SendMessage("setMovRad", true);
            flower.SendMessage("setMinRadium", -7f);
            flower.SendMessage("setMaxRadium", 7f);
            flower.SendMessage("setSpeedRadium", 0.2f);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        circle = false;
        polen = false;
        intro = true;
        end = false;

        angle = 0;
        stage = 0; 
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        anim.SetBool("intro", true);
        dir = 1;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B) && !polen && !intro && !end)
        {
            dir *= -1;
            Debug.Log(stage);
            if (circle)
            {
                anim.SetBool("polen", true);
                flower_anim.SetBool("polen", true);

                cd_polen = 1.2f;
                polen = true;

                if (stage < 8)
                {
                    circ_gen.changeColor(stage, 2);
                    stage++;
                    setStage();
                    flower.SendMessage("setMove", false);
                }
                else
                {
                    end = true;
                    circle_go.SetActive(false);
                }


            }
            else
            {
                if (stage > 0)
                {
                    circ_gen.changeColor(stage - 1, 1);
                    stage--;
                }
                setStage();
            }

        }
        else if (polen)
        {
            cd_polen -= Time.deltaTime;
            if (cd_polen < 0)
            {
                if (!end) flower.SendMessage("recalculatePositionCircle");
                flower_anim.SetBool("polen", false);
                anim.SetBool("polen", false);
                flower.SendMessage("setMove", true);
                polen = false;
                
            }
        }
        else if (intro)
        {
            cd_polen -= Time.deltaTime;
        }
            
        
    }
    private void FixedUpdate()
    {
        if (intro)
        {
            transform.position += new Vector3(0, 0.2f, 0);
            if (transform.position.y > 0.9)
            {
                anim.SetBool("intro", false);
                intro = false;
                flower.SendMessage("recalculatePositionCircle");
            }
        }
        if(!polen && !intro && !end)
        {
            angle += speed * dir;
            rb.MoveRotation(angle);
        }
        if(end && !polen)
        {
            transform.position += new Vector3(0, 0.25f, 0);
            anim.SetBool("intro", true);
            ControllerScript.stage = "polen";
            door.SendMessage("updateDoors");

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name.Equals("circulo"))
        {
            circle = true;
            Debug.Log(circle);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.name.Equals("circulo"))
        {
            circle = false;
        }
    }
}
