using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MolkGame
{
    public class PinPlacementSystem : MonoBehaviour
    {
        [Header("Prefab")]
        [SerializeField] private GameObject skittlePrefab;

        [Header("Placement")]
        [SerializeField] private int rows = 4;
        [SerializeField] private float rowSpacing = 0.9f;
        [SerializeField] private float pinSpacing = 0.7f;
        [SerializeField] private float rowOffset = 0.18f;
        [SerializeField] private float pinTiltAngle = 5f;
        [SerializeField] private float pinHeightOffset = 0.02f;
        [SerializeField] private int[] rowCounts = { 2, 3, 4, 3 };
        [SerializeField] private Vector3 placementOffset = Vector3.zero;

        private void Awake()
        {
            if (transform.childCount == 0)
            {
                PlacePins();
            }
        }

        [ContextMenu("Place Pins")]
        public void PlacePins()
        {
            if (skittlePrefab == null)
            {
                Debug.LogWarning("Skittle prefab is not assigned.");
                return;
            }

            ClearPins();

            int totalRows = Mathf.Min(rows, rowCounts.Length);
            float sharedCenterX = 0f;
            List<GameObject> placedPins = new List<GameObject>();

            for (int row = 0; row < totalRows; row++)
            {
                int columns = rowCounts[row];
                float rowZOffset = row * rowSpacing;
                float tilt = Application.isPlaying ? 0f : ((row % 2 == 0) ? -pinTiltAngle : pinTiltAngle);
                float rowCenterOffset = (columns - 1) * pinSpacing * 0.5f;

                for (int column = 0; column < columns; column++)
                {
                    GameObject skittle = Instantiate(skittlePrefab, transform);
                    skittle.name = $"Skittle_{row}_{column}";
                    float xPosition = (column * pinSpacing) - rowCenterOffset;
                    Vector3 localPosition = new Vector3(
                        placementOffset.x + xPosition + sharedCenterX,
                        placementOffset.y + row * pinHeightOffset,
                        placementOffset.z + rowZOffset
                    );

                    skittle.transform.localPosition = localPosition;
                    skittle.transform.localRotation = Quaternion.Euler(0f, 0f, tilt);
                    skittle.transform.localScale = skittlePrefab.transform.localScale;
                    skittle.transform.SetSiblingIndex(0);

                    if (Application.isPlaying)
                    {
                        // 生成直後の衝突・回転でピンが一瞬でズレるのを防ぐため、配置完了後に物理を有効化する。
                        PreparePinForPlacement(skittle);
                        placedPins.Add(skittle);
                    }
                }
            }

            if (Application.isPlaying)
            {
                Physics.SyncTransforms();
                StartCoroutine(ActivatePlacedPins(placedPins));
            }
        }

        private void ClearPins()
        {
            for (int i = transform.childCount - 1; i >= 0; i--)
            {
                Transform child = transform.GetChild(i);
                if (Application.isPlaying)
                {
                    Destroy(child.gameObject);
                }
                else
                {
                    DestroyImmediate(child.gameObject);
                }
            }
        }

        private static void PreparePinForPlacement(GameObject pin)
        {
            // 物理演算を一時停止して、配置中にコライダーが衝突を起こさないようにする。
            Rigidbody[] rigidbodies = pin.GetComponentsInChildren<Rigidbody>(true);
            Collider[] colliders = pin.GetComponentsInChildren<Collider>(true);

            foreach (Rigidbody rigidbody in rigidbodies)
            {
                rigidbody.isKinematic = true;
                rigidbody.linearVelocity = Vector3.zero;
                rigidbody.angularVelocity = Vector3.zero;
                rigidbody.ResetCenterOfMass();
                rigidbody.ResetInertiaTensor();
                rigidbody.Sleep();
            }

            foreach (Collider collider in colliders)
            {
                collider.enabled = false;
            }
        }

        private IEnumerator ActivatePlacedPins(IEnumerable<GameObject> pins)
        {
            yield return new WaitForSeconds(0.1f);
            Physics.SyncTransforms();

            foreach (GameObject pin in pins)
            {
                if (pin == null)
                {
                    continue;
                }

                // 配置完了後にまとめて物理を再開し、初期状態の不安定さを避ける。

                Rigidbody[] rigidbodies = pin.GetComponentsInChildren<Rigidbody>(true);
                Collider[] colliders = pin.GetComponentsInChildren<Collider>(true);

                foreach (Collider collider in colliders)
                {
                    collider.enabled = true;
                }

                foreach (Rigidbody rigidbody in rigidbodies)
                {
                    rigidbody.isKinematic = false;
                    rigidbody.linearVelocity = Vector3.zero;
                    rigidbody.angularVelocity = Vector3.zero;
                    rigidbody.WakeUp();
                }
            }

            yield return null;
            Physics.SyncTransforms();
        }
    }
}
