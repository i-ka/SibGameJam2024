using System;
using System.Collections;
using System.Collections.Generic;
using Code.Scripts.Enemy;
using UnityEngine;
using Zenject;

public class LavaMultipedHive : MonoBehaviour
{
    [SerializeField] private int totalMultipedsToSpawn = 8;
    [SerializeField] private LavaMultiped multipedPrefab;
    [SerializeField] private float spawnPeriod;

    private float _spawnTimer = 0;
    private readonly HashSet<LavaMultiped> _spawnedMultipeds = new ();
    private TestPlayer _player;
    private HiveContext _hiveContext;

    [Inject]
    public void Construct(TestPlayer testPlayer)
    {
        _player = testPlayer;
        _hiveContext = new HiveContext
        {
            PatrolRadius = 5,
            HivePosition = transform
        };
    }

    private void Update()
    {
        _spawnTimer += Time.deltaTime;
        if (_spawnTimer >= spawnPeriod)
        {
            SpawnMultiped();
            _spawnTimer = 0;
        }
    }

    public void SpawnMultiped()
    {
        if (_spawnedMultipeds.Count >= totalMultipedsToSpawn)
            return;
        
        var multiped = Instantiate(multipedPrefab, transform.position, Quaternion.identity);
        _spawnedMultipeds.Add(multiped);
        multiped.SetPLayer(_player);
        multiped.SetHive(_hiveContext);
        multiped.OnDestroyed += m => _spawnedMultipeds.Remove(m);
    }
}
