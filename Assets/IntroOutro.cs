using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroOutro : MonoBehaviour
{
    public string scene;
    private LevelChanger levelchanger;
    // Start is called before the first frame update
    void Start()
    {
        levelchanger = FindObjectOfType<LevelChanger>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.B))
        {
            Debug.Log("B");
            levelchanger.FadetoLevel(scene);
        }
    }
}
