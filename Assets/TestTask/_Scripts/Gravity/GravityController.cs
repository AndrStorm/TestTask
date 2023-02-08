using System.Collections.Generic;
using UnityEngine;

public class GravityController
{
    private readonly List<Rigidbody> _gravityObjectsRbs;
    private readonly Transform _targetTransform;
    private readonly float _decelerationDist;
    private readonly  float _accelerationMult;
    
    private List<Rigidbody> _antigravityRbs;
    
    public GravityController(GravitySettings gravitySettings)
    {
        _gravityObjectsRbs = new List<Rigidbody>();
        _antigravityRbs = new List<Rigidbody>();
        _targetTransform = gravitySettings.target.transform;
        _decelerationDist = gravitySettings.decelerationDist;
        _accelerationMult = gravitySettings.accelerationMult;
    }

    public void OnTick()
    {
        ApplyGravity();
    }

    private void ApplyGravity()
    {
        foreach (var gravityObjectsRb in _gravityObjectsRbs)
        {
            Vector3 forceDir = _targetTransform.position - gravityObjectsRb.position;
            
            if (forceDir.magnitude >= _decelerationDist)
            {
                gravityObjectsRb.velocity += _accelerationMult * Time.fixedDeltaTime * forceDir.normalized;
            }
            else
            {
                gravityObjectsRb.velocity *= Mathf.Clamp01(forceDir.magnitude / _decelerationDist);
            }
        }
    }

    public void DeactivateGravity(GravityObject gravityObject)
    {
        var rb = gravityObject.Rb;
        _gravityObjectsRbs.Remove(rb);
        _antigravityRbs.Add(rb);
    }

    public void ActivateGravity(GravityObject gravityObject)
    {
        var rb = gravityObject.Rb;
        _gravityObjectsRbs.Add(rb);
        _antigravityRbs.Remove(rb);
    }

    public void AddGravityObject(GravityObject gravityObject)
    {
        _gravityObjectsRbs.Add(gravityObject.Rb);
    }
    
}
