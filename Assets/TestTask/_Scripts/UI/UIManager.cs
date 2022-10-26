using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]private Text textTimer;
    [SerializeField]private Text textImpactCounter;
    
    
    private float _valTimer = 0f;
    private int _valImpactCounter = 0;
    private const string _strDefault ="0";
    private readonly WaitForSeconds secDelay = new WaitForSeconds(1f);

    
    public static UIManager Instance;

    
    private void OnEnable() => ImpactExplosion.OnImpact += ImpactIncr;
    private void OnDisable() => ImpactExplosion.OnImpact -= ImpactIncr;


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
        textTimer.text = _strDefault;
        textImpactCounter.text = _strDefault;
    }

    public void ImpactIncr()
    {
        _valImpactCounter++;
        ShowImpacts();
    }

    public void ShowImpacts()
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
