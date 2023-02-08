using System.Collections;
using UnityEngine;
using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class GravityObject : MonoBehaviour
{
    [SerializeField] private GravityObjectCollisionSettings _collisionSettings;
    [SerializeField] private Rigidbody _rb;
    public Rigidbody Rb => _rb;


    private List<Transform> _recentlyCilidedObjects;
    
    private WaitForSeconds _recentlyColidedDelay;
    private WaitForSeconds _structureColidedDelay;
    private WaitForSeconds _antigravityDuration;
    private Material _collisionMaterial;
    private Vector2 _explosionForceRange;
    private float _explosionForce;
    private float _exposionRad;
    private bool _isRecentlyColide;

    
    public static event Action<GravityObject, bool> OnAntigravityRequested;
    public static event Action OnStructureColided;

    private void Start()
    {
        _recentlyCilidedObjects = new List<Transform>();
        _recentlyColidedDelay = new WaitForSeconds(_collisionSettings.recentlyColidedDelay);
        _structureColidedDelay = new WaitForSeconds(_collisionSettings.structureColidedDelay);
        _antigravityDuration =  new WaitForSeconds(_collisionSettings.antigravitydDuration);
        _explosionForceRange = _collisionSettings.explosionForceRangeMul;
        _explosionForce = _collisionSettings.explosionForce;
        _exposionRad = _collisionSettings.exposionRad;
        _collisionMaterial = _collisionSettings.collisionMateriall;
    }

    private void OnCollisionEnter(Collision collision)
    {
        bool isGravityObject = false;
        collision.gameObject.TryGetComponent(out GravityObject obj);
        if (obj != null) isGravityObject = true;

        if (!isGravityObject) return;
        RegisterCollision(collision);
        if (_isRecentlyColide) return;
        HandleCollision(collision);
        
#if UNITY_EDITOR
        if (_collisionSettings.debugMod)
        {
            ShowCollision(collision);
        }
#endif
        
    }

    
    
    private void RegisterCollision(Collision collision)
    {
        foreach (var obj in _recentlyCilidedObjects)
        {
            if (collision.transform == obj) return;
        }
        StartCoroutine(RegisterCollisionCoroutine(collision));
    }
    
    private IEnumerator RegisterCollisionCoroutine(Collision collision)
    {
        var colTransdorm = collision.transform;
        _recentlyCilidedObjects.Add(colTransdorm);
        OnStructureColided!.Invoke();
        yield return _structureColidedDelay;
        _recentlyCilidedObjects.Remove(colTransdorm);
    }
    
    
    
    private void HandleCollision(Collision collision)
    {
        StartCoroutine(RecentlyColideDelayCoroutine());
        StartCoroutine(AntigravityCoroutine());

        
        GameObject cubeColision = collision.GetContact(collision.contacts.Length - 1).otherCollider.gameObject;
        MeshRenderer cubeMR = cubeColision.GetComponent<MeshRenderer>();
        cubeMR.material = _collisionMaterial;
        
        _rb.velocity = Vector3.zero;
        _rb.AddExplosionForce(_explosionForce * Random.Range(_explosionForceRange.x,_explosionForceRange.y),
            collision.transform.position,_exposionRad);
    }

    private IEnumerator RecentlyColideDelayCoroutine()
    {
        _isRecentlyColide = true;
        yield return _recentlyColidedDelay;
        _isRecentlyColide = false;
    }
    
    private IEnumerator AntigravityCoroutine()
    {
        OnAntigravityRequested?.Invoke(this, true);
        yield return _antigravityDuration;
        OnAntigravityRequested?.Invoke(this, false);
    }
    
    
    
    
#if UNITY_EDITOR
    private void ShowCollision(Collision collision)
    {
        
        Debug.Log("Impact" + collision.gameObject.name + " - other -" + collision.
            GetContact(collision.contacts.Length-1).otherCollider.gameObject.name);
        foreach (ContactPoint contact in collision.contacts)
        {
            Debug.DrawRay(contact.point, contact.normal*10, Color.red,5f);
        }
        
    }
#endif


}
