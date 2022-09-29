using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


public class BirdFly : MonoBehaviour
{
    public float speed;
    public CinemachineVirtualCamera cam;
    private LevelChanger levelchanger;
    public GameObject bee;
    public Transform bee_tr, end_tr;
    private Vector3 diff;
    bool ate;

    Transform objective;
    // Start is called before the first frame update
    void Start()
    {
        levelchanger = FindObjectOfType<LevelChanger>();
        ate = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (ate)
        {
            objective = end_tr;
        }
        else
        {
            objective = bee_tr;
        }
        diff = objective.position - transform.position;
        diff.Normalize();
        float rot = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rot);

        transform.position = Vector2.MoveTowards(transform.position, objective.position, speed);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            ate = true;
            Destroy(bee);
            cam.m_Follow = transform;
            levelchanger.FadetoLevel("end");
        }
    }
}
