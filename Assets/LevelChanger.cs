using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelChanger : MonoBehaviour
{
    public Animator anim;
    public string scene_to_load;
    public void FadetoLevel(string scene)
    {
        scene_to_load = scene;
        anim.SetTrigger("fadeOut");
        Debug.Log("AHHHHHHH");
    }

    public void OnLevelComplete()
    {
        SceneManager.LoadScene(scene_to_load);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
