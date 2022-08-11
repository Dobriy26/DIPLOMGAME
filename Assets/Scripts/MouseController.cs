using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour
{
    [SerializeField]
    private float jetpackForce = 75.0f;
    [SerializeField]
    private float movementSpeed = 3.0f;
    [SerializeField]
    private Texture2D coinIconTexture;
    [SerializeField] 
    private GroundChecker groundChecker;
    [SerializeField]
    private ParticleSystem jetpack;
    [SerializeField]
    private ParallaxScroll parallax;
    
    private Rigidbody2D _rigidbody2D;
    private bool dead = false;
    private uint coins = 0;
    private bool isHit = false;
    private Animator _animator;
    
    
    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        groundChecker = GetComponentInChildren<GroundChecker>();
        _animator = GetComponent<Animator>();
       
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        bool jetpackActive = Input.GetButton("Fire1");
        jetpackActive = jetpackActive && !dead;
        if (jetpackActive)
        {
            _rigidbody2D.AddForce(new Vector2(0,jetpackForce));
        }

        if (!dead)
        {
            Vector2 newVelocity = GetComponent<Rigidbody2D>().velocity;
            newVelocity.x = movementSpeed;
            GetComponent<Rigidbody2D>().velocity = newVelocity;
        }
        UpdateGroundedStatus();
        AdjustJetpack(jetpackActive);
        parallax.offset = transform.position.x;
    }
    
    private void DisplayCoinsCount(){
        Rect coinIconRect = new Rect(10, 10, 32, 32);
        GUI.DrawTexture(coinIconRect, coinIconTexture);
        GUIStyle style =new GUIStyle();
        style.fontSize = 30;
        style.fontStyle = FontStyle.Bold;
        style.normal.textColor = Color.yellow;

        Rect labelRect = new Rect(coinIconRect.xMax, coinIconRect.y, 0, 32);
        GUI.Label(labelRect, coins.ToString(), style);
    }

    private void OnGUI(){
        DisplayCoinsCount();
    }

    private void CollectCoin(Collider2D coinCollider)
    {
        coins++;

        Destroy(coinCollider.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Coins"))
        {
            CollectCoin(collider);
        }
        else
        {
            HitByLaser(collider);
        }
    }
    
    private void HitByLaser(Collider2D laserCollider)
    {
        if (!dead)
        {
            if (!isHit) {
                isHit = true;
                dead = true;
                _animator.SetBool("dead", true);
            }
            if (isHit)
            {
                Invoke("Cancel", 1f);
            }
        }
    }
    private void UpdateGroundedStatus(){
        var isGround = groundChecker.IsGrounded();
        _animator.SetBool("grounded", isGround);
    }
    private void Cancel() {
        isHit=false;
    }
    
    private void AdjustJetpack(bool jetpackActive){
        ParticleSystem.EmissionModule jpEmission = jetpack.emission;
        var isGround = groundChecker.IsGrounded();
        jpEmission.enabled = !isGround;
        jpEmission.rateOverTime = jetpackActive ? 300.0f : 75.0f;
    }
}
