using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public GameObject circle;
    public string stage_needed, scene;

    private LevelChanger levelchanger;
    static string stage;
    
    void updateDoors() //activa-desactiva el circulo y los colliders de las puertas
    {
        if (!stage_needed.Equals(ControllerScript.stage))
        {
            foreach (Collider2D c in GetComponents<Collider2D>())
            {
                c.enabled = false;
            }
            circle.SetActive(false);
        }
        else
        {
            foreach (Collider2D c in GetComponents<Collider2D>())
            {
                c.enabled = true;
            }
            circle.SetActive(true);
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        levelchanger = FindObjectOfType<LevelChanger>();
        updateDoors();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            Debug.Log(ControllerScript.stage);
            levelchanger.FadetoLevel(scene);
        }
    }
}
