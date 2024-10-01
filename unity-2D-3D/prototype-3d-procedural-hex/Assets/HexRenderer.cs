using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexRenderer : MonoBehaviour
{
    //A class that allows you to create or modify meshes.
    //Meshes contain vertices and multiple triangle arrays.
    //
    private Mesh m_mesh;
    private MeshFilter m_meshFilter;
    private MeshRenderer m_meshRenderer;

    private List<Face> m_faces;

    [SerializeField] Material material;
    [SerializeField] float innerRadius = 0.5f;
    [SerializeField] float outerRadius = 0.5f;
    [SerializeField] float height = 0.5f;


    private void Awake()
    {
        m_meshFilter = GetComponent<MeshFilter>();
        m_meshRenderer = GetComponent<MeshRenderer>();

        m_mesh = new Mesh();
        m_mesh.name = "Hex";

        m_meshFilter.mesh = m_mesh;
        m_meshRenderer.material = material;
    }
    private void OnEnable()
    {
        DrawMesh();
    }

    private void DrawMesh()
    {
        DrawFaces();
        CombineFaces();
    }

    private void CombineFaces()
    {
        List<Vector3> vertices = new List<Vector3>();
        List<int> triangles = new List<int>();
        List<Vector2> uvs = new List<Vector2>();

        for (int i = 0; i < m_faces.Count; i++)
        {
            vertices.AddRange(m_faces[i].vertices);
            uvs.AddRange(m_faces[i].uvs);
            int offset = 4 * i;
            foreach (int triangle in m_faces[i].triangles)
            {
                triangles.Add(triangle + offset);
            }
        }
    }

    private void DrawFaces()
    {
        m_faces = new List<Face>();

        for (int point = 0; point < 6; point++)
        {
            m_faces.Add(CreateFace(innerRadius, outerRadius, height / 2f, height / 2f, point));
        }
    }

    private Face CreateFace(float innerRad, float outerRad, float heightA, float heightB, int point, bool reverse = false)
    {
        Vector3 pointA = GetPoint(innerRad, heightB, point);
        Vector3 pointB = GetPoint(innerRad, heightB, (point < 5) ? point + 1 : 0);
        Vector3 pointC = GetPoint(outerRad, heightA, (point < 5) ? point + 1 : 0);
        Vector3 pointD = GetPoint(outerRad, heightA, point);

        List<Vector3> vertices = new List<Vector3>() { pointA, pointB, pointC, pointD };
        List<int> triangles = new List<int>() { 0, 1, 2, 2, 3, 0 };
        List<Vector2> uvs = new List<Vector2>() { new Vector2(0, 0), new Vector2(1, 0), new Vector2(1, 1), new Vector2(0, 1) };

        if (reverse)
        {
            vertices.Reverse();
        }
        return new Face(vertices, triangles, uvs);
    }

    private Vector3 GetPoint(float size, float height, int index)
    {
        float angle_deg = 60 * index;
        float angle_rad = Mathf.PI / 180f * angle_deg;
        return new Vector3(size * Mathf.Cos(angle_rad), height, size * Mathf.Sin(angle_rad));
    }
}

public struct Face
{
    public List<Vector3> vertices { get; private set; } //vertici della faccia
    public List<int> triangles { get; private set; } //triangoli
    public List<Vector2> uvs { get; private set; } //non so cos'è uv

    public Face(List<Vector3> vertices, List<int> triangles, List<Vector2> uvs)
    {
        this.vertices = vertices;
        this.triangles = triangles;
        this.uvs = uvs;
    }
}
