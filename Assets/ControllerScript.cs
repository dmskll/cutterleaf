using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ControllerScript : MonoBehaviour
{
    public static string stage;
    public string start_stage;
    // Start is called before the first frame update
    void Start()
    {
        
    }


    public void setStage(string s)
    {
        stage = s;
    }
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        stage = start_stage;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            stage = "none";
            SceneManager.LoadScene("birth");
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            stage = "birth";
            SceneManager.LoadScene("garden");
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            stage = "none";
            SceneManager.LoadScene("birth");
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            stage = "none";
            SceneManager.LoadScene("birth");
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            stage = "none";
            SceneManager.LoadScene("birth");
        }
    }
}
