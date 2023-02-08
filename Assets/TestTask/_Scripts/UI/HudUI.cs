using System;
using UnityEngine;
using UnityEngine.UI;

public class HudUI : MonoBehaviour
{
    [SerializeField]private Text textTimer;
    [SerializeField]private Text textImpactCounter;
    
    private const string STR_DEFAULT ="0";

    public event Action OnResetPressed;
    
    
    public void PressReset()
    {
        OnResetPressed?.Invoke();
    }

    public void SetTimerText(string text)
    {
        textTimer.text = text;
    }
    
    public void SetImpactCounterText(string text)
    {
        textImpactCounter.text = text;
    }

    public void ResetTmerAndCounter()
    {
        textTimer.text = STR_DEFAULT;
        textImpactCounter.text = STR_DEFAULT;
    }

}
