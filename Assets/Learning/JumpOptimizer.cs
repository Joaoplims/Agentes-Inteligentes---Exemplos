using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class JumpOptimizer : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float jumpForce;
private Rigidbody rb;
    void Start()
    {
        float initialParameter = Random.Range(-100f, 100f) ;
        rb = GetComponent<Rigidbody>();
        // Define a função a ser otimizada (exemplo simples: soma dos quadrados)
        
        //jumpForce = HillClimbingOptimizer.HillClimbSingle(initialParameter, 20, EvaluateJumpForce(initialParameter, 7f));
    }

    public float EvaluateJumpForce(float parameter, float targetHeight)
    {
        // Supondo que parameters[0] é a força do pulo
        float jumpForce = parameter;

        // Simula o pulo com a força dada (você precisará implementar sua física real)
        float achievedHeight = SimulateJump(jumpForce);

        // Calcula a diferença para o alvo
        float heightDifference = Mathf.Abs(targetHeight - achievedHeight);

        // Quanto menor a diferença, melhor o resultado
        // Usamos negativo porque queremos minimizar a diferença
        return -heightDifference;

        // Alternativa com margem de erro aceitável:
        // float acceptableMargin = 0.1f;
        // if (heightDifference <= acceptableMargin) return achievedHeight;
        // else return -heightDifference;
    }

    private float SimulateJump(float jumpForce)
    {
        rb.AddForce(Vector3.up * jumpForce);
        return rb.position.y;
    }
}
