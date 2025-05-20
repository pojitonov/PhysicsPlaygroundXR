using Flexalon;
using UnityEngine;

[ExecuteAlways]
public class FlexalonHide : MonoBehaviour
{
    [SerializeField] private FlexalonGridLayout flaxonGrid;

    private void OnValidate()
    {
        UpdateVisibility();
    }

    private void Update()
    {
        UpdateVisibility();
    }

    private void UpdateVisibility()
    {
        if (flaxonGrid == null) return;

        uint visibleCount = flaxonGrid.Columns;
        int total = transform.childCount;

        for (int i = 0; i < total; i++)
        {
            transform.GetChild(i).gameObject.SetActive(i < visibleCount);
        }
    }
}