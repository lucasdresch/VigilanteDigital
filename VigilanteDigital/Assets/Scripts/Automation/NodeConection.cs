using UnityEngine;

public enum ConnectionType
{
    Forward,
    Backward,
    Left,
    Right,
    Other
}

[System.Serializable]
public class NodeConnection
{
    public Node targetNode;

    [Tooltip("Tipo de direção / semântica da conexão")]
    public ConnectionType connectionType = ConnectionType.Other;

    [Tooltip("Chance/peso de escolher esta conexão (0 a 100)")]
    [Range(0, 100)]
    public int weight = 100;

    [Tooltip("Cor da linha no Gizmo")]
    public Color lineColor = Color.white;

    [Tooltip("Se a conexão está temporariamente bloqueada")]
    public bool isBlocked = false;

}
