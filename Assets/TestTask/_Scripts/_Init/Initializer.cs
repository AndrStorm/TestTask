using System.Collections.Generic;
using UnityEngine;

public class Initializer : MonoBehaviour
{
    [SerializeField] private HudUI _hudUI;
    [SerializeField] private List<GravityObject> _sceneGravityObjects;
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
        //_gameController.DeInit();
        _gravityController.DeInit();
        _hudUIController.DeInit();
    }

    void Update()
    {
        _hudUIController.OnTick();
    }

    private void FixedUpdate()
    {
        _gravityController.OnFixedTick();
    }


    private void InitGravity()
    {
        _gravityController = new GravityController(_gameSettings.gravitySettings);
        _gravityObjectSpawner = new GravityObjectSpawner
            (_gravityController, _gameSettings.prefab,_gameSettings.spawnPositions);

        foreach (var gravityObject in _sceneGravityObjects)
        {
            _gravityController.AddGravityObject(gravityObject);
        }
        
    }
    
    private void InitGameController()
    {
        _gameController = new GameController
            (_gravityObjectSpawner, _gameSettings);
        _gameController.Init();
    }
    

    private void InitUI()
    {
        _hudUIController = new HudUIController(_hudUI);
        _hudUIController.Init();
    }
    
}
