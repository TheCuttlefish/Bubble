using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class CameraScript : MonoBehaviour
{
    bool zoomOut = false;
    float dt;
    float zPos = -15;
    float playerZoom = 35;


    public void SetZoom(float _z)
    {
        playerZoom = _z;
    }

    public void DecreaseZoom() // this needs to be fixed! dones't do anything!
    {
        if (playerZoom > 20) playerZoom -= 10;
    }

    public void ZoomOut()
    {
        
        zoomOut = true;
        GetComponent<PositionConstraint>().enabled = false;
    }
    private void Start()
    {
       // transform.localEulerAngles = new Vector3 (0f, 0f, Random.Range(0,360) );    
    }
    void Update()
    {

        dt = Time.deltaTime;

        if (zoomOut) // on game over
        {

            zPos -= (zPos - -50) / 2 * dt;
            transform.Rotate(0, 0, 2*dt);
            transform.position = new Vector3(transform.position.x,transform.position.y, zPos);

        }else // in game
        {
            playerZoom -= (playerZoom - 35) / 0.2f * dt;//easing zoom to 40
            zPos -= (zPos - ( -playerZoom )) / 3f * dt;//easing applying it on camZ - 3 slow , 0,2 = fast
            transform.position = new Vector3(transform.position.x, transform.position.y, zPos);
        }
    }
}
