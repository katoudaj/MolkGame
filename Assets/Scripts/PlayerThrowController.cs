using UnityEngine;

namespace MolkGame
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerThrowController : MonoBehaviour
    {
        [Header("Throw")]
        [SerializeField] private GameObject molkBallPrefab;
        [SerializeField] private Transform spawnPoint;
        [SerializeField] private float throwForce = 6f;

        private void Update()
        {
            // simple keyboard test: space to throw forward
            if (Input.GetKeyDown(KeyCode.Space))
            {
                ThrowTest();
            }

            // debug adjust force
            if (Input.GetKeyDown(KeyCode.UpArrow)) throwForce += 0.5f;
            if (Input.GetKeyDown(KeyCode.DownArrow)) throwForce = Mathf.Max(0f, throwForce - 0.5f);
        }

        public void ThrowTest()
        {
            if (molkBallPrefab == null || spawnPoint == null) return;
            GameObject go = Instantiate(molkBallPrefab, spawnPoint.position, spawnPoint.rotation);
            var ball = go.GetComponent<MolkBall>();
            if (ball == null) ball = go.AddComponent<MolkBall>();

            Vector3 dir = spawnPoint.forward;
            ball.Throw(dir, throwForce);
        }
    }
}
