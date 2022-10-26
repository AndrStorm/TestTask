using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class ImpactExplosion : MonoBehaviour
{
    
    [SerializeField]private Vector2 explosionForceRange = new Vector2(0.8f,1.2f);
    [SerializeField]private float explosionForce = 750f;
    [SerializeField]private float exposionRad = 5f;
    
    
    private Transform target;
    private Rigidbody _rb;
    private bool _recentlyColide;
    private readonly WaitForSeconds secDelay = new WaitForSeconds(1f);
    
    public delegate void Action();
    public static event Action OnImpact;
    
    
    private void Awake()
    {
        _rb = gameObject.GetComponent<Rigidbody>();

    }

    
    private void Start()
    {
        target = GameManager.Instance.GetTargetTransform();
    }

    
    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("Obstacle") && !_recentlyColide)
        {
            
#if UNITY_EDITOR

            if (GameManager.Instance.debugmode)
            {
                Debug.Log("Impact" + collision.gameObject.name + " - other -" + collision.GetContact(collision.contacts.Length-1).otherCollider.gameObject.name);
                foreach (ContactPoint contact in collision.contacts)
                {
                    Debug.DrawRay(contact.point, contact.normal*10, Color.red,5f);
                }
            }
            
#endif
            
            GameObject cubeColision = collision.GetContact(collision.contacts.Length - 1).otherCollider.gameObject;
            MeshRenderer cubeMR = cubeColision.GetComponent<MeshRenderer>();
            cubeMR.material = GameManager.Instance.cubeColideMat;

            
            _recentlyColide = true;
            GameManager.Instance.controlVelocity = false;
            OnImpact?.Invoke();

            _rb.velocity = Vector3.zero;
            _rb.AddExplosionForce(explosionForce * Random.Range(explosionForceRange.x,explosionForceRange.y),collision.transform.position,exposionRad);
            
            StartCoroutine(RecentlyColideDelay());
        }
    }

    
    IEnumerator RecentlyColideDelay()
    {
        yield return secDelay;
        _recentlyColide = false;
        GameManager.Instance.controlVelocity = true;
    }
    
    
}
