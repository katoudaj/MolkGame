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

            for (int row = 0; row < totalRows; row++)
            {
                int columns = rowCounts[row];
                float rowZOffset = row * rowSpacing;
                float tilt = (row % 2 == 0) ? -pinTiltAngle : pinTiltAngle;
                float rowCenterOffset = (columns - 1) * pinSpacing * 0.5f;

                for (int column = 0; column < columns; column++)
                {
                    GameObject skittle = Instantiate(skittlePrefab, transform);
                    skittle.name = $"Skittle_{row}_{column}";
                    float xPosition = (column * pinSpacing) - rowCenterOffset;
                    skittle.transform.localPosition = new Vector3(
                        placementOffset.x + xPosition + sharedCenterX,
                        placementOffset.y + row * pinHeightOffset,
                        placementOffset.z + rowZOffset
                    );
                    skittle.transform.localRotation = Quaternion.Euler(0f, 0f, tilt);
                    skittle.transform.localScale = skittlePrefab.transform.localScale;
                }
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
    }
}
