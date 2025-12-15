using UnityEngine;

public class DoorController : MonoBehaviour
{
    [Header("Consumos de energia")]
    public float consumoBaixo = 1f;
    public float consumoNormal = 2f;
    public float consumoAlto = 4f;

    [Header("Configurações")]
    public bool modoInvertido = false;
    public bool controladoPorCamera = false;

    private bool algoNoTrigger = false;
    private bool inimigoVistoPorCamera = false;

    private bool portaForcadaFechada = false;

    private BoxCollider2D trigger;

    private void Start()
    {
         trigger = gameObject.GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        // --- PRIORIDADE 0: FORÇADO PELO JOGADOR ---
        if (portaForcadaFechada)
        {
            Fechar();
            Consumir(consumoNormal);
            return;
        }

        // --- PRIORIDADE 1: CÂMERA ---
        if (controladoPorCamera && inimigoVistoPorCamera)
        {
            Fechar();
            Consumir(consumoBaixo);
            return;
        }

        // --- PRIORIDADE 2: MODO INVERTIDO ---
        if (modoInvertido && algoNoTrigger)
        {
            Fechar();
            Consumir(consumoAlto);
            return;
        }

        // --- PRIORIDADE 3: MODO NORMAL ---
        if (algoNoTrigger)
        {
            Abrir();
            Consumir(consumoNormal);
        }
        else
        {
            Fechar();
        }


    }

    private void Consumir(float amount)
    {
        GameManager.Instance.ConsumeEnergy(amount * Time.deltaTime);
    }

    private void Abrir()
    {
        // TODO: animação ou lógica visual
        // Apenas debug por enquanto
        Debug.Log("Abrindo porta");
    }

    private void Fechar()
    {
        // TODO: animação ou lógica visual
        // Debug.Log("Fechando porta");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Inimigo"))
            algoNoTrigger = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Inimigo"))
            algoNoTrigger = false;
    }

    // Chamado pela câmera
    public void SetCameraDetection(bool detected)
    {
        inimigoVistoPorCamera = detected;
    }

    public void ToggleDoor()
    {
        portaForcadaFechada = !portaForcadaFechada;

        Debug.Log(
            $"Porta '{name}' forçada {(portaForcadaFechada ? "FECHADA" : "AUTOMÁTICA")}"
        );
    }

}
