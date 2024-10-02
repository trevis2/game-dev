using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Test : MonoBehaviour
{
    private Mesh m_mesh;
    private MeshFilter m_meshFilter;
    private MeshRenderer m_meshRenderer;
    [SerializeField] Material material;
    [SerializeField][Range(0.0f, 20.0f)] float outerRange = 2.0f;
    [SerializeField][Range(0.0f, 20.0f)] float innerRange = 1.0f;

    private void Awake()
    {
        m_meshFilter = this.AddComponent<MeshFilter>();
        m_meshRenderer = this.AddComponent<MeshRenderer>();
        m_mesh = new Mesh();
        m_mesh.name = "Hex";

        m_meshFilter.mesh = m_mesh;
        // material = new Material(Shader.Find("Standard"));
        m_meshRenderer.material = material;
    }

    private void Update()
    {
        m_mesh.vertices = CreazioneVertici(); //creo i vertici prima gli interni e poi gli esterni
        m_mesh.triangles = CreazioneTriangoli(); //la superficie è divisa in 6 facce e ogni faccia composta da 2 triangoli. ogni triangolo ha 3 vertici

        m_mesh.RecalculateNormals();

    }

    private int[] CreazioneTriangoli()
    {
        int[] triangles = new int[0];
        for (int i = 0; i < 6; i++)
        {
            int[] faccia = CreaFaccia(i);
            triangles = triangles.Concat(faccia).ToArray();
        }
        string arrayString = string.Join(", ", triangles);
        Debug.Log("triangles: " + arrayString);
        return triangles.Reverse().ToArray();
    }
    private int[] CreaFaccia(int i)
    {
        int verticeInterno = i;
        int verticeEsterno = i + 6;
        int verticeInternoSuccessivo = (i + 1) % 6;
        int verticeEsternoSuccessivo = 6 + ((i + 1) % 6);

        int[] triangoloBasso = new int[] { verticeInterno, verticeEsterno, verticeInternoSuccessivo };
        int[] triangoloAlto = new int[] { verticeInternoSuccessivo, verticeEsterno, verticeEsternoSuccessivo };
        int[] faccia = triangoloBasso.Concat(triangoloAlto).ToArray();
        return faccia;
    }

    private Vector3[] CreazioneVertici()
    {
        Vector3[] vertices = new Vector3[12];
        for (int i = 0; i < 6; i++)
        {
            const float deg = 60;
            float angleInRad = i * deg * Mathf.Deg2Rad;
            vertices[i] = new Vector3(innerRange * Mathf.Cos(angleInRad), 0, innerRange * Mathf.Sin(angleInRad));
            vertices[i + 6] = new Vector3(outerRange * Mathf.Cos(angleInRad), 0, outerRange * Mathf.Sin(angleInRad));
        }
        string arrayString = string.Join(", ", vertices);
        Debug.Log("vertices: " + arrayString);
        return vertices;
    }

}
