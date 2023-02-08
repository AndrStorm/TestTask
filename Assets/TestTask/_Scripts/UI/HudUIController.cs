using UnityEngine;

public class HudUIController
{
    private readonly HudUI _hudUI;

    private const float _timerUpdateDelay = 1f;
    private float _delayTimer;
    private float _timer;
    private int _impactsCounter;


    public HudUIController(HudUI hudUI)
    {
        _hudUI = hudUI;
    }
    
    public void Init()
    {
        GravityObject.OnStructureColided += OnStructureColided;
        _hudUI.OnResetPressed += ResetTimerAndImpactCounter;
    }
    
    public void DeInit()
    {
        GravityObject.OnStructureColided -= OnStructureColided;
        _hudUI.OnResetPressed -= ResetTimerAndImpactCounter;
    }

    public void OnTick()
    {
        CalculateTime();
    }


    private void CalculateTime()
    {
        _timer += Time.deltaTime;
        _delayTimer += Time.deltaTime;

        if (_delayTimer >= _timerUpdateDelay)
        {
            _delayTimer = 0;
            SetTimer(Mathf.FloorToInt(_timer).ToString());
        }
    }

    
    
    private void OnStructureColided()
    {
        _impactsCounter += 1;
        SetImpactCounter((_impactsCounter / 2).ToString());
    }
    
    private void ResetTimerAndImpactCounter()
    {
        _timer = 0f;
        _delayTimer = 0f;
        _impactsCounter = 0;
        _hudUI.ResetTmerAndCounter();
    }
    
    
    private void SetTimer(string text)
    {
        _hudUI.SetTimerText(text);
    }
    
    private void SetImpactCounter(string text)
    {
        _hudUI.SetImpactCounterText(text);
    }

    
}
