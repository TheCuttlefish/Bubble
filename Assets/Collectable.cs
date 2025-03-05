using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    float dt;
    float xRot, yRot, zRot;
    bool pickedUp = false;
    public AnimationCurve sizeCurve;
    Transform player;
    float moveToPlayerSpeed = 1;
    public GameObject art;
    public Gradient colours;
    public ParticleSystem burst;

    public List<TrailRenderer> trails = new List<TrailRenderer>();
    bool waitBeforeDestroy = false;

    [SerializeField] CollectableAmbient ambientPlayer; // to play ambient idle loop
    
    void Start()
    {
        art = transform.Find("art").gameObject;
        player = GameObject.Find("player").transform;
        

        xRot = Random.Range(-10f, 10f);
        yRot = Random.Range(-10f, 10f);
        zRot = Random.Range(-10f, 10f);
        transform.localEulerAngles = new Vector3(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360));
        transform.localScale = new Vector3(1, 1, 1) * 0.5f;
        
    }


    void Update()
    {
        dt = Time.deltaTime;
        transform.Rotate(xRot * dt, yRot * dt, zRot * dt);


        if (pickedUp)
        {
     
            //GetComponent<Renderer>().enabled = false;
            transform.position -= (transform.position - player.position) / moveToPlayerSpeed * dt;


            
            if (moveToPlayerSpeed > 0.01)
            {
                moveToPlayerSpeed -= Time.deltaTime * 0.8f;
                transform.localScale = new Vector3 (1,1,1) * sizeCurve.Evaluate(moveToPlayerSpeed  ) * 2;
            }else
            {


                if (!waitBeforeDestroy)
                {
                    waitBeforeDestroy = true;
                    Destroy(gameObject, 0.1f);
                    foreach (var t in trails) t.time = 0.1f;
                }
            }

        }

    }


    public void PickUp()
    {
        if (!pickedUp)
        {
            pickedUp = true;
            burst.gameObject.SetActive(true);
            art.GetComponent<Renderer>().enabled = false;
            ambientPlayer.StopAmbient();  // stopping idle loop when picked up           
        }
    }
    


}
