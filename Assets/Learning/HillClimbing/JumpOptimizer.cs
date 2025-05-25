using UnityEngine;

public class JumpOptimizer : MonoBehaviour
{
    public float jumpForce = 5f;      // Força inicial do pulo
    public float targetHeight = 4f;   // Altura da plataforma-alvo
    public float stepSize = 0.5f;     // Tamanho do ajuste
    public float maxForce = 10f;      // Força máxima
    public float minForce = 2f;       // Força mínima

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        OptimizeJump();
        targetHeight = GameObject.Find("Target").transform.position.y;
        InvokeRepeating(nameof(TestJump),0f,1f);
    }

    // Simula o pulo e retorna a altura alcançada
    float SimulateJump(float force)
    {
        rb.linearVelocity = new Vector2(0, force);
        float height = rb.position.y;
        return height;
    }

    void OptimizeJump()
    {
        float currentHeight = SimulateJump(jumpForce);
        float newForce = jumpForce + stepSize;

        // Limita o valor entre minForce e maxForce
        newForce = Mathf.Clamp(newForce, minForce, maxForce);
        float newHeight = SimulateJump(newForce);

        // Se a nova altura for mais próxima do alvo, aceita a mudança
        if (Mathf.Abs(newHeight - targetHeight) < Mathf.Abs(currentHeight - targetHeight))
        {
            jumpForce = newForce;
            Debug.Log($"Melhor ajuste: Força = {jumpForce}, Altura = {newHeight}");
        }
        else
        {
            // Tenta um passo na direção oposta
            newForce = jumpForce - stepSize;
            newHeight = SimulateJump(newForce);
            if (Mathf.Abs(newHeight - targetHeight) < Mathf.Abs(currentHeight - targetHeight))
            {
                jumpForce = newForce;
                Debug.Log($"Melhor ajuste: Força = {jumpForce}, Altura = {newHeight}");
            }
        }
    }

    // Chamado a cada pulo (pode ser ativado por um botão UI)
    public void TestJump()
    {
        rb.linearVelocity = new Vector2(0, jumpForce);
    }
}