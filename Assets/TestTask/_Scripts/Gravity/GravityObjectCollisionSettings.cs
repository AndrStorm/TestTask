using System;
using UnityEngine;

[Serializable]
public class GravityObjectCollisionSettings
{
    public bool debugMod = false;
    public float structureColidedDelay = 0.5f;
    public float recentlyColidedDelay = 0.75f;
    public float antigravitydDuration = 1f;
    public float exposionRad = 5f;
    public float explosionForce = 750f;
    public Vector2 explosionForceRangeMul = new Vector2(0.8f,1.2f);
    public Material collisionMateriall;
}
