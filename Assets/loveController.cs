using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class loveController : MonoBehaviour
{
    public GameObject female, male;
    private Animator anim_female, anim_male;
    // Start is called before the first frame update
    void Start()
    {
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
}
