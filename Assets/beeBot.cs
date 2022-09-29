using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class beeBot : MonoBehaviour
{

    private Rigidbody2D rb;

    public GameObject player;
    public Transform bee_body;
    public ParticleSystem trail, hearts;
    public GameObject childCircle, door;
    public float min, max;

    public float offset_movement;

    private float rad;
    private int dir;
    private float nextmove, loveValue;
    private bool love;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rad = 0;
        dir = 1;
        love = false;
        loveValue = 0;
    }

    bool CheckToggleDir()
    {
        if (player == null) return true;

        Vector3 relative_point = transform.InverseTransformPoint(player.transform.position);
        return Mathf.Sign(relative_point.y) != Mathf.Sign(dir);
    }
    // Update is called once per frame
    void Update()
    {
        nextmove -= Time.deltaTime;
        if (nextmove < 0)
        {
            nextmove = Random.Range(min, max);

            if(CheckToggleDir())
            {
                dir = dir * -1;
            }
        }
    }

    private void FixedUpdate()
    {
        rad += offset_movement * dir;

        double y = System.Math.Sin(rad);
        double x = System.Math.Cos(rad);
        float xPos = (float)x * 0.15f;
        float yPos = (float)y * 0.15f;

        rb.position = rb.position + new Vector2(xPos, yPos);
        var main = trail.main;
        main.startRotation = -Mathf.Atan2(yPos, xPos) / 1.04f;

        //salvamos la rotacion del circulo hijo
        Quaternion saveChildRotation = childCircle.transform.rotation;

        float angle = Mathf.Atan2(yPos, xPos) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        //despues de modificar la rotacion de la abeja de debolvemos la rotación al hijo
        //no queremos que el hijo rote como el padre
        childCircle.transform.rotation = saveChildRotation;

        if (love) loveValue += 4;
        else if (loveValue > 0) loveValue -= 0.3f;

        var emision = hearts.emission;
        emision.rateOverTime = loveValue * 4 / 500;

        if (loveValue > 500)
        {
            //update door and stage
            ControllerScript.stage = "male";
            door.SendMessage("updateDoors");

            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            love = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            love = false;
        }
    }
}
