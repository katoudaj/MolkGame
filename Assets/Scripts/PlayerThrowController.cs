using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

namespace MolkGame
{
    public class PlayerThrowController : MonoBehaviour
    {
        [Header("Throw")]
        [SerializeField] private GameObject throwingStickPrefab;
        [SerializeField] private Transform spawnPoint;
        [SerializeField] private float throwForce = 6f;

        private void Update()
        {
#if ENABLE_INPUT_SYSTEM && !ENABLE_LEGACY_INPUT_MANAGER
            // New Input System (when legacy input is disabled)
            if (Keyboard.current != null)
            {
                if (Keyboard.current.spaceKey.wasPressedThisFrame) ThrowTest();
                if (Keyboard.current.upArrowKey.wasPressedThisFrame) throwForce += 0.5f;
                if (Keyboard.current.downArrowKey.wasPressedThisFrame) throwForce = Mathf.Max(0f, throwForce - 0.5f);
            }
#else
            // Legacy Input (Input Manager)
            if (Input.GetKeyDown(KeyCode.Space))
            {
                ThrowTest();
            }

            // debug adjust force
            if (Input.GetKeyDown(KeyCode.UpArrow)) throwForce += 0.5f;
            if (Input.GetKeyDown(KeyCode.DownArrow)) throwForce = Mathf.Max(0f, throwForce - 0.5f);
#endif
        }

        public void ThrowTest()
        {
            if (throwingStickPrefab == null || spawnPoint == null) return;
            GameObject go = Instantiate(throwingStickPrefab, spawnPoint.position, spawnPoint.rotation);
            var stick = go.GetComponent<ThrowingStick>();
            if (stick == null) stick = go.AddComponent<ThrowingStick>();

            Vector3 dir = spawnPoint.forward;
            stick.Throw(dir, throwForce);
        }
    }
}
