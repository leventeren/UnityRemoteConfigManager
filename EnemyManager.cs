using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyManager : Singleton<EnemyManager>
{
    
    [Header("Spawn Settings")]
    public Transform[] spawnLocations;

    //Enemy Health
    private int warriorTotalHealth = 100;

    //Spawn Amount
    private int warriorSpawnAmount = 1;

    //Game Loop Settings
    [HideInInspector]public List<EnemyBehaviour> spawnedEnemyBehaviours;



    void Start()
    {
        spawnedEnemyBehaviours = new List<EnemyBehaviour>();
    }



    //Spawning Logic ----

    public void SpawnEnemySet()
    {

        for(int i = 0; i < warriorSpawnAmount; i++)
        {
            GameObject enemy = ObjectPooler.SharedInstance.GetPooledObject("Enemy");


            if(enemy != null)
            {
                enemy.SetActive(true);
                EnemyBehaviour enemyBehaviour = enemy.GetComponent<EnemyBehaviour>();
                enemyBehaviour.EnemyNowAlive(spawnLocations[0], warriorTotalHealth);
                spawnedEnemyBehaviours.Add(enemyBehaviour);
            }
        }
       
    }



    //Enemy Collision Logic ----

    public void EnemyHitByProjectile(GameObject enemyHit, int damageToApplyToEnemy)
    {
        enemyHit.GetComponent<EnemyBehaviour>().RemoveHealth(damageToApplyToEnemy);
    }

    public void RemoveEnemy(EnemyBehaviour enemyBehaviourToRemove)
    {
        spawnedEnemyBehaviours.Remove(enemyBehaviourToRemove);
    }

    public void SetRemoteConfigurationToSettings(int newSpawnAmount, int newTotalHealth)
    {
        warriorTotalHealth = newTotalHealth;
        warriorSpawnAmount = newSpawnAmount;

    }
    
}
