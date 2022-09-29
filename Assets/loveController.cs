using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class loveController : MonoBehaviour
{
    public static loveController Instance { get; private set; }
    public void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    public struct loveCirclesState{
        public bool ok, perfect;
    }

    public loveCirclesState[] loveCircles; 

    public GameObject female, male;
    private Animator anim_female, anim_male;
    // Start is called before the first frame update
    void Start()
    {
        loveCircles = new loveCirclesState[10];
        anim_female = female.GetComponent<Animator>();
        anim_male = female.GetComponent<Animator>();

    }



    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.B))
        {
            anim_female.SetBool("buzzing", true);
            //anim_female.SetFloat("offset", Random.Range(0, 10) / 10.0f);
        }
        else
        {
            anim_female.SetBool("buzzing", false);
        }
    }

    public void setLoveCircle(int i, string score, bool b)
    {
        if(score.Equals("ok"))
        {
            loveCircles[i].ok = b;
            Debug.Log("ok: " + b);
        }
        else if(score.Equals("perfect"))
        {
            loveCircles[i].perfect = b;
            Debug.Log("perfect: " + b);
        }
    }
}
