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
        // m_faces
        // Face facciaInferiore = DisegnaFaccia(-heightInferior);
        DisegnaFaccia(heightSuperior);
        ComponiFacce();
        // m_mesh.vertices = facciaSuperiore.vertices.ToArray();
        // m_mesh.triangles = facciaSuperiore.triangles.ToArray();
        // DisegnaFacciaInferiore();
        // m_mesh.triangles = null;
        // m_mesh.uv2 = null;
        m_mesh.RecalculateNormals();

    }

    private void ComponiFacce()
    {
        List<Vector3> vertices = new List<Vector3>();
        List<int> triangles = new List<int>();
        List<Vector2> uvs = new List<Vector2>();


        foreach (Face face in m_faces)
        {
            vertices.AddRange(face.vertices);
            uvs.AddRange(face.uvs);
            triangles.AddRange(face.triangles);
        }

        m_mesh.vertices = vertices.ToArray();
        m_mesh.triangles = triangles.ToArray();
        m_mesh.uv = uvs.ToArray();
    }

    private Face DisegnaFaccia(float heightVertex)
    {

        List<Vector3> vertici = CalculateVertexArray(heightVertex);
        // List<int> triangoli = DisegnaTriangoli();

        // return new Face(vertici, triangoli, new List<Vector2>());
        return new Face();
    }

    private List<Vector3> CalculateVertexArray(float heightVertex)
    {
        float angle = 360.0f / numberVertex * Mathf.Deg2Rad;
        List<Vector3> vertices = new List<Vector3>();
        List<Vector3> vertices2 = new List<Vector3>(numberVertex * 2);
        vertices2.Insert(0, Vector3.right);
        vertices.Insert(0, Vector3.right);
        // vertices2.Insert(2, Vector3.left);
        // for (int i = 0; i < numberVertex; i++)
        // {
        //     float angleInRad = i * angle + GetAngleRotation();
        //     vertices.Insert(i, new Vector3(innerRange * Mathf.Cos(angleInRad), heightVertex, innerRange * Mathf.Sin(angleInRad)));
        //     vertices.Insert(i + numberVertex, new Vector3(outerRange * Mathf.Cos(angleInRad), heightVertex, outerRange * Mathf.Sin(angleInRad)));
        // }
        string arrayVertices = string.Join(", ", vertices);
        Debug.Log("vertices: " + arrayVertices);
        Debug.Log("Capacity: " + vertices.Capacity);
        string vertices2q = string.Join(", ", vertices2);
        Debug.Log("vertices2: " + vertices2q);
        Debug.Log("Capacity: " + vertices2.Capacity);
        return vertices;
    }

    private List<int> DisegnaTriangoli()
    {
        List<int> triangles = new List<int>();
        for (int i = 0; i < numberVertex; i++)
        {
            int internalVertex = i;
            int externalVertex = i + numberVertex;
            int verticeInternoSuccessivo = (i + 1) % numberVertex;
            int verticeEsternoSuccessivo = numberVertex + ((i + 1) % numberVertex);

            int[] lowTriangle = new int[] { internalVertex, externalVertex, verticeInternoSuccessivo };
            int[] highTriangle = new int[] { verticeInternoSuccessivo, externalVertex, verticeEsternoSuccessivo };

            int[] triangoli = lowTriangle.Concat(highTriangle).ToArray();
            triangles.AddRange(triangoli);
        }
        string arrayTriangles = string.Join(", ", triangles);
        Debug.Log("triangles: " + arrayTriangles);
        triangles.Reverse();
        return triangles;
    }

    private float GetAngleRotation()
    {
        return applyRotation ? (angleRotation * Mathf.Deg2Rad) : 0f;
    }

    // private void Update()
    // {
    //     angoloFisso = 360.0f / numeroVertici;

    //     m_mesh.vertices = CreazioneVertici(); //creo i vertici prima gli interni e poi gli esterni
    //     m_mesh.triangles = CreazioneTriangoli(); //la superficie è divisa in 6 facce e ogni faccia composta da 2 triangoli. ogni triangolo ha 3 vertici

    //     m_mesh.RecalculateNormals();
    // }

    // private int[] CreazioneTriangoli()
    // {
    //     int[] triangles = new int[0];
    //     for (int i = 0; i < numeroVertici; i++)
    //     {
    //         int[] faccia = CreaFaccia(i);
    //         triangles = triangles.Concat(faccia).ToArray();
    //     }
    //     string arrayString = string.Join(", ", triangles);
    //     Debug.Log("triangles: " + arrayString);
    //     return triangles.Reverse().ToArray();
    // }
    // private int[] CreaFaccia(int i)
    // {
    //     int verticeInterno = i;
    //     int verticeEsterno = i + numeroVertici;
    //     int verticeInternoSuccessivo = (i + 1) % numeroVertici;
    //     int verticeEsternoSuccessivo = numeroVertici + ((i + 1) % numeroVertici);

    //     int[] triangoloBasso = new int[] { verticeInterno, verticeEsterno, verticeInternoSuccessivo };
    //     int[] triangoloAlto = new int[] { verticeInternoSuccessivo, verticeEsterno, verticeEsternoSuccessivo };
    //     int[] faccia = triangoloBasso.Concat(triangoloAlto).ToArray();
    //     return faccia;
    // }

    // private Vector3[] CreazioneVertici()
    // {
    //     Vector3[] vertices = new Vector3[numeroVertici * 2];
    //     for (int i = 0; i < numeroVertici; i++)
    //     {
    //         float angleInRad = i * angoloFisso * Mathf.Deg2Rad + AngleRotation();
    //         vertices[i] = new Vector3(innerRange * Mathf.Cos(angleInRad), 0, innerRange * Mathf.Sin(angleInRad));
    //         vertices[i + numeroVertici] = new Vector3(outerRange * Mathf.Cos(angleInRad), 0, outerRange * Mathf.Sin(angleInRad));
    //     }
    //     string arrayString = string.Join(", ", vertices);
    //     Debug.Log("vertices: " + arrayString);
    //     return vertices;
    // }



}

public struct Face
{
    public List<Vector3> vertices { get; private set; }
    public List<int> triangles { get; private set; }
    public List<Vector2> uvs { get; private set; }
    public Face(List<Vector3> vertices, List<int> triangles, List<Vector2> uvs)
    {
        this.vertices = vertices;
        this.triangles = triangles;
        this.uvs = uvs;
    }
}
