using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NestScript : MonoBehaviour
{
    public Animator bee_anim;
    public Animator ground_anim;
    public GameObject door;

    private float progress;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B) && progress < 1)
        {
            progress += 0.05f;
            bee_anim.Play("Base Layer.bee_progress", 0, progress);
            ground_anim.Play("Base Layer.ground", 0, progress);
            bee_anim.SetFloat("speed", 0f);
            bee_anim.SetTrigger("dig");
        }

        if(progress > 1)
        {
            ControllerScript.stage = "nest";
            door.SendMessage("updateDoors");
            bee_anim.SetTrigger("exit");
        }


    }
}
