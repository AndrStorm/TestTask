using System;
using System.Collections.Generic;


[Serializable]
public class GameSettings
{
    public GravityObject prefab;
    public int GravityObjectsStartSpawn = 2;
    public List<SpawnPosition> spawnPositions;
    public GravitySettings gravitySettings;
    
    
}
