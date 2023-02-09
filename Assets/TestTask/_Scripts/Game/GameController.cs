
public class GameController
{
    private readonly GravityObjectSpawner _gravityObjectSpawner;
    private readonly int _gravityObjectsQuantity;

    public GameController(GravityObjectSpawner gravityObjectSpawner, GameSettings gameSettings)
    {
        _gravityObjectSpawner = gravityObjectSpawner;
        _gravityObjectsQuantity = gameSettings.GravityObjectsStartSpawn;
    }

    public void Init()
    {
        SpawnGravityObjects(_gravityObjectsQuantity);
    }
    
    
    private void SpawnGravityObjects(int quantity)
    {
        _gravityObjectSpawner.SpawnGravityObjects(quantity);
    }

    
    
    
    
    
    
}
