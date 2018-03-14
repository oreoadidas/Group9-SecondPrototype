using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortingLayer : MonoBehaviour {

    public string sortingLayer;
    public int orderInLayer;

    void Start()
    {
        //GetComponent<ParticleSystem>().sortingLayerName = sortingLayer;
        //GetComponent<Renderer>().sortingOrder = orderInLayer;
    }
}
