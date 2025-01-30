using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seed : MonoBehaviour
{


    public int seed = 12;

    public int  Next(float _hashKey, int _range)
    {
        System.Random rand = new System.Random((int)_hashKey + seed); // Using a fixed seed
        return(  rand.Next(_range)  );
     
    }

    public float Signature()
    {
        System.Random rand = new System.Random( seed ); // Using a fixed seed
        return (float)rand.Next(100) / 100;
        
    }


}
