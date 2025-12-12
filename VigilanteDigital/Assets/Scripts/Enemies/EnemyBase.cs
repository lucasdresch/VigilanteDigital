using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using System.Linq;

public class EnemyBase : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 2f;
    public Transform[] waypoints;
    public int currentIndex = 0;

    protected bool canMove = true;
    public bool primaryWay = true;
    public List<AlternativeWays> AlternativeWays = new List<AlternativeWays>();
    public int CurrentAltWayIndex = 0;

    private void Update()
    {
        if (!canMove) return;

        if (waypoints != null && waypoints.Length > 0)
            FollowPath();
    }

    private void FollowPath()
    {
        Transform target = waypoints[currentIndex];
        transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, target.position) < 0.1f)
        {
            currentIndex++;
            if (currentIndex >= waypoints.Length)
            {
                Debug.Log(waypoints.Length);
                //CheckWay(CurrentAltWayIndex);
                CheckNewWays();
                currentIndex = 0;
            }
        }
        
    }

    // ---------------------------------------------------
    // ADD NEW WAYPOINTS FOR ALTERNATIVE WAYS
    // ---------------------------------------------------
    private void CheckWay(int wayIndex) { 

    }


    private void CheckNewWays() { 
        if (primaryWay) {
            waypoints = AlternativeWays[CurrentAltWayIndex].PrimaryWay.GetComponent<EnemyPath>().points.ToArray();
        }
        else if (!primaryWay) {
            waypoints = AlternativeWays[CurrentAltWayIndex].SecondaryWay.GetComponent<EnemyPath>().points.ToArray();

        }

    }

    // ---------------------------------------------------
    // SLEEP / STUN SYSTEM FOR GAS TRAPS
    // ---------------------------------------------------
    public void Sleep(float duration = 3f)
    {
        StartCoroutine(SleepRoutine(duration));
    }

    private IEnumerator SleepRoutine(float duration)
    {
        canMove = false;

        // Você pode trocar por animação futuramente
        float timer = 0f;
        while (timer < duration)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        canMove = true;
    }
    public void Stun(float duration = 3f)
    {
        StartCoroutine(StunRoutine(duration));
    }

    private IEnumerator StunRoutine(float duration)
    {
        canMove = false;

        // Você pode trocar por animação futuramente
        float timer = 0f;
        while (timer < duration)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        canMove = true;
    }

    public virtual void Move(Vector3 direction)
    {
        if (!canMove) return;

        transform.position += direction * moveSpeed * Time.deltaTime;
    }

    // Optional gizmos to visualize path
    private void OnDrawGizmos()
    {
        if (waypoints == null || waypoints.Length == 0)
            return;

        Gizmos.color = Color.yellow;
        for (int i = 0; i < waypoints.Length - 1; i++)
        {
            //sGizmos.DrawLine(waypoints[i].position, waypoints[i + 1].position);
        }
    }
}
[System.Serializable]
public class AlternativeWays {
    public GameObject PrimaryWay;
    public GameObject SecondaryWay;
}
