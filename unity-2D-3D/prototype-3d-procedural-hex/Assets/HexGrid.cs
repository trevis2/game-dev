using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;



public class HexGrid : MonoBehaviour
{
    [SerializeField] int numeroAsseX = 3;
    [SerializeField] int numeroAsseZ = 3;
    [SerializeField] Material material;
    [SerializeField] GameObject pezzo;
    void Update()
    {
        for (int x = 0; x < numeroAsseX; x++)
        {
            for (int z = 0; z < numeroAsseZ; z++)
            {
                GameObject tile = new GameObject($"Hex {x},{z}", typeof(HexRenderer));
                tile.transform.SetParent(transform);
                tile.transform.position = GetPos(new Vector2Int(x, z));

                HexRenderer hexRenderer = tile.GetComponent<HexRenderer>();
                hexRenderer.material = material;
                hexRenderer.outerRange = pezzo.GetComponent<HexRenderer>().outerRange;
                hexRenderer.innerRange = pezzo.GetComponent<HexRenderer>().innerRange;
                hexRenderer.inferiorHeight = pezzo.GetComponent<HexRenderer>().inferiorHeight;
                hexRenderer.superiorHeight = pezzo.GetComponent<HexRenderer>().superiorHeight;
                hexRenderer.DrawMesh();
            }
        }
    }

    private Vector3 GetPos(Vector2Int coordinate)
    {
        int column = coordinate.x;
        int row = coordinate.y;

        bool off = column % 2 == 0;
        float width = 2f * pezzo.GetComponent<HexRenderer>().outerRange;
        float heig = Mathf.Sqrt(3f) * pezzo.GetComponent<HexRenderer>().outerRange;

        float horizontD = width * (3f / 4f);
        float verticalD = heig;
        float offsetRea = off ? heig / 2 : 0;


        float xPos = column * horizontD;
        float zPos = (row * verticalD) - offsetRea;

        return new Vector3(xPos, 0, -zPos);
    }


}
