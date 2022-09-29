using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class circleGenerator : MonoBehaviour
{
    public Color color1, color2;
    public float radium, scale_x, scale_y;
    public int num;
    public GameObject parts;

    private GameObject[] elements;

    public void changeColor(int i, int color)
    {
        if(color == 1)
        {
            elements[i].GetComponent<SpriteRenderer>().color = color1;
        }
        else
        {
            elements[i].GetComponent<SpriteRenderer>().color = color2;
        }

    }
    // Start is called before the first frame update
    void Start()
    {
        elements = new GameObject[num];
        float x, y;
        float ang = 360 / num;
        for (int i = 0; i < num; i++)
        {

            y = Mathf.Sin(i * ang * Mathf.Deg2Rad) * radium;
            x = Mathf.Cos(i * ang * Mathf.Deg2Rad) * radium;
            var newpart1 = Instantiate(parts, new Vector2(this.transform.position.x + x, this.transform.position.y + y), Quaternion.identity);
            float angle = Mathf.Atan2(y, x) * Mathf.Rad2Deg + 90;

            newpart1.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            newpart1.GetComponent<SpriteRenderer>().color = color1;
            newpart1.transform.parent = gameObject.transform;
            newpart1.transform.localScale = new Vector3(scale_x, scale_y, newpart1.transform.localScale.z);

            elements[i] = newpart1;

            



        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        this.transform.Rotate(new Vector3(0, 0, 0.2f));
    }
}
