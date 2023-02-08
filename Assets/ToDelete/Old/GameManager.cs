using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float accelerationMult = 21f;
    [SerializeField] private float decelerationDist = 1f;
    [SerializeField] private Transform target;
    [SerializeField] private Transform[] obstacles;
    public Material cubeColideMat;
    
    
    [NonSerialized]public bool isControlVelocity = true;
    
#if UNITY_EDITOR
    public  bool debugmode = true;
#endif
    
    
    private Rigidbody[] _rb;
    
    
    public static GameManager Instance { get; private set; }

    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        
        _rb = new Rigidbody[obstacles.Length];

        for (int i = 0; i < obstacles.Length; i++)
        {
            _rb[i] = obstacles[i].gameObject.GetComponent<Rigidbody>();
        }
    }

    
    private void FixedUpdate()
    {
        if (!isControlVelocity) return;
       
        
        ApplyGravity();
    }
    

    private void ApplyGravity()
    {
        for (int i = 0; i < obstacles.Length; i++)
        {
            Vector3 forceDir = target.position - obstacles[i].position;
            
            if (forceDir.magnitude >= decelerationDist)
            {
                _rb[i].velocity += accelerationMult * Time.fixedDeltaTime * forceDir.normalized;
            }
            else
            {
                _rb[i].velocity *= Mathf.Clamp01(forceDir.magnitude / decelerationDist);
            }
        }
    }
    

    
    public Transform GetTargetTransform()
    {
        return target;
    }
}
