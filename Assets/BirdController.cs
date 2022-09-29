using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdController : MonoBehaviour
{
    public GameObject fly, walk, bee;
    // Start is called before the first frame update
    void Start()
    {
        //el fly solo se activa cuando el stage es egg y el walk cuando fly no esta activado
        fly.SetActive(ControllerScript.stage.Equals("egg"));
        walk.SetActive(!ControllerScript.stage.Equals("egg"));

        // el bee bot solo estara activo cuando el stage sea birth
        bee.SetActive(ControllerScript.stage.Equals("birth"));
   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
