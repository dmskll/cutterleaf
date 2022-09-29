using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class beeMovement : MonoBehaviour
{
    public SpriteRenderer hoja;
    private Rigidbody2D rb;
    public Transform bee_body;
    public ParticleSystem trail;
    public Animator anim;
    public Transform birth, nest, polen, leaf, egg;

    public float offset_range;
    
    private float rad, nextdir, offset_movement, stamina;
    private bool tired;
    private int dir;
    
    void stageChanges()
    {
        hoja.enabled = false;
        string stage = ControllerScript.stage;
        if (stage.Equals("birth"))
        {
            transform.position = new Vector3(birth.position.x, birth.position.y, transform.position.z);
        }
        else if (stage.Equals("nest"))
        {
            transform.position = new Vector3(nest.position.x, nest.position.y, transform.position.z);
        }
        else if (stage.Equals("polen"))
        {
            transform.position = new Vector3(polen.position.x, polen.position.y, transform.position.z);
        }
        else if (stage.Equals("leaf"))
        {
            hoja.enabled = true;
            transform.position = new Vector3(leaf.position.x, leaf.position.y, transform.position.z);
        }
        else if (stage.Equals("egg"))
        {
            transform.position = new Vector3(egg.position.x, egg.position.y, transform.position.z);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        stageChanges();
        
        rb = GetComponent<Rigidbody2D>();
        tired = false;
        
        rad = 0;
        dir = 1;
        offset_movement = offset_range;
    }

    // Update is called once per frame
    void Update()
    {
        nextdir -= Time.deltaTime;
        if (stamina > 0) stamina -= Time.deltaTime * 18;
        else
        {
            tired = false;
            anim.SetBool("tired", tired);
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            if(nextdir < 0)
            {
                nextdir = 0.4f;
                if (!tired) stamina += 10;
                if (stamina > 100)
                {
                    tired = true;
                    anim.SetBool("tired", tired);
                }
                dir = dir * -1;
                offset_movement = Random.Range(offset_range, offset_range * 1.5f);
            }
            else if (nextdir < 2)
            {
                if (!tired) stamina += 20;
                dir = dir * -1;
                offset_movement = Random.Range(offset_range, offset_range * 1.5f);
            }
            else if (!tired) stamina += 40;
        }

    }
    
    private void FixedUpdate()
    {
       // float h = Input.GetAxis("Horizontal");

        rad += offset_movement * dir;

        double y = System.Math.Sin(rad);
        double x = System.Math.Cos(rad);
        float xPos = (float) x * 0.15f;
        float yPos = (float) y * 0.15f;        

        if (tired) rb.position = rb.position + new Vector2(xPos/2 , yPos/2);
        else rb.position = rb.position + new Vector2(xPos, yPos);

        var main = trail.main;
        main.startRotation = -Mathf.Atan2(yPos, xPos) / 1.04f;
        
        float angle = Mathf.Atan2(yPos, xPos) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        anim.SetFloat("Stamina", stamina);
    }
}
