using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]private Text textTimer;
    [SerializeField]private Text textImpactCounter;
    
    
    private float _valTimer = 0f;
    private int _valImpactCounter = 0;
    private string _strDefault ="0";

    
    public static UIManager Instance;

    
    private void OnEnable() => ImpactExplosion.OnImpact += ImpactIncr;
    private void OnDisable() => ImpactExplosion.OnImpact -= ImpactIncr;


    private void Awake()
    {
        if(Instance==null)
            Instance=this as UIManager;
    }

    private void Start()
    {
        StartCoroutine(SimpleTimer());
    }

    
    void Update()
    {
        _valTimer += Time.deltaTime;
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
            yield return new WaitForSeconds(1f);
            textTimer.text = Mathf.FloorToInt(_valTimer).ToString();
        }
    }
}
