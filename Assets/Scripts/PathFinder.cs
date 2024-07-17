using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    EnemySpawner enemySpawner;
    WavesConfigSO wavesConfig;
    List<Transform> waypoints;
    int waypopintsIndex = 0;

    void Awake()
    {
       enemySpawner = FindObjectOfType<EnemySpawner>();  
    }
    void Start()
    {
        wavesConfig = enemySpawner.GetCurrentWave();
        waypoints = wavesConfig.GetWaypoints();
        transform.position = waypoints[waypopintsIndex].position;
          
    }

    void Update()
    {
        FollowPath();
    }

    private void FollowPath()
    {
        if(waypopintsIndex < waypoints.Count)
        {
            Vector3 targetPosition = waypoints[waypopintsIndex].position;
            float delta = wavesConfig.GetMoveSpeed() * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, delta);
            if(transform.position == targetPosition)
            {
                waypopintsIndex++;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
