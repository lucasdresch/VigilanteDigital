using UnityEngine;
public class EnemyPath : MonoBehaviour
{
    public Transform[] points;

    public Transform GetPoint(int index)
    {
        return points[index];
    }
}
