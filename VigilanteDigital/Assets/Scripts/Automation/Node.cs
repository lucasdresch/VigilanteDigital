using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
public class Node : MonoBehaviour
{
    [Header("Configurações Visuais")]
    public Color nodeColor = Color.yellow;
    public float labelSize = 14f;

    [Space(10)]
    public List<NodeConnection> connections = new List<NodeConnection>();

    [Header("Tipos de Node")]
    public bool isGoal = false;       // Sala principal onde gera dano
    public bool isExit = false;       // Node de saída final
    public Node exitNode = null;      // Para onde ir após causar dano (só usado se isGoal = true)

    private void OnDrawGizmos()
    {
        // Desenhar o nó (um cubo pequeno)
        Gizmos.color = nodeColor;
        Gizmos.DrawSphere(transform.position, 0.2f);

        // Nome com contorno
        DrawLabelWithOutline(transform.position + (Vector3.up * 0.4f), gameObject.name, Color.white, Color.black, labelSize);

        // Desenhar conexões
        if (connections == null) return;

        foreach (var c in connections)
        {
            if (c == null || c.targetNode == null) continue;

            Vector3 start = transform.position;
            Vector3 end = c.targetNode.transform.position;

            // Linha colorida da conexão
            Gizmos.color = c.lineColor;
            Gizmos.DrawLine(start, end);

            // Desenhar seta direcional
            DrawArrow(end, start, c.lineColor);

        }
    }


    // ---------- DESENHO DE SETA ----------
    private void DrawArrow(Vector3 to, Vector3 from, Color color)
    {
        Gizmos.color = color;
        Vector3 dir = (to - from).normalized;

        float size = 0.25f;

        Vector3 right = Quaternion.LookRotation(dir) * Quaternion.Euler(0, 150, 0) * new Vector3(0, 0, 1);
        Vector3 left = Quaternion.LookRotation(dir) * Quaternion.Euler(0, -150, 0) * new Vector3(0, 0, 1);

        Gizmos.DrawLine(to, to + right * size);
        Gizmos.DrawLine(to, to + left * size);
    }


    // ---------- TEXTO COM CONTORNO ----------
    // Substitua somente esta função no seu Node.cs
    private void DrawLabelWithOutline(Vector3 worldPosition, string text, Color textColor, Color outlineColor, float size)
    {
#if UNITY_EDITOR
        // Ajusta a posição para ficar logo acima do node
        Vector3 labelPos = worldPosition ;

        // Desenho do label no SceneView
        Handles.color = textColor;
        GUIStyle style = new GUIStyle();
        style.fontSize = Mathf.RoundToInt(size);
        style.alignment = TextAnchor.MiddleCenter;

        // contorno simples: 4 offsets
        style.normal.textColor = outlineColor;
        Handles.Label(labelPos + Vector3.up * 0.01f, text, style);
        Handles.Label(labelPos + Vector3.down * 0.01f, text, style);
        Handles.Label(labelPos + Vector3.left * 0.01f, text, style);
        Handles.Label(labelPos + Vector3.right * 0.01f, text, style);

        // texto principal
        style.normal.textColor = textColor;
        Handles.Label(labelPos, text, style);
#endif
    }

}

