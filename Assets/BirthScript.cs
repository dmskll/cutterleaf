using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirthScript : MonoBehaviour
{
    public GameObject bee, chr, door;

    private Animator chr_anim, bee_anim;
    private int meneos;
    // Start is called before the first frame update
    void Start()
    {
        meneos = 0;
        chr_anim = chr.GetComponent<Animator>();
        bee_anim = bee.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            chr_anim.SetTrigger("meneo");
            meneos++;
            if(meneos > 5 && meneos != -1)
            {
                meneos = -1;
                bee_anim.SetBool("birth", true);
                chr_anim.SetTrigger("birth");

                //update door and stage
                ControllerScript.stage = "birth";
                door.SendMessage("updateDoors");
            }
        }
    }
}
