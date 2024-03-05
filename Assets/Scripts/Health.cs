using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] bool isPlayer;
    [SerializeField] int health = 50;
    [SerializeField] int score = 50;

    [SerializeField] ParticleSystem hit_effect; // particle system object from unity

    [SerializeField] bool applyCameraShake;
    camerashake cameraShake;
    AudioPlayer audioPlayer;
    ScoreKeeper scoreKeeper;
    LevelManager levelManager;

    void Awake()
    {
        cameraShake = Camera.main.GetComponent<camerashake>();
        audioPlayer = FindObjectOfType<AudioPlayer>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        levelManager = FindObjectOfType<LevelManager>();
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damage_dealer = other.GetComponent<DamageDealer>();

        if (damage_dealer != null) // if there still health then it can take more hits 
        {
            // The amount of damage taken has to be passed
            TakeDamage(damage_dealer.GetDamage());//here we need to take the damage and tell the damage dealer that we hit something
            PlayHitEffect();
            audioPlayer.PlayDamageClip();
            ShakeCamera();
            damage_dealer.Hit();
        }
    }
    public int GetHealth()
    {
        return health;
    }


    void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die(); ;// once the health is zero the game object should be destroyed, run out the health
        }
    }
    void PlayHitEffect()
    {
        if (hit_effect != null)
        {
            ParticleSystem instance = Instantiate(hit_effect, transform.position, Quaternion.identity); // quarntine identity : no rotation
            Destroy(instance.gameObject, instance.main.duration + instance.main.startLifetime.constantMax);
        }

    }
    void ShakeCamera()
    {
        if (cameraShake != null && applyCameraShake)
        {
            cameraShake.Play();
        }
    }
    void Die()
    {
        if (!isPlayer)
        {
            scoreKeeper.ModifyScore(score);
        }
        else
        {
            levelManager.LoadGameOver();
        }

        Destroy(gameObject);
    }



}
