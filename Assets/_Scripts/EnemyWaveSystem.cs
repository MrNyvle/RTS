using System;
using System.Collections;
using System.Collections.Generic;
using _ScriptableObjects.VillageUnit;
using _Scripts;
using _Scripts.Enemies;
using _Scripts.Unit;
using NaughtyAttributes;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyWaveSystem : _Scripts.Singleton<EnemyWaveSystem>
{
    [SerializeField] Enemy enemyGO;
    [SerializeField] int initialWaveSize;
    [CurveRange(1,500,1,500, EColor.Blue)]
    [SerializeField] AnimationCurve waveSizeIncrease;
    [CurveRange(1,500,1,500, EColor.Blue)]
    [SerializeField] AnimationCurve TEMP;
    [SerializeField] float waveSpawnInterval;

    [SerializeField, ReorderableList] List<Transform> spawnPoints = new List<Transform>();

    private List<Enemy> enemies = new List<Enemy>();
    private bool waveInProgress = false;

    private TimeSpan _DebugTimeADD = TimeSpan.Zero;

    [Button]
    public void AddTime()
    {
        _DebugTimeADD = TimeSpan.FromMinutes(50);
    }
    
    [Button]
    public void SpawnWave()
    {
        if (waveInProgress) return;
        waveInProgress = true;
        TimeSpan currentGameTime = GameManager.Instance.GetGameTime() + _DebugTimeADD;
        
        int waveSize = initialWaveSize + (int)waveSizeIncrease.Evaluate(currentGameTime.Minutes);

        Vector3 spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Count)].position;
        
        for (int i = 0; i < waveSize; i++)
        {
           enemies.Add(MakeRandomEnemy(GetRandomArchetype(), spawnPoint));
        }
    }
    
    private Enemy MakeRandomEnemy(ECombatArchetype archetype, Vector3 spawnPoint)
    {
        Enemy instance = Instantiate(enemyGO, spawnPoint, Quaternion.identity);
        instance.unitStats.eCombatType = archetype;
        instance.unitStats.RecalculateCombatStats();
        
        return instance;
    }

    private ECombatArchetype GetRandomArchetype()
    {
        return (ECombatArchetype)Random.Range(0, Enum.GetValues(typeof(ECombatArchetype)).Length - 1);
    }


    public void OnUnitDeath(Enemy enemy)
    {
        enemies.Remove(enemy);
        Destroy(enemy.gameObject);
        
        if (enemies.Count == 0)
        {
            waveInProgress = false;
            StartCoroutine(WaitToSpawn());
        }
    }

    IEnumerator WaitToSpawn()
    {
        yield return new WaitForSecondsRealtime(waveSpawnInterval);
        SpawnWave();
    }
}
