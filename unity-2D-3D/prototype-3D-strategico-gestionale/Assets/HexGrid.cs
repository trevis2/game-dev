using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HexGrid : MonoBehaviour
{
    [SerializeField] int gridWidth;
    [SerializeField] int gridHeight;

    [SerializeField] GameObject prefab;

    GameObject[] cells;

    public TMP_Text cellLabelPrefab;
    Canvas gridCanvas;


    private void Awake()
    {
        gridCanvas = GetComponentInChildren<Canvas>();
        cells = new GameObject[gridHeight * gridWidth];
        for (int z = 0, i = 0; z < gridHeight; z++)
        {
            for (int x = 0; x < gridWidth; x++)
            {
                CreateCell(x, z, i++);
            }
        }
    }

    void CreateCell(int x, int z, int i)
    {
        Vector3 position;
        position.x = (x + z * 0.5f - z / 2) * (HexMetrics.innerRadius * 2f);
        position.y = 0f;
        position.z = z * (HexMetrics.outerRadius * 1.5f);

        GameObject cell = cells[i] = Instantiate(prefab);
        cell.transform.SetParent(transform, false);
        cell.transform.localPosition = position;

        TMP_Text label = Instantiate(cellLabelPrefab);
        label.rectTransform.SetParent(gridCanvas.transform, false);
        label.rectTransform.anchoredPosition =
            new Vector2(position.x, position.z);
        label.text = "(" + x.ToString() + "," + z.ToString() + ")";
    }
}
