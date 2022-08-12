using UnityEngine;

public class ParticleSortingFixLayers : MonoBehaviour
{
    
    private void Start()
    {
        GetComponent<ParticleSystem>().GetComponent<Renderer>().sortingLayerName = "Player";
        GetComponent<ParticleSystem>().GetComponent<Renderer>().sortingOrder = -1;
    }

}