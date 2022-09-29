using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scriptIndicador : MonoBehaviour
{
    public GameObject dots_1, dots_2, dots_3, door;
    public Animator indicator_anim;
    
    public float velocity, range_orange, range_yellow, range_green;
    public GameObject orange_go, yellow_go, green_go, bee, small_leaf;
    
    private Animator bee_anim;
    private float start_p, start_pw, x, cd_move, intro;
    private int dir, progress, state;
    private Rigidbody2D rb, orange_rb, yellow_rb, green_rb, bee_rb;
    private bool green, yellow, orange, red, move, end;
    // Start is called before the first frame update
    void Start()
    {
        dots_1.SetActive(false);
        dots_2.SetActive(false);
        dots_3.SetActive(false);



        Debug.Log(transform.localPosition.x);
        dir = 1;
        progress = 0;
        rb = GetComponent<Rigidbody2D>();
        start_p = transform.localPosition.x;
        start_pw = transform.position.x;
        green = false;
        yellow = false;
        orange = false;
        red = true;
        move = false;
        end = false;
        cd_move = 5f;

        bee_anim = bee.GetComponent<Animator>();
        bee_rb = bee.GetComponent<Rigidbody2D>();
        green_rb = green_go.GetComponent<Rigidbody2D>();
        yellow_rb = yellow_go.GetComponent<Rigidbody2D>();
        orange_rb = orange_go.GetComponent<Rigidbody2D>();

        bee_anim.SetBool("start", true);
        intro = 7f;
    }

    void recalculatePosition()
    {
        yellow_rb.transform.localPosition = new Vector2(Random.Range(range_yellow, -range_yellow), 0);
        green_rb.transform.localPosition = new Vector2(Random.Range(range_green, -range_green), 0);
        orange_rb.transform.localPosition = new Vector2(Random.Range(range_orange, -range_orange), 0);
    }
    
    void changeState()
    {
        if (progress > 40 && state < 1)
        {
            bee_anim.SetBool("1to2", true);
            dots_1.SetActive(true);
            state = 1;
        }
        else if (progress > 80 && state < 2)
        {
            dots_2.SetActive(true);
            state = 2;
        }
        else if (progress > 130 && state < 3)
        {
            dots_3.SetActive(true);
            end = true;
        }
        bee_anim.SetBool("cut", true);
    }

    void sumProgress()
    {
        if (green) progress += 30;
        else if (yellow) progress += 15;
        else if (orange) progress += 5;
        else if (red) progress += 1;
    }
    // Update is called once per frame
    void Update()
    {
        if(!move && intro < 0)
        {
            if(cd_move < 0)
            {
                move = true;
                bee_anim.SetBool("cut", false);
                bee_anim.SetBool("1to2", false);
                rb.position = new Vector2(start_pw, rb.position.y);
                dir = 1;
                

                if(end)
                {
                    move = false;
                    bee_anim.SetBool("3toEnd", true);
                    cd_move = 0.15f;

                    //update door and stage
                    ControllerScript.stage = "leaf";
                    door.SendMessage("updateDoors");
                }
                else
                {
                    recalculatePosition();
                }
            }
            else
            {
                cd_move -= Time.deltaTime;
            }
        }
        if (end)
        {
            if(cd_move < 0)
            {
                small_leaf.transform.parent = bee.transform;
                small_leaf.GetComponent<SpriteRenderer>().sortingLayerName = "bee2";
            }
            else
            {
                cd_move -= Time.deltaTime;
            }
        }
        if (move && Input.GetKeyDown(KeyCode.B))
        {
            move = false;
            cd_move = 1f;
            sumProgress();
            changeState();
        }

        if (intro > 0) intro -= Time.deltaTime;
        else if (intro > -10) 
        {
            bee_anim.SetBool("start", false);
            indicator_anim.SetBool("start", false);
            move = true;
            intro = -10;
        }

        x = transform.localPosition.x;
        if (dir > 0 && -start_p < x || dir < 0 && start_p > x)
        {
            dir *= -1;
        }
    }
    private void FixedUpdate()
    {
        if (move)
        {
            rb.position = rb.position + new Vector2(velocity * dir, 0);
        }
        /*
        if(rotate)
        {
            bee.transform.rotation = Quaternion.AngleAxis(10, Vector3.forward);
        }
        bee.transform.rotation = Quaternion.AngleAxis(10, Vector3.forward); */
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("green"))
        {
            green = true;
            velocity += 0.1f;
        }
        else if (collision.gameObject.name.Equals("yellow"))
        {
            yellow = true;
            velocity += 0.1f;
        }
        else if (collision.gameObject.name.Equals("orange"))
        {
            orange = true;
            velocity += 0.06f;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("green"))
        {
            green = false;
            velocity -= 0.1f;
        }
        else if (collision.gameObject.name.Equals("yellow"))
        {
            yellow = false;
            velocity -= 0.1f;
        }
        else if (collision.gameObject.name.Equals("orange"))
        {
            orange = false;
            velocity -= 0.06f;
        }
    }
}
