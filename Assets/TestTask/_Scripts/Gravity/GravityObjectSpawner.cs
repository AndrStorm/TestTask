using System.Collections.Generic;
using UnityEngine;

public class GravityObjectSpawner
{
    private readonly GravityObject _prefab;
    private readonly List<SpawnPosition> _spawnPositions;
    private readonly GravityController _gravityController;

    private int _objCounter;

    public GravityObjectSpawner(GravityController gravityController,
        GravityObject prefab, List<SpawnPosition> spawnPositions)
    {
        _gravityController = gravityController;
        _prefab = prefab;
        _spawnPositions = spawnPositions;
        _objCounter = 0;
    }

    public void SpawnGravityObjects(int quantity)
    {
        for (int i = 1; i <= quantity; i++)
        {
            if (_spawnPositions.Count < i) return;
            
            _objCounter++;
            var position = ChooseSpawnPosition();
            var obj = Object.Instantiate(_prefab, position, Quaternion.identity);
            obj.gameObject.name += _objCounter;
            _gravityController.AddGravityObject(obj);
        }
        
    }

    private Vector3 ChooseSpawnPosition()
    {
        if (_objCounter < 0) return Vector3.zero;
        int spawnerIndex = (_objCounter - 1) % _spawnPositions.Count;
        return _spawnPositions[spawnerIndex].transform.position;
    }
}
