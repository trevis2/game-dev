using UnityEngine;

public class MapGeneration : MonoBehaviour
{
    [SerializeField] GameObject hex;
    [SerializeField] int xElements;
    [SerializeField] int yElements;
    // Start is called before the first frame update
    void Start()
    {
        CreaGriglia();
    }

    private void CreaGriglia()
    {
        for (int riga = 0; riga < yElements; riga++)
            for (float colonna = 0; colonna < xElements; colonna++)
            {
                hex.GetComponent<SpriteRenderer>().color = new Color(Random.Range(0f, 255f), Random.Range(0f, 255f), Random.Range(0f, 255f), Random.Range(0f, 1f));
                Instantiate(hex, new Vector3((colonna * 1.5f) + segno(riga) * 0.75f, riga * 0.5f * 0.866f, 0), Quaternion.identity, this.transform);
            }
    }


    private int segno(int riga)
    {
        if (riga % 2 == 0)
        {
            return 0;
        }
        else
        {
            return 1;
        }
    }
}
