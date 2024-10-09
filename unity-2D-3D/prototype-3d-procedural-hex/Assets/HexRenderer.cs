using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class HexRenderer : MonoBehaviour
{
    private Mesh m_mesh;
    private MeshFilter m_meshFilter;
    private MeshRenderer m_meshRenderer;
    [SerializeField] Material material;
    public float outerRange { get; set; } = 2.0f;
    public float innerRange { get; set; } = 1.0f;
    public float superiorHeight { get; set; } = 2.0f;
    public float inferiorHeight { get; set; } = 0.0f;

    //[SerializeField][Range(1, 10)] 
    int numberVertex = 6;
    //[SerializeField][Range(0.0f, 360.0f)] 
    float angleRotation = 30.0f;
    [SerializeField] bool applyRotation = false;



    private void Awake()
    {
        m_meshFilter = GetComponent<MeshFilter>();
        m_meshRenderer = GetComponent<MeshRenderer>();
        m_mesh = new Mesh();
        m_mesh.name = "Hex";

        m_meshFilter.mesh = m_mesh;
        m_meshRenderer.material = material;
    }

    private void Update()
    {
        DrawMesh();

    }
    public void DrawMesh()
    {
        List<Face> m_faces = new List<Face>(4);
        DisegnaSuperfici(m_faces);
        ComponiSuperfici(m_faces);
    }

    private void DisegnaSuperfici(List<Face> m_faces)
    {
        //questo metodo disegna le facce del volume e le inserisce nella lista di Facce m_faces
        m_faces.Add(CreaFacciaSuperiore());
        m_faces.Add(CreaFacciaInferiore());
        m_faces.Add(CreaFacciaLateraleInterna());
        m_faces.Add(CreaFacciaLateraleEsterna());
    }

    private void ComponiSuperfici(List<Face> m_faces)
    {
        List<Vector3> vertices = new List<Vector3>();
        List<int> triangles = new List<int>();
        // List<Vector2> uvs = new List<Vector2>();
        // Debug.Log("vertices:\n" + string.Join(", ", m_faces[0].vertices.Select(v => v.ToString())));
        for (int j = 0; j < m_faces.Count; j++)
        {
            vertices.AddRange(m_faces[j].vertices);
            triangles.AddRange(m_faces[j].triangles);
        }

        string verticesString = string.Join(", ", vertices);
        // Debug.Log("vertices: " + verticesString);
        string arrayTriangles = string.Join(", ", triangles);
        // Debug.Log("triangles: " + arrayTriangles);

        m_mesh.vertices = vertices.ToArray();
        m_mesh.triangles = triangles.ToArray();
        // m_mesh.uv = uvs.ToArray();
        m_mesh.RecalculateNormals();
    }

    private Face CreaFacciaLateraleEsterna()
    {
        List<Vector3> vertices = CalculateVertexArrayLateraleEsterna();
        List<int> triangles = DisegnaTriangoliLateraleEsterna();
        return new Face(vertices, triangles);
    }

    private List<int> DisegnaTriangoliLateraleEsterna()
    {
        return DisegnaTriangoli((int)FaceEnum.LATERALE_ESTERNA, false);
    }

    private List<Vector3> CalculateVertexArrayLateraleEsterna()
    {
        return CalculateVertexArrayLaterale(outerRange);
    }

    private Face CreaFacciaLateraleInterna()
    {
        List<Vector3> vertices = CalculateVertexArrayLateraleInterna();
        List<int> triangles = DisegnaTriangoliLateraleInterna();
        return new Face(vertices, triangles);
    }

    private List<int> DisegnaTriangoliLateraleInterna()
    {
        return DisegnaTriangoli((int)FaceEnum.LATERALE_INTERNA, true);
    }

    private List<Vector3> CalculateVertexArrayLateraleInterna()
    {
        return CalculateVertexArrayLaterale(innerRange);
    }

    private List<Vector3> CalculateVertexArrayLaterale(float range)
    {

        float angle = 360.0f / numberVertex * Mathf.Deg2Rad;

        List<Vector3> lowVertices = new List<Vector3>(numberVertex);
        List<Vector3> highVertices = new List<Vector3>(numberVertex);

        for (int i = 0; i < numberVertex; i++)
        {
            float angleInRad = i * angle + GetAngleRotation();
            //vertici interni inferiori
            lowVertices.Add(new Vector3(range * Mathf.Cos(angleInRad), inferiorHeight, range * Mathf.Sin(angleInRad)));
        }
        for (int i = 0; i < numberVertex; i++)
        {
            float angleInRad = i * angle + GetAngleRotation();
            //vertici interni superiori
            highVertices.Add(new Vector3(range * Mathf.Cos(angleInRad), superiorHeight, range * Mathf.Sin(angleInRad)));
        }

        lowVertices.AddRange(highVertices);
        return lowVertices;
    }

    Face CreaFacciaSuperiore()
    {
        // List<Vector3> vertices = ;
        // List<int> triangles = ;
        // string verticesString = string.Join(", ", vertices);
        // Debug.Log("vertici Superior: " + verticesString);
        // string arrayTriangles = string.Join(", ", triangles);
        // Debug.Log("triangoli Superiori: " + arrayTriangles);
        List<Vector3> returnvertices = CalculateVertexArraySuperiore();
        Face faccia = new Face(returnvertices, DisegnaTriangoliSuperiori());
        return faccia;
    }
    Face CreaFacciaInferiore()
    {
        List<Vector3> vertices = CalculateVertexArrayInferiore();
        List<int> triangles = DisegnaTriangoliInferiori();
        string verticesString = string.Join(", ", vertices);
        // Debug.Log("vertici Inferiori: " + verticesString);
        string arrayTriangles = string.Join(", ", triangles);
        // Debug.Log("triangoli Inferiori: " + arrayTriangles);
        return new Face(vertices, triangles);
    }

    private List<Vector3> CalculateVertexArraySuperiore()
    {

        return CalculateVertexArray(superiorHeight);
    }

    private List<Vector3> CalculateVertexArrayInferiore()
    {
        return CalculateVertexArray(inferiorHeight);
    }
    private List<Vector3> CalculateVertexArray(float height)
    {
        float angle = 360.0f / numberVertex * Mathf.Deg2Rad;
        List<Vector3> returnvertices = new List<Vector3>();
        List<Vector3> internalVertices = new List<Vector3>(numberVertex);
        List<Vector3> externalVertices = new List<Vector3>(numberVertex);

        for (int i = 0; i < numberVertex; i++)
        {
            float angleInRad = i * angle + GetAngleRotation();
            //vertici interni
            internalVertices.Add(new Vector3(innerRange * Mathf.Cos(angleInRad), height, innerRange * Mathf.Sin(angleInRad)));
        }
        for (int i = 0; i < numberVertex; i++)
        {
            float angleInRad = i * angle + GetAngleRotation();
            //vertici esterni
            externalVertices.Add(new Vector3(outerRange * Mathf.Cos(angleInRad), height, outerRange * Mathf.Sin(angleInRad)));
        }
        returnvertices.AddRange(internalVertices);
        returnvertices.AddRange(externalVertices);
        return returnvertices;
    }
    private List<int> DisegnaTriangoliSuperiori()
    {
        List<int> triangles = DisegnaTriangoli((int)FaceEnum.SUPERIORE, true);
        string arrayTriangles = string.Join(", ", triangles);
        // Debug.Log("DisegnaTriangoliSuperiori: " + arrayTriangles);
        return triangles;
    }
    private List<int> DisegnaTriangoliInferiori()
    {
        return DisegnaTriangoli((int)FaceEnum.INFERIORE, true);
    }
    private List<int> DisegnaTriangoli(int faceIndex, bool reverse)
    {
        // Debug.Log("faceIndex: " + faceIndex);
        List<int> triangles = new List<int>();
        int factor = faceIndex * 2 * numberVertex;
        // Debug.Log("factor: " + factor);
        for (int i = 0; i < numberVertex; i++)
        {
            int internalVertex = i;
            int externalVertex = numberVertex + internalVertex;
            int internalVertexSuccessivo = (i + 1) % numberVertex;
            int externalVertexSuccessivo = numberVertex + internalVertexSuccessivo;

            int[] lowTriangle = new int[] { internalVertex + factor, externalVertex + factor, internalVertexSuccessivo + factor };
            int[] highTriangle = new int[] { internalVertexSuccessivo + factor, externalVertex + factor, externalVertexSuccessivo + factor };

            // string arraylowTriangle = string.Join(", ", lowTriangle);
            // Debug.Log("arraylowTriangle: " + arraylowTriangle);
            // string arrayhighTriangle = string.Join(", ", highTriangle);
            // Debug.Log("arraylowTriangle: " + arrayhighTriangle);

            triangles.AddRange(lowTriangle);
            triangles.AddRange(highTriangle);
        }

        if (reverse) triangles.Reverse();
        return triangles;
    }

    private float GetAngleRotation()
    {
        return applyRotation ? (angleRotation * Mathf.Deg2Rad) : 0f;
    }



}

enum FaceEnum : int
{
    SUPERIORE = 0,
    INFERIORE = 1,
    LATERALE_INTERNA = 2,
    LATERALE_ESTERNA = 3
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
