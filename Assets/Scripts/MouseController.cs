using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

public class MouseController : MonoBehaviour
{
    [SerializeField] private float jetpackForce = 75.0f;
    [SerializeField] private float movementSpeed = 3.0f;
    [SerializeField] private Texture2D coinIconTexture;
    [SerializeField] private GroundChecker groundChecker;
    [SerializeField] private ParticleSystem jetpack;
    [SerializeField] private ParallaxScroll parallax;
    [SerializeField] private GameObject deathScreen;
    [SerializeField] private GameObject tweenScreen;
    [SerializeField] private AudioClip coinCollectSound;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private AudioSource jetpackAudio;
    [SerializeField] private AudioSource footstepsAudio;
    
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

    private void Start()
    {
        mainCamera.GetComponent<AudioSource>().volume = GlobalSettings.GetVolume();
        mainCamera.GetComponent<AudioSource>().mute = GlobalSettings.GetMute();
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
        AdjustFootstepsAndJetpackSound(jetpackActive);
        parallax.offset = transform.position.x;
    }
    private void AdjustFootstepsAndJetpackSound(bool jetpackActive)
    {
        var isGround = groundChecker.IsGrounded();
        if (GlobalSettings.GetMute() == false)
        {
            footstepsAudio.volume = GlobalSettings.GetVolume();
        }
        else
        {
            footstepsAudio.mute = GlobalSettings.GetMute();
        }
        
        
        footstepsAudio.enabled = !dead && isGround;
        jetpackAudio.enabled = !dead && !isGround;
        if (jetpackActive && GlobalSettings.GetMute() == false)
        {
            
            jetpackAudio.volume = GlobalSettings.GetVolume();
            
        }else if(jetpackActive && GlobalSettings.GetMute() == true)
        {
            jetpackAudio.mute = GlobalSettings.GetMute();
        }
        else
        {
            jetpackAudio.volume = 0.1f;
        }
    }
    private void DisplayCoinsCount(){
        Rect coinIconRect = new Rect(20, 20, 42, 42);
        GUI.DrawTexture(coinIconRect, coinIconTexture);
        GUIStyle style =new GUIStyle();
        style.fontSize = 60;
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
        if (GlobalSettings.GetMute() == false)
        {
            AudioSource.PlayClipAtPoint(coinCollectSound, transform.position, GlobalSettings.GetVolume());
        }

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
                var laserZap = laserCollider.gameObject.GetComponent<AudioSource>();
                laserZap.volume = GlobalSettings.GetVolume(); 
                laserZap.mute = GlobalSettings.GetMute();
                laserZap.Play();
                dead = true;
                _animator.SetBool("dead", true);
                deathScreen.transform.DOMoveY(tweenScreen.transform.position.y, 1f);
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
