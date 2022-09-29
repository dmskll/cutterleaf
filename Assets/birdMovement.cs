using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class birdMovement : MonoBehaviour
{
    public float speed, min, max;
    private float cd_dir;
    private int dir;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        dir = Random.Range(0, 2) == 0 ? 1 : -1;
        rb = GetComponent<Rigidbody2D>();
    }

    void changeDir()
    {
        dir *= -1;
        transform.localScale = new Vector3(dir, transform.localScale.y, transform.localScale.z);
        cd_dir = Random.Range(min, max);
    }

    // Update is called once per frame
    void Update()
    {
        if( cd_dir > 0)
        {
            cd_dir -= Time.deltaTime;
        }
        else
        {
            changeDir();
        }
        
    }

    private void FixedUpdate()
    {
        rb.position += new Vector2(speed * dir, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision) 
    {
        if (collision.tag.Equals("border"))
        {
            changeDir();
        }
    }
}
