using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seed : MonoBehaviour
{


    int seed = 1;

    public int  Next(float _hashKey, int _range)
    {
        System.Random rand = new System.Random((int)_hashKey + seed); // Using a fixed seed
        return(  rand.Next(_range)  );
     
    }


}
