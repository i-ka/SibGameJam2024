using System;
using System.Collections;
using System.Collections.Generic;
using Code.Scripts;
using Code.Scripts.Enemy;
using UnityEngine;
using Zenject;

public class LavaMultipedHive : MonoBehaviour
{
    [SerializeField] private int totalMultipedsToSpawn = 8;
    [SerializeField] private LavaMultiped multipedPrefab;
    [SerializeField] private float spawnPeriod;
    [SerializeField] private float patrolRadius = 10;

    private float _spawnTimer = 0;
    private readonly HashSet<LavaMultiped> _spawnedMultipeds = new ();
    private TestPlayer _player;
    private GameStateController _gameStateController;
    private HiveContext _hiveContext;
    private DiContainer _container;
    private bool dying = false;

    [Inject]
    public void Construct(TestPlayer testPlayer, GameStateController gameStateController, DiContainer container)
    {
        _player = testPlayer;
        _gameStateController = gameStateController;
        _hiveContext = new HiveContext
        {
            PatrolRadius = patrolRadius,
            HivePosition = transform
        };
        _container = container;
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
        if (dying) return;
        if (_spawnedMultipeds.Count >= totalMultipedsToSpawn)
            return;
        
        var multiped = _container.InstantiatePrefabForComponent<LavaMultiped>(multipedPrefab, transform.position, Quaternion.identity, null);
        _spawnedMultipeds.Add(multiped);
        multiped.SetPLayer(_player);
        multiped.SetHive(_hiveContext);
        multiped.OnDestroyed += m => _spawnedMultipeds.Remove(m);
    }

    public void OnDead()
    {
        _gameStateController.HiveDestroy();
        StartCoroutine(DeadSequence());
    }
    
    private IEnumerator DeadSequence()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
