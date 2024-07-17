using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthScript : MonoBehaviour
{
    [SerializeField] bool isPlayer;
    [SerializeField] int health = 50;
    [SerializeField] int pointsToAdd = 100;
    [SerializeField] ParticleSystem hitEffect;

    [SerializeField] bool applyCameraShake;
    CameraShake cameraShake;

    AudioPlayer audioPlayer;

    ScoreKeeper scoreKeeper;

    LevelManger manager;

    void Awake()
    {
        cameraShake = Camera.main.GetComponent<CameraShake>();
        audioPlayer = FindObjectOfType<AudioPlayer>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        manager = FindObjectOfType<LevelManger>();
        
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.GetComponent<DamageDealer>();
        if (damageDealer != null)
        {
            TakeDamage(damageDealer.GetDamage());
            PlayHitEffect();
            ShakeCamera();
            damageDealer.Hit();
        }
    }

    void ShakeCamera()
    {
       if(cameraShake != null && applyCameraShake)
       {
            cameraShake.Play();
       }
    }

    void TakeDamage(int damage)
    {
        
        health -= damage;
        if (health <= 0)
        {
            Die();

        }
    }

    void Die()
    {
        audioPlayer.PlayExplosionClip();
        if (!isPlayer)
        {
            scoreKeeper.AddScore(pointsToAdd);
        }
        if (isPlayer)
        {
                       
            manager.LoadGameOver();
        }
        Destroy(gameObject);
    }

    void PlayHitEffect()
    {
        if(hitEffect != null)
        {
            ParticleSystem instance = Instantiate(hitEffect, transform.position, Quaternion.identity );
            Destroy(instance.gameObject, instance.main.duration + instance.main.startLifetime.constantMax);
        }
    }

    public int GetHealth()
    {
        return health;
    }
}
