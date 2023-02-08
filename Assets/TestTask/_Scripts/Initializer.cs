using UnityEngine;

public class Initializer : MonoBehaviour
{
    [SerializeField] private HudUI _hudUI;
    [SerializeField] private GameSettings _gameSettings;


    private HudUIController _hudUIController;
    private GameController _gameController;
    private GravityController _gravityController;
    private GravityObjectSpawner _gravityObjectSpawner;
    
    
    void Start()
    {
        InitGravity();
        InitGameController();
        InitUI();
    }

    private void OnDestroy()
    {
        _gameController.DeInit();
        _hudUIController.DeInit();
    }

    void Update()
    {
        _gravityController.OnTick();
        _hudUIController.OnTick();
    }


    private void InitGravity()
    {
        _gravityController = new GravityController(_gameSettings.gravitySettings);
        _gravityObjectSpawner = new GravityObjectSpawner
            (_gravityController, _gameSettings.prefab,_gameSettings.spawnPositions);
    }
    
    private void InitGameController()
    {
        _gameController = new GameController
            (_gravityController, _gravityObjectSpawner, _gameSettings);
        _gameController.Init();
    }
    

    private void InitUI()
    {
        _hudUIController = new HudUIController(_hudUI);
        _hudUIController.Init();
    }
    
}
