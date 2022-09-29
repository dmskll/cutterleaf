using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class loveScoreController : MonoBehaviour
{
    private string score;
    // Start is called before the first frame update
    void Start()
    {
        score = this.name;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("circle"))
        {
            loveController.Instance.setLoveCircle(int.Parse(collision.name), score, true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag.Equals("circle"))
        {
            loveController.Instance.setLoveCircle(int.Parse(collision.name), score, false);
        }
    }
}
