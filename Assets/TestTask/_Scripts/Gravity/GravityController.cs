using System.Collections.Generic;
using UnityEngine;

public class GravityController
{
    private readonly List<GravityObject> _gravityObjectsRbs;
    private readonly Transform _targetTransform;
    private readonly float _decelerationDist;
    private readonly  float _accelerationMult;
    
    
    public GravityController(GravitySettings gravitySettings)
    {
        _gravityObjectsRbs = new List<GravityObject>();
        _targetTransform = gravitySettings.target.transform;
        _decelerationDist = gravitySettings.decelerationDist;
        _accelerationMult = gravitySettings.accelerationMult;
    }

    public void DeInit()
    {
        foreach (var gravityObject in _gravityObjectsRbs)
        {
            gravityObject.OnAntigravityRequested -= OnGravityObjectAntigravityRequested;
        }
    }

    public void OnFixedTick()
    {
        ApplyGravity();
    }
    
    
    public void AddGravityObject(GravityObject gravityObject)
    {
        ActivateGravity(gravityObject);
        gravityObject.OnAntigravityRequested += OnGravityObjectAntigravityRequested;
    }
    

    private void ApplyGravity()
    {
        foreach (var gravityObject in _gravityObjectsRbs)
        {
            Vector3 forceDir = _targetTransform.position - gravityObject.Rb.position;
            
            if (forceDir.magnitude >= _decelerationDist)
            {
                gravityObject.Rb.velocity += _accelerationMult * Time.fixedDeltaTime * forceDir.normalized;
            }
            else
            {
                gravityObject.Rb.velocity *= Mathf.Clamp01(forceDir.magnitude / _decelerationDist);
            }
        }
    }

    private void DeactivateGravity(GravityObject gravityObject)
    {
        _gravityObjectsRbs.Remove(gravityObject);
    }

    private void ActivateGravity(GravityObject gravityObject)
    {
        _gravityObjectsRbs.Add(gravityObject);
    }
    
    
    private void OnGravityObjectAntigravityRequested(GravityObject gravityObject, bool isColide)
    {
        if (isColide)
        {
            DeactivateGravity(gravityObject);
        }
        else
        {
            ActivateGravity(gravityObject);
        }
    }
    
}
