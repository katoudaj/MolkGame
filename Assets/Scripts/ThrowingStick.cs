using UnityEngine;

namespace MolkGame
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Collider))]
    public class ThrowingStick : MonoBehaviour
    {
        private Rigidbody _rb;
        private const float LifetimeSeconds = 8f;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            _rb.mass = 0.5f;
            _rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
            _rb.useGravity = true;
            Destroy(gameObject, LifetimeSeconds);
        }

        public void Throw(Vector3 direction, float force)
        {
            _rb.linearVelocity = Vector3.zero;
            _rb.angularVelocity = Vector3.zero;
            _rb.AddForce(direction.normalized * force, ForceMode.Impulse);
        }
    }
}
