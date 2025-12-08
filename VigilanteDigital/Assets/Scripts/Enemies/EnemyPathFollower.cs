using UnityEngine;

public class EnemyPathFollower : MonoBehaviour
{
    public EnemyBase enemy;
    public EnemyPath path;
    public int currentPoint = 0;

    void Update()
    {
        if (path == null || enemy == null) return;

        Vector3 target = path.GetPoint(currentPoint).position;
        Vector3 dir = (target - transform.position).normalized;
        enemy.Move(dir);

        if (Vector3.Distance(transform.position, target) < 0.1f)
        {
            currentPoint = (currentPoint + 1) % path.points.Length;
        }
    }
}

