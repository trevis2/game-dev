using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMover : MonoBehaviour
{
    [SerializeField] List<Waypoint> path = new List<Waypoint>();
    [SerializeField][Range(0f, 5f)] float speed = 3f;
    float oldSpeed = 0f;
    bool isPressedPause = false;
    //[SerializeField] GameObject panelPause;
    Enemy enemy;

    void OnEnable()
    {
        //panelPause = GameObject.FindWithTag("PausePanel");
        FindPath();
        ReturnToStart();
        StartCoroutine(FollowPath());
    }
    private void Start()
    {
        enemy = GetComponent<Enemy>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (!isPressedPause)
            {
                //panelPause.SetActive(true);
                oldSpeed = speed;
                speed = 0;
                isPressedPause = true;
            }
            else
            {
                //panelPause.SetActive(false);
                speed = oldSpeed;
                isPressedPause = false;
            }
        }
    }

    void FindPath()
    {
        path.Clear();
        GameObject parent = GameObject.FindGameObjectWithTag("Path");
        foreach (Transform child in parent.transform)
        {
            Waypoint waypoint = child.GetComponent<Waypoint>();
            if (waypoint != null)
            {
                path.Add(waypoint);
            }
        }
    }

    void ReturnToStart()
    {
        transform.position = path[0].transform.position;
    }

    void FinishPath()
    {
        enemy.StealGold();
        gameObject.SetActive(false);

    }
    public void IncreaseSpeed()
    {
        speed += enemy.DifficultyRamp;
    }

    IEnumerator FollowPath()
    {
        foreach (Waypoint waypoint in path)
        {
            Vector3 startPosition = transform.position;
            Vector3 endPosition = waypoint.transform.position;
            float travelPercent = 0f;
            transform.LookAt(endPosition);
            while (travelPercent < 1f)
            {
                travelPercent += Time.deltaTime * speed;
                transform.position = Vector3.Lerp(startPosition, endPosition, travelPercent);
                yield return new WaitForEndOfFrame();
            }
        }
        FinishPath();
    }
}
