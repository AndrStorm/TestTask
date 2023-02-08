using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]private Text textTimer;
    [SerializeField]private Text textImpactCounter;
    
    
    private float _valTimer = 0f;
    private int _valImpactCounter = 0;
    
    private readonly WaitForSeconds secDelay = new WaitForSeconds(1f);
    private const string STR_DEFAULT ="0";

    
    public static UIManager Instance;

    
    private void OnEnable() => ImpactExplosion.OnImpact += CountImpacts;
    private void OnDisable() => ImpactExplosion.OnImpact -= CountImpacts;


    private void Awake()
    {
        if(Instance==null)
            Instance=this;
    }

    private void Start()
    {
        StartCoroutine(SimpleTimer());
    }

    
    public void ResetTimer()
    {
        _valTimer = 0f;
        _valImpactCounter = 0;
        textTimer.text = STR_DEFAULT;
        textImpactCounter.text = STR_DEFAULT;
    }

    private void CountImpacts()
    {
        _valImpactCounter++;
        ShowImpacts();
    }

    private void ShowImpacts()
    {
        textImpactCounter.text = (_valImpactCounter/2).ToString();
    }
    
    IEnumerator SimpleTimer()
    {
        while (true)
        {
            yield return secDelay;
            _valTimer += 1f;
            textTimer.text = Mathf.FloorToInt(_valTimer).ToString();
        }
    }
}
