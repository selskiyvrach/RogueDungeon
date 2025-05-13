using UnityEngine;
using UnityEngine.UI;

namespace Common.UtilsDotNet
{
    public static class GridLayoutExtensions
    {
        public static void GetRowsAndColumns(this GridLayoutGroup grid, out int rows, out int columns)
        {
            var childCount = grid.transform.childCount;
            var rt = grid.GetComponent<RectTransform>();

            columns = Mathf.FloorToInt(
                (rt.rect.width + grid.spacing.x + 0.01f) /
                (grid.cellSize.x + grid.spacing.x)
            );

            rows = Mathf.CeilToInt((float)childCount / columns);
        }
    }
}