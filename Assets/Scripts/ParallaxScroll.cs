using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxScroll : MonoBehaviour
{
    [SerializeField]
    private Renderer background;
    [SerializeField]
    private Renderer foreground;
    [SerializeField]
    private float backgroundSpeed = 0.02f;
    [SerializeField]
    private float foregroundSpeed = 0.06f;
    [SerializeField] public float offset = 0.0f;

    
    void Update()
    {
        float backgroundOffset = offset * backgroundSpeed;
        float foregroundOffset = offset * foregroundSpeed;

        background.material.mainTextureOffset = new Vector2(backgroundOffset, 0);
        foreground.material.mainTextureOffset = new Vector2(foregroundOffset, 0);
    }
}