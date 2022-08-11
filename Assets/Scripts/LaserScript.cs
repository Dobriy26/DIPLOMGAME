using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserScript : MonoBehaviour
{
    [SerializeField]
    private Sprite laserOnSprite;
    [SerializeField]
    private Sprite laserOffSprite;
    [SerializeField]
    private float toggleInterval = 0.5f;
    [SerializeField]
    private float rotationSpeed = 0.0f;
    
    private bool isLaserOn = true;
    private float timeUntilNextToggle;
    private Collider2D laserCollider;
    private SpriteRenderer laserRenderer;

    void Start()
    {
        timeUntilNextToggle = toggleInterval;
        laserCollider = GetComponent<Collider2D>();
        laserRenderer = GetComponent<SpriteRenderer>(); 
    }

    void Update()
    {
        timeUntilNextToggle -= Time.deltaTime; 
        if (timeUntilNextToggle <= 0)
        {
            isLaserOn = !isLaserOn;
            laserCollider.enabled = isLaserOn;
            if (isLaserOn)
            {
                laserRenderer.sprite = laserOnSprite;
            }
            else
            {
                laserRenderer.sprite = laserOffSprite;
            }
            timeUntilNextToggle = toggleInterval;
        }
        transform.RotateAround(transform.position, Vector3.forward, rotationSpeed * Time.deltaTime);
    }
}
