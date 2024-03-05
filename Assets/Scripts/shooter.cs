using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UIElements.UxmlAttributeDescription;

public class shooter : MonoBehaviour
{
    [Header("General")]
    [SerializeField] GameObject projectile_prefab; // hold the prefab objetc
    [SerializeField] float projectile_speed = 10f; // how speed the laser goes
    [SerializeField] float projectile_lifetime = 5f; // time that the projectile appears after being released
    [SerializeField] float base_firing_rate = 0.2f; // how speed the enemy shooting

    [Header("AI")]
    [SerializeField] bool use_aI; // here in player shooting is not automated. but for the enemy here is the case. if useai is true then is firing is also true.
    [SerializeField] float firing_rate_variance = 0f; // firing is done so random by we can add this value to the base fring rate
    [SerializeField] float minimum_firingRate = 0.1f;

    [HideInInspector] public bool isFiring;
    

    Coroutine firing_coroutine; // control the starting and stopping of this routine with a fire method
    AudioPlayer audioPlayer;

    void Awake()
    {
        audioPlayer = FindObjectOfType<AudioPlayer>();
    }


    void Start()
    {
        if (use_aI)
        {
            isFiring = true; // if use ai is true then firing happens automatically for enemys. but for player useai is flase and only true when we press space
        }

    }

    void Update()
    {
        Fire();
    }

    void Fire()
    {
       
        // if our player script is telling that we are firing currently then we need to start the couroutine 
        // if our player script is telling us that we are not firing curently then we need to stop the couroutine
        if (isFiring && firing_coroutine == null)
        {
            firing_coroutine = StartCoroutine(FireContinuously());
        }
        else if (!isFiring && firing_coroutine != null)
        {
            StopCoroutine(firing_coroutine);
            firing_coroutine = null;
        }
    }

    IEnumerator FireContinuously()
    {
        // here i am instantiate a projectile in every loop
        //destroy the projectile after the projectile life time ended
        
        while (true) // we want infinite loop
        {
            GameObject instance = Instantiate(projectile_prefab,
                                            transform.position,
                                            Quaternion.identity);  //creating the object constantly when we press space 

            //giving velocity to the fire object we create in the up direction
            Rigidbody2D rigidbody = instance.GetComponent<Rigidbody2D>();
            if (rigidbody != null)
            {
                rigidbody.velocity = transform.up * projectile_speed; // UP : shooting in the direction of green arrow
            }

            Destroy(instance, projectile_lifetime); // destroy the instance after 0.5s after it hitting with another rigid
            float timeToNextProjectile = Random.Range(base_firing_rate - firing_rate_variance,
                                            base_firing_rate + firing_rate_variance);
            timeToNextProjectile = Mathf.Clamp(timeToNextProjectile, minimum_firingRate, float.MaxValue);
            audioPlayer.PlayShootingClip();
            yield return new WaitForSeconds(timeToNextProjectile);

        }
    }

}
