using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [Header("General")]
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileLifeTime = 5f;
    [SerializeField] float fireRate = 1f;

    [Header("AI")]
    [SerializeField] bool useAI;
    [SerializeField] float fireTimeVarience = 0f;
    [SerializeField] float minimumFireTime = 0.2f;

    float randomShooting;

    [HideInInspector] public bool isFiring;

    Coroutine firingCoroutine;

    AudioPlayer audioPlayer;

    void Awake()
    {
        audioPlayer = FindObjectOfType<AudioPlayer>();
    }
    void Start()
    {
        if(useAI)
        {
            isFiring = true;
            randomShooting = Random.Range(fireRate - fireTimeVarience,
                                    fireRate + fireTimeVarience); 
            randomShooting = Mathf.Clamp(randomShooting, minimumFireTime, float.MaxValue);
        }
        else
        {
            randomShooting = fireRate;
        }
    }

    
    void Update()
    {
        Fire();
    }

    void Fire()
    {
        if (isFiring && firingCoroutine == null)
        {
            firingCoroutine = StartCoroutine(FireContinuously());

        }
        else if(!isFiring && firingCoroutine != null)
        {
            StopCoroutine(firingCoroutine);
            firingCoroutine = null;
        }
    }

    IEnumerator FireContinuously()
    {
        while(true)
        {   
            
            GameObject instance = Instantiate(projectilePrefab, 
                transform.position, 
                Quaternion.identity);

            Rigidbody2D projectileBody = instance.GetComponent<Rigidbody2D>();
            if (projectileBody != null)
            {
                projectileBody.velocity = transform.up * projectileSpeed;
            }         
            Destroy(instance, projectileLifeTime);
            audioPlayer.PlayShootingClip();
            yield return new WaitForSeconds(randomShooting);
        }
        
    }


}
