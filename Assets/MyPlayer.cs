using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class MyPlayer : MonoBehaviour
{
    public GameObject art;
    public GameObject artInside;
    float dt;
    public float movementSpeed = 0.5f;
    public float rotSpeed = 30;
    float artRotation;
    float artScale = 1;
    bool gameOver = false;
    float speedBurst;
    public ParticleSystem burst;
    public UnityEvent onGameOver;
    float scale = 1;
    TrailRenderer trailRenderer;
    public TrailRenderer lifeIndication;
    public Gradient lifeGradient;
    Color insideOriginalColour;
    GameObject cam;
    ScoreCounter score;


    public Material sharedMaterial;
    public Material shaderDustMat;
    public Gradient colourProgressoin;
    public Gradient colourProgressForDust;

    public Seed seed;
    public float colourSeed;
    [Range(0f, 1f)]
    public float showC;
    private void Start()
    {

        seed = GameObject.Find("mapGen").GetComponent<Seed>();
        colourSeed = seed.Signature();

        cam = Camera.main.gameObject;
        artInside = transform.Find("art inside").gameObject;
        insideOriginalColour = artInside.GetComponent<Renderer>().material.color;
        trailRenderer = GetComponent<TrailRenderer>();
        score = GameObject.Find("score").GetComponent<ScoreCounter>();
    }

    void UpdateScale()
    {
        if (scale > 0)
        {
            scale -= 0.02f;
            transform.localScale = new Vector3(0.3f, 0.3f, 0.3f) +  new Vector3(0.7f, 0.7f, 0.7f) * scale ;
            trailRenderer.startWidth = scale;

            lifeIndication.startWidth = 0.2f ;
            lifeIndication.time = ((scale) * 2.5f);
            // lifeIndication.time = ((scale) * 2.5f) + -speedBurst/10; -- code to extend tail on pick up / control burrst speed - need fixing
            lifeIndication.textureScale = new Vector2( (int)((scale + 0.02f) * 49), 1); 

            art.GetComponent<Renderer>().material.color = lifeGradient.Evaluate(scale);
            cam.GetComponent<CameraScript>().SetZPos(scale);// - 1 to 0.3f

        }
        else
        {
            GameOver();
        }
    }

    float blink;
    void Update()
    {

        //progress
       // sharedMaterial.color = colourProgressoin.Evaluate((Mathf.Cos(  transform.position.x/200 + transform.position.y/200  ) +1) / 2);

        
        showC = (Mathf.PerlinNoise(colourSeed + transform.position.x / 400, colourSeed + transform.position.y / 400) * 3) - 1f ;


        sharedMaterial.color = colourProgressoin.Evaluate(showC);
        shaderDustMat.color = colourProgressForDust.Evaluate(showC);



        blink = (Mathf.Cos(Time.time * 10 ) + 1) / 2;
        art.GetComponent<Renderer>().material.color = lifeGradient.Evaluate(scale) * new Vector4(1, 1, 1, Mathf.Clamp ((blink + scale),0,1 ) );
        

        if (Input.GetKeyDown(KeyCode.R)) SceneManager.LoadScene(0);

        if (!gameOver)
        {

            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                rotSpeed = -rotSpeed;
                artRotation = rotSpeed * 4;
                artScale = 1.1f;
                speedBurst = 4.7f;

                UpdateScale();
            }

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                scale = 1;
                UpdateScale();
                score.Add();
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                scale = 0.5f;
                UpdateScale();
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                scale = 0.32f;
                UpdateScale();
            }

            dt = Time.deltaTime;
            transform.Translate(-transform.up * (movementSpeed + speedBurst) * dt);
            transform.Rotate(0, rotSpeed * dt, 0, Space.Self);


            speedBurst -= (speedBurst - 0) / 0.6f * dt;


            artScale -= (artScale - 1) / 0.6f * dt;
            art.transform.localScale = new Vector3(1, 1, 1) * artScale;


            //art feedback
            artRotation -= (artRotation - 0) / 1f * dt;
            art.transform.Rotate(0, artRotation * dt, 0);

        }
    }


    void GameOver()
    {
        gameOver = true;
        onGameOver.Invoke();
        burst.gameObject.transform.parent = null;
        burst.gameObject.SetActive(true);
        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(false);
        transform.GetChild(2).gameObject.SetActive(false);
        transform.GetChild(3).gameObject.SetActive(false);
        GetComponent<TrailRenderer>().time = 0.4f;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "bubble")
        {
            if (!gameOver)
            {
                GameOver();
            }
        }
        if (collision.tag == "collectable")
        {
            //player knows about the collectable here !!!
            collision.gameObject.GetComponent<Collectable>().PickUp();
            scale = 1f;
            UpdateScale();
            score.Add();
        }

    }
}
