using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using UnityEngine.UI;

public class MyPlayer : MonoBehaviour
{
    public GameObject art;
    public GameObject artInside;
    float dt;
    public float movementSpeed = 0.5f;
    public float rotSpeed = 110;
    float artRotation;
    float artScale = 1;
    bool gameOver = false;
    float speedBurst;
    public ParticleSystem burst;
    public UnityEvent onGameOver;
    float scale = 1;
    TrailRenderer trailRenderer;
    public Gradient lifeGradient;
    Color insideOriginalColour;
    GameObject cam;
    ScoreCounter score;


    public Material sharedMaterial;
    public Material shaderDustMat;
    public Gradient colourProgressoin;
    public Gradient colourProgressForDust;
    public Gradient lifeDotsGradient;
    public Seed seed;
    public float colourSeed;
    [Range(0f, 1f)]
    public float showC;


    public Image inner_UI, outer_UI;

    bool lastStand = false;
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
        if (scale > 0.01f)
        {
            scale -= 0.02f;
            // transform.localScale = new Vector3(0.3f, 0.3f, 0.3f) +  new Vector3(0.7f, 0.7f, 0.7f) * scale ;
            //trailRenderer.startWidth = scale;

            if (scale > 0.5f)
            {
                inner_UI.fillAmount = 1;//inner is full
                outer_UI.fillAmount = (scale * 2) - 1;
                inner_UI.color = lifeDotsGradient.Evaluate(1);
            }
            else
            {
                outer_UI.fillAmount = 0;//--added to fix the bug when it did not disapear when (10%) removed
                inner_UI.fillAmount = (scale * 2) - 0;
                inner_UI.color = lifeDotsGradient.Evaluate(scale * 2);
  
            }

            //lifeIndication.startWidth = 0.2f ;
            //lifeIndication.time = ((scale) * 2.5f);
            // lifeIndication.time = ((scale) * 2.5f) + -speedBurst/10; -- code to extend tail on pick up / control burrst speed - need fixing
            //lifeIndication.textureScale = new Vector2( (int)((scale + 0.02f) * 49), 1); 

            art.GetComponent<Renderer>().material.color = lifeGradient.Evaluate(scale);
            cam.GetComponent<CameraScript>().DecreaseZoom();

        }
        else
        {
            //GameOver(); -- don't kill player!!
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
                if (scale > 0.03f)
                {
                    rotSpeed = -rotSpeed;
                    artRotation = rotSpeed * 4;
                    speedBurst = 4.7f;
                }
                else//last stand!!
                {

                    if(!lastStand) speedBurst = 17.7f;
                    lastStand = true;
                }

                artScale = 1.1f;
                

                UpdateScale();
            }

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                scale = 1;
                UpdateScale();
                score.Add();
                lastStand = false;
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                lastStand = false;
                scale = 0.5f;
                UpdateScale();
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                
                scale = 0.04f;//last stance
                UpdateScale();
            }

            dt = Time.deltaTime;
            // last stand mechanic and regualr movemnt
            if (scale > 0.022f || scale < 0.01) { 
                transform.Translate(-transform.up * (movementSpeed + speedBurst) * dt);
            }else
            {
                Camera.main.GetComponent<CameraScript>().SetZoom(50);

            }

            if(!lastStand)  transform.Rotate(0, rotSpeed * dt, 0, Space.Self);


            speedBurst -= (speedBurst - 0) / 0.6f * dt;


            artScale -= (artScale - 1) / 0.6f * dt;
            art.transform.localScale = new Vector3(1, 1, 1) * artScale;


            //art feedback
            artRotation -= (artRotation - 0) / 1f * dt;
            art.transform.Rotate(0, artRotation * dt, 0);

        }
    }


    void GameOver() // - this needs to be fixed as some points ! too much stuff
    {
        inner_UI.enabled = false;
        outer_UI.enabled = false;
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

            //there is bug with collisoin - if you collide with 2 circles at the same time then you pass through
            // this can be fixed with on collision stay = push player out of the planet
            // or have a little cool down on collision so that you can't collide with 2 things at the same time

            // also maybe if you're on 0.1 health (10%) and you get hit it shouold not be gave over!!!
            //-- put player on last stance instead
            var b =Instantiate(burst,transform.position, Quaternion.identity);
            b.gameObject.SetActive(true);

            transform.Rotate(0, 180, 0);
            speedBurst = 5;
            scale -= 0.1f;
            UpdateScale();
            lastStand = false;
            if (scale < 0.01f) if (!gameOver)  GameOver(); // activate last stand when player has 0 moves
        }
        if (collision.tag == "collectable")
        {
            lastStand = false;
            //player knows about the collectable here !!!
            collision.gameObject.GetComponent<Collectable>().PickUp();
            scale = 1f;
            UpdateScale();
            score.Add();
        }
        

    }
}
