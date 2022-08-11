using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSortingLayerFix : MonoBehaviour
{
    
    private void Start()
    {
		GetComponent<ParticleSystem>().GetComponent<Renderer>().sortingLayerName = "Player";
		GetComponent<ParticleSystem>().GetComponent<Renderer>().sortingOrder = -1;
    }

}
