using System;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class HexRenderer : MonoBehaviour
{
    private Mesh m_mesh;
    private MeshFilter m_meshFilter;
    private MeshRenderer m_meshRenderer;
    [SerializeField] Material material;
    [SerializeField][Range(0.0f, 20.0f)] float outerRange = 2.0f;
    [SerializeField][Range(0.0f, 20.0f)] float innerRange = 1.0f;
    [SerializeField] bool applyRotation = false;
    [SerializeField][Range(0, 1000)] int numeroVertici = 6;
    float angoloFisso;
    [SerializeField][Range(0.0f, 360.0f)] float angleRotation = 30.0f;


    private void Awake()
    {

        m_meshFilter = this.AddComponent<MeshFilter>();
        m_meshRenderer = this.AddComponent<MeshRenderer>();
        m_mesh = new Mesh();
        m_mesh.name = "Hex";

        m_meshFilter.mesh = m_mesh;
        m_meshRenderer.material = material;
    }

    private void Update()
    {
        angoloFisso = 360.0f / numeroVertici;



        m_mesh.vertices = CreazioneVertici(); //creo i vertici prima gli interni e poi gli esterni
        m_mesh.triangles = CreazioneTriangoli(); //la superficie è divisa in 6 facce e ogni faccia composta da 2 triangoli. ogni triangolo ha 3 vertici
        Color[] colors = new Color[m_mesh.vertices.Length];

        for (int i = 0; i < m_mesh.vertices.Length; i++)
            colors[i] = Color.Lerp(Color.red, Color.green, m_mesh.vertices[0].y);

        m_mesh.colors = colors;
        m_mesh.RecalculateNormals();
    }

    private int[] CreazioneTriangoli()
    {
        int[] triangles = new int[0];
        for (int i = 0; i < numeroVertici; i++)
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
        int verticeEsterno = i + numeroVertici;
        int verticeInternoSuccessivo = (i + 1) % numeroVertici;
        int verticeEsternoSuccessivo = numeroVertici + ((i + 1) % numeroVertici);

        int[] triangoloBasso = new int[] { verticeInterno, verticeEsterno, verticeInternoSuccessivo };
        int[] triangoloAlto = new int[] { verticeInternoSuccessivo, verticeEsterno, verticeEsternoSuccessivo };
        int[] faccia = triangoloBasso.Concat(triangoloAlto).ToArray();
        return faccia;
    }

    private Vector3[] CreazioneVertici()
    {
        Vector3[] vertices = new Vector3[numeroVertici * 2];
        for (int i = 0; i < numeroVertici; i++)
        {
            float angleInRad = i * angoloFisso * Mathf.Deg2Rad + AngleRotation();
            vertices[i] = new Vector3(innerRange * Mathf.Cos(angleInRad), 0, innerRange * Mathf.Sin(angleInRad));
            vertices[i + numeroVertici] = new Vector3(outerRange * Mathf.Cos(angleInRad), 0, outerRange * Mathf.Sin(angleInRad));
        }
        string arrayString = string.Join(", ", vertices);
        Debug.Log("vertices: " + arrayString);
        return vertices;
    }


    private float AngleRotation()
    {
        return applyRotation ? (angleRotation * Mathf.Deg2Rad) : 0f;
    }
}
