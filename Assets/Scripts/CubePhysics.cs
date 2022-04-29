using UnityEngine;

public class CubePhysics : MonoBehaviour
{
    private const float ExplosionForce = 1500f;
    private const float ExplosionRadius = 1f;
    private const float PushForce = 5000;

    public bool IsOutsideStartArea { get; set; }
    public bool HasContact { get; set; }

    private Rigidbody _rigidbody;
    private Collider _collider;

    public void Init()
    {
        _collider = GetComponent<Collider>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        _collider.enabled = true;
        _rigidbody.isKinematic = false;
    }

    private void OnDisable()
    {
        IsOutsideStartArea = false;
        _rigidbody.rotation = Quaternion.identity;
        _rigidbody.velocity = Vector3.zero;        
        HasContact = false;
    }

    public void DeactivatePhysics(bool isEnable)
    {
        HasContact = isEnable;
        _rigidbody.isKinematic = isEnable;
        _collider.enabled = !isEnable;
    }

    public void Shoot()
    {
        _rigidbody.constraints = RigidbodyConstraints.None;
        _rigidbody.AddForce(Vector3.forward * PushForce);
    }

    public void Explosion()
    {
        float random = Random.Range(-0.4f, 0.4f);
        Vector3 explosionPosition = transform.position - Vector3.up + new Vector3(random, random, random);
        _rigidbody.AddExplosionForce(ExplosionForce, explosionPosition, ExplosionRadius);
    }
}
