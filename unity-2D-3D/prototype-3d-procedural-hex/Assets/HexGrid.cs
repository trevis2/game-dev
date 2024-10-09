using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexGrid : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        for (int x = 0; x < 5; x++)
        {
            for (int z = 0; z < 5; z++)
            {
                GameObject tile = new GameObject($"Hex {x},{z}", typeof(HexRenderer));
                tile.transform.position = GetPos(new Vector2Int(x, z));

                HexRenderer hexRenderer = tile.GetComponent<HexRenderer>();
                hexRenderer.outerRange = 0.4f;
                hexRenderer.innerRange = 0.2f;
                hexRenderer.inferiorHeight = 0f;
                hexRenderer.superiorHeight = 0.1f;
                hexRenderer.DrawMesh();
                // tile.SetParent(transform, true);

            }
        }
    }

    private Vector3 GetPos(Vector2Int coordinate)
    {
        int column = coordinate.x;
        int row = coordinate.y;

        bool off = column % 2 == 0;
        float width = 2f * 0.4f;
        float heig = Mathf.Sqrt(3f) * 0.4f;

        float horizontD = width * (3f / 4f);
        float verticalD = heig;
        float offsetRea = off ? heig / 2 : 0;


        float xPos = column * horizontD;
        float zPos = (row * verticalD) - offsetRea;

        return new Vector3(xPos, 0, -zPos);
    }


}
