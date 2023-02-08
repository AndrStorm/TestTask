
public class GameController
{
    private readonly GravityController _gravityController;
    private readonly GravityObjectSpawner _gravityObjectSpawner;
    private readonly int _gravityObjectsQuantity;

    public GameController(GravityController gravityController, 
        GravityObjectSpawner gravityObjectSpawner, GameSettings gameSettings)
    {
        _gravityController = gravityController;
        _gravityObjectSpawner = gravityObjectSpawner;
        _gravityObjectsQuantity = gameSettings.startSpawnGravityObjectsQuanity;
    }

    public void Init()
    {
        GravityObject.OnAntigravityRequested += OnGravityObjectAntigravityRequested;
        SpawnGravityObjects(_gravityObjectsQuantity);
    }
    
    public void DeInit()
    {
        GravityObject.OnAntigravityRequested -= OnGravityObjectAntigravityRequested;
    }


    private void SpawnGravityObjects(int quantity)
    {
        _gravityObjectSpawner.SpawnGravityObjects(quantity);
    }

    private void OnGravityObjectAntigravityRequested(GravityObject gravityObject, bool isColide)
    {
        if (isColide)
        {
            _gravityController.DeactivateGravity(gravityObject);
        }
        else
        {
            _gravityController.ActivateGravity(gravityObject);
        }
        
    }
    
    
    
    
    
}
