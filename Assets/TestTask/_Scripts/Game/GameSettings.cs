using System;
using System.Collections.Generic;


[Serializable]
public class GameSettings
{
    public GravityObject prefab;
    public int startSpawnGravityObjectsQuanity = 2;
    public List<SpawnPosition> spawnPositions;
    public GravitySettings gravitySettings;
    
    
}
