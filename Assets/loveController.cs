using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class loveController : MonoBehaviour
{
    public static loveController Instance { get; private set; }
    public struct loveCirclesState
    {
        public GameObject reference;
        public bool ok, perfect, dead;
    }
    public List<loveCirclesState> loveCircles = new List<loveCirclesState>();
    int love_circles_start = 0;
    int score = 0;
    float cd_spawn;
    
    public GameObject female, male;
    public GameObject circle_spawn1, circle_spawn2, circle;
    private Animator anim_female, anim_male;

    public void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        anim_female = female.GetComponent<Animator>();
        anim_male = female.GetComponent<Animator>();

    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.C))
            createLoveCircle();

        if (Input.GetKeyDown(KeyCode.B))
        {
            Debug.Log(score);
            anim_female.SetBool("buzzing", true);
            //anim_female.SetFloat("offset", Random.Range(0, 10) / 10.0f);
            if(!checkCircles()) //que pasa si no hay ningun circulo
            {
                score -= 25;
                //crear interrogante
            }
        }
        else
        {
            anim_female.SetBool("buzzing", false);
        }
    }
    private void FixedUpdate()
    {
        if (cd_spawn < 0)
        {
            createLoveCircle();
            if(score < 70)
            {
                cd_spawn = Random.Range(25, 45) / 10.0f;
            }
            else if(score < 130)
            {
                cd_spawn = Random.Range(8, 25) / 10.0f;
            }
            else if(score < 250)
            {
                cd_spawn = Random.Range(5, 15) / 10.0f;
            }
        }
        else
            cd_spawn -= Time.deltaTime;
       
    }
    public void setLoveCircle(int i, string score, bool b)
    {
        if(score.Equals("ok"))
        {
            var love_circle = loveCircles[i];
            love_circle.ok = b;
            loveCircles[i] = love_circle;
           // Debug.Log(i + "-> ok: " + b);
            if (!b && !loveCircles[i].dead)
            {
                loveCircles[i].reference.SetActive(false);
                this.score -= 20;
            }
        }
        else if(score.Equals("perfect"))
        {
            var love_circle = loveCircles[i];
            love_circle.perfect = b;
            loveCircles[i] = love_circle;
            //Debug.Log(i + "-> perfect: " + b);
        }
    }

    //modificamos el valor de inicio para recorrer la lista loveCircles, así no hace falta iterar circulos muertos
    public void updateLoveCircleStart(int x)
    {
        for (int i = x; i >= love_circles_start; i--)
        {
            if (!loveCircles[i].dead)
                return;
        }
        love_circles_start = x;
    }
    public bool checkCircles()
    {
 
        for(int i = love_circles_start; i < loveCircles.Count; i++)
        {
            if(!loveCircles[i].dead)
            {
                if(loveCircles[i].ok)
                {
                    
                    var aux = loveCircles[i];
                    aux.dead = true;
                    loveCircles[i] = aux;
                    updateLoveCircleStart(i);
       
                    if (loveCircles[i].perfect)
                    {
                        score += 15;
                    }
                    else
                    {
                        score += 5;
                    }

                    loveCircles[i].reference.SetActive(false);
                    return true;
                }
            }
        }
        return false;
    }
    public void createLoveCircle()
    {
        loveCirclesState aux;
        aux.ok = false;
        aux.perfect = false;
        aux.dead = false;
        if(Random.Range(0,1) == 0)
            aux.reference = Instantiate(circle, circle_spawn1.transform.position, Quaternion.identity);
        else
            aux.reference = Instantiate(circle, circle_spawn2.transform.position, Quaternion.identity);
        aux.reference.SetActive(true);

        aux.reference.name = "" + loveCircles.Count; //int to string 
        circleGenerator aux_script = aux.reference.GetComponent<circleGenerator>();
        aux_script.objective = transform;
        if (score < 70)
        {
            aux_script.speed = Random.Range(2, 3) / 10.0f;
        }
        else if (score < 130)
        {
            aux_script.speed = Random.Range(40, 50) / 100.0f;
        }
        else if (score < 250)
        {
            aux_script.speed = Random.Range(60, 80) / 100.0f;
        }
        
        aux_script.radium = 2;
        aux_script.scale_x = 0.78f;
        aux_script.scale_y = 0.78f;
        aux_script.color1 = new Color(1, 1, 1, 255);

        loveCircles.Add(aux);
    }
}
