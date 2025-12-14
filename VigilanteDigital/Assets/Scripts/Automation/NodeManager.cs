using UnityEngine;
using System.Collections.Generic;

public class NodeManager : MonoBehaviour
{
    [Header("Nodes da cena")]
    public List<Node> allNodes = new List<Node>();

    [Header("Configurações do Gizmo")]
    public Color lineColor = Color.white;

    #region Singleton
    public static NodeManager Instance;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
    #endregion

    // ---------------------------------------------------
    // Buscar node por nome
    // ---------------------------------------------------
    public Node GetNodeByName(string nodeName)
    {
        foreach (var n in allNodes)
        {
            if (n.gameObject.name == nodeName)
                return n;
        }
        return null;
    }

    // ---------------------------------------------------
    // Resetar bloqueios de todas conexões
    // ---------------------------------------------------
    public void ResetConnections()
    {
        foreach (var n in allNodes)
        {
            foreach (var conn in n.connections)
            {
                conn.isBlocked = false;
            }
        }
    }

    // ---------------------------------------------------
    // Buscar node mais próximo de uma posição
    // útil para spawnar inimigos
    // ---------------------------------------------------
    public Node GetNearestNode(Vector3 position)
    {
        Node nearest = null;
        float minDist = Mathf.Infinity;

        foreach (var n in allNodes)
        {
            float d = Vector3.Distance(position, n.transform.position);
            if (d < minDist)
            {
                minDist = d;
                nearest = n;
            }
        }

        return nearest;
    }

    // ---------------------------------------------------
    // Gizmos para visualização
    // ---------------------------------------------------
    private void OnDrawGizmos()
    {
        if (allNodes == null || allNodes.Count == 0)
            return;

        foreach (var n in allNodes)
        {
            if (n.connections == null)
                continue;

            foreach (var conn in n.connections)
            {
                if (conn.targetNode == null)
                    continue;

                Gizmos.color = conn.lineColor;
                Gizmos.DrawLine(n.transform.position, conn.targetNode.transform.position);

#if UNITY_EDITOR
                UnityEditor.Handles.Label(n.transform.position + Vector3.up * 0.3f, n.gameObject.name);
#endif
            }
        }
    }
}
