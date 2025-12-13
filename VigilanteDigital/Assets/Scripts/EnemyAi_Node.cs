using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyAI_Node : MonoBehaviour
{
    [Header("Movimento")]
    public float moveSpeed = 2f;

    [Header("Nodes")]
    public Node currentNode;                // node em que o inimigo está atualmente
    private Node targetNode;                // próximo node para onde o inimigo vai
    private bool isMoving = false;

    [Header("Estados")]
    public bool reachedGoal = false;        // se chegou na sala principal
    public bool exited = false;             // se saiu da cena

    [Header("Pathfinding")]
    private bool goToExitAfterGoal = false;  // indica que inimigo deve buscar o exitNode
    private Queue<Node> pathToExit = new Queue<Node>(); // nodes intermediarios até exit

    private void Start()
    {
        // registra o inimigo no GameManager para que ele seja contado
        if (GameManager.Instance != null)
            GameManager.Instance.RegisterEnemy(this);
        else
            Debug.LogWarning("GameManager.Instance é nulo — registre um GameManager na cena.");

        if (currentNode != null)
            ChooseNextNode();
        else
            Debug.LogWarning($"Enemy '{name}' não tem currentNode definido!");
    }
    private void OnDestroy()
    {
        // DESREGISTRA NO GAME MANAGER (ADICIONADO)
        if (GameManager.Instance != null)
            GameManager.Instance.UnregisterEnemy(this);
    }

    private void Update()
    {
        if (!isMoving || targetNode == null) return;

        MoveTowardsTarget();
    }

    private void MoveTowardsTarget()
    {
        Vector3 dir = (targetNode.transform.position - transform.position).normalized;
        transform.position += (Vector3)dir * moveSpeed * Time.deltaTime;
        /*
        if (Vector2.Distance(transform.position, targetNode.transform.position) < 0.05f)
        {
            currentNode = targetNode;
            ChooseNextNode();
        }//*/
        if (Vector2.Distance(transform.position, targetNode.transform.position) < 0.05f)
        {
            currentNode = targetNode;

            // verifica se chegou na sala principal
            if (currentNode.isGoal && !reachedGoal)
            {
                reachedGoal = true;
                // DANO AO PLAYER (MANTEVE SEU CÓDIGO)
                GameManager.Instance.PlayerTakeDamage(1);
                //targetNode = currentNode.exitNode;  // define próximo node como saída

                // depois do dano vai para o exitNode do Goal
                if (currentNode.exitNode != null)
                {
                    goToExitAfterGoal = true;
                    pathToExit = FindPath(currentNode, currentNode.exitNode);
                    if (pathToExit.Count > 0)
                    {
                        targetNode = pathToExit.Dequeue();
                    }
                    else
                    {
                        Debug.LogWarning("Não foi possivel gerar caminho até exitNode!");
                    }
                }
                else
                    Debug.LogWarning("Node de Goal NÃO tem exitNode configurado!");

                return;
            }

            // verifica se chegou na saída
            if (currentNode.isExit)
            {
                exited = true;
                // remove do GameManager antes de destruir para evitar dangling refs
                if (GameManager.Instance != null)
                    GameManager.Instance.UnregisterEnemy(this);

                Destroy(gameObject); // inimigo sai da cena
                return;
            }
            ChooseNextNode();
        }
    }

    // ↓↓↓ ADICIONADO – Esse método agora serve para ativar movimento
    public void StartFollowing()
    {
        isMoving = true;

        if (currentNode != null && targetNode == null)
            ChooseNextNode();
    }

    private void ChooseNextNode()
    {
        // se o inimigo está seguindo caminho para exit, pega próximo node do path
        if (goToExitAfterGoal && pathToExit != null && pathToExit.Count > 0)
        {
            targetNode = pathToExit.Dequeue();
            return;
        }

        // caso normal: escolhe próximo node baseado em peso
        if (currentNode == null || currentNode.connections.Count == 0)
        {
            targetNode = null;
            isMoving = false;
            return;
        }


    /*    // filtra nodes bloqueados
        List<Node> availableNodes = new List<Node>();
        foreach (var conn in currentNode.connections)
        {
            if (!conn.isBlocked)
                availableNodes.Add(conn.targetNode);
        }

        if (availableNodes.Count == 0)
        {
            // se todos bloqueados, para aqui
            targetNode = null;
            isMoving = false;
            return;
        }//*/

        // Pega todas as conexões disponíveis do node atual
        List<NodeConnection> availableConnections = new List<NodeConnection>();

        foreach (var conn in currentNode.connections)
        {
            if (!conn.isBlocked && conn.targetNode != null)
                availableConnections.Add(conn);
        }

        // Se não houver nenhuma conexão disponível, fica no node atual
        if (availableConnections.Count == 0)
        {
            targetNode = null;
            isMoving = false;
            return;
        }

        // Escolhe baseado em prioridade/peso
        float totalWeight = 0f;
        foreach (var conn in availableConnections)
            totalWeight += conn.weight; // peso de 0 a 100

        float rnd = Random.Range(0f, totalWeight);
        float sum = 0f;

        foreach (var conn in availableConnections)
        {
            sum += conn.weight;
            if (rnd <= sum)
            {
                targetNode = conn.targetNode; // pega o node destino da conexão
                return;
            }
        }

        // fallback
        //targetNode = availableNodes[0];
        targetNode = availableConnections[0].targetNode;
    }

    // ------------------------
    // BFS para encontrar caminho de nodes
    // ------------------------
    private Queue<Node> FindPath(Node start, Node end)
    {
        Queue<Node> resultPath = new Queue<Node>();
        Dictionary<Node, Node> cameFrom = new Dictionary<Node, Node>();
        Queue<Node> frontier = new Queue<Node>();
        HashSet<Node> visited = new HashSet<Node>();

        frontier.Enqueue(start);
        visited.Add(start);

        while (frontier.Count > 0)
        {
            Node current = frontier.Dequeue();
            if (current == end) break;

            foreach (var conn in current.connections)
            {
                Node neighbor = conn.targetNode;
                if (neighbor != null && !visited.Contains(neighbor) && !conn.isBlocked)
                {
                    visited.Add(neighbor);
                    frontier.Enqueue(neighbor);
                    cameFrom[neighbor] = current;
                }
            }
        }

        // reconstrói caminho
        Node temp = end;
        if (!cameFrom.ContainsKey(end))
            return resultPath; // caminho não encontrado

        Stack<Node> stack = new Stack<Node>();
        while (temp != start)
        {
            stack.Push(temp);
            temp = cameFrom[temp];
        }

        // converte stack para fila para percorrer na ordem
        while (stack.Count > 0)
            resultPath.Enqueue(stack.Pop());

        return resultPath;
    }
}
