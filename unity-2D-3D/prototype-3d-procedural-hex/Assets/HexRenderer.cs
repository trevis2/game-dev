using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class HexRenderer : MonoBehaviour
{
    private Mesh m_mesh;
    private MeshFilter m_meshFilter;
    private MeshRenderer m_meshRenderer;
    [SerializeField] Material material;
    [SerializeField][Range(0.0f, 20.0f)] float outerRange = 2.0f;
    [SerializeField][Range(0.0f, 20.0f)] float innerRange = 1.0f;
    [SerializeField][Range(0.0f, 20.0f)] float heightSuperior = 2.0f;
    [SerializeField][Range(0.0f, 20.0f)] float heightInferior = 0.0f;
    [SerializeField][Range(1, 10)] int numberVertex = 6;
    [SerializeField][Range(0.0f, 360.0f)] float angleRotation = 30.0f;
    [SerializeField] bool applyRotation = false;

    private List<Face> m_faces;


    private void Awake()
    {

        m_meshFilter = GetComponent<MeshFilter>();
        m_meshRenderer = GetComponent<MeshRenderer>();
        m_mesh = new Mesh();
        m_mesh.name = "Hex";

        m_meshFilter.mesh = m_mesh;
        m_meshRenderer.material = material;
    }
    private void Start()
    {
        m_faces = new List<Face>(4);
        DisegnaFacce();
        ComponiFacce();
    }

    private void ComponiFacce()
    {
        List<Vector3> vertices = new List<Vector3>();
        List<int> triangles = new List<int>();
        List<Vector2> uvs = new List<Vector2>();


        for (int j = 0; j < m_faces.Count; j++)
        {
            vertices.AddRange(m_faces[j].vertices);
            triangles.AddRange(m_faces[j].triangles);
        }

        string verticesString = string.Join(", ", vertices);
        Debug.Log("vertices: " + verticesString);
        Debug.Log("Capacity: " + vertices.Capacity);
        string arrayTriangles = string.Join(", ", triangles);
        Debug.Log("triangles: " + arrayTriangles);

        m_mesh.vertices = vertices.ToArray();
        m_mesh.triangles = triangles.ToArray();
        m_mesh.uv = uvs.ToArray();
        m_mesh.RecalculateNormals();
    }

    private void DisegnaFacce()
    {

        m_faces.Add(new Face(CalculateVertexArray(heightSuperior), DisegnaTriangoli(0)));
        m_faces.Add(new Face(CalculateVertexArray(heightInferior), DisegnaTriangoli(1)));
    }

    private List<Vector3> CalculateVertexArray(float heightVertex)
    {
        float angle = 360.0f / numberVertex * Mathf.Deg2Rad;

        List<Vector3> vertices = new List<Vector3>(numberVertex * 2);
        Vector3[] vertici = new Vector3[numberVertex * 2];
        for (int i = 0; i < numberVertex; i++)
        {
            float angleInRad = i * angle + GetAngleRotation();
            vertici[i] = new Vector3(innerRange * Mathf.Cos(angleInRad), heightVertex, innerRange * Mathf.Sin(angleInRad));
            vertici[i + numberVertex] = new Vector3(outerRange * Mathf.Cos(angleInRad), heightVertex, outerRange * Mathf.Sin(angleInRad));
        }

        vertices.AddRange(vertici);
        return vertices;
    }

    private List<int> DisegnaTriangoli(int k)
    {
        List<int> triangles = new List<int>();
        for (int i = 0; i < numberVertex; i++)
        {
            int internalVertex = i + 2 * numberVertex * k;
            int externalVertex = i + numberVertex + 2 * numberVertex * k;
            int verticeInternoSuccessivo = (i + 2 * numberVertex * k + 1) % (numberVertex + 2 * numberVertex * k);
            int verticeEsternoSuccessivo = numberVertex + 2 * numberVertex * k + ((i + 1) % numberVertex);

            int[] lowTriangle = new int[] { internalVertex, externalVertex, verticeInternoSuccessivo };
            int[] highTriangle = new int[] { verticeInternoSuccessivo, externalVertex, verticeEsternoSuccessivo };

            int[] triangoli = lowTriangle.Concat(highTriangle).ToArray();
            triangles.AddRange(triangoli);
        }

        triangles.Reverse();
        return triangles;
    }

    private float GetAngleRotation()
    {
        return applyRotation ? (angleRotation * Mathf.Deg2Rad) : 0f;
    }
}

public struct Face
{
    public List<Vector3> vertices { get; private set; }
    public List<int> triangles { get; private set; }
    //public List<Vector2> uvs { get; private set; }
    public Face(List<Vector3> vertices, List<int> triangles)//, List<Vector2> uvs)
    {
        this.vertices = vertices;
        this.triangles = triangles;
        //this.uvs = uvs;
    }
}
