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
        // Define a fun��o a ser otimizada (exemplo simples: soma dos quadrados)
        
        //jumpForce = HillClimbingOptimizer.HillClimbSingle(initialParameter, 20, EvaluateJumpForce(initialParameter, 7f));
    }

    public float EvaluateJumpForce(float parameter, float targetHeight)
    {
        // Supondo que parameters[0] � a for�a do pulo
        float jumpForce = parameter;

        // Simula o pulo com a for�a dada (voc� precisar� implementar sua f�sica real)
        float achievedHeight = SimulateJump(jumpForce);

        // Calcula a diferen�a para o alvo
        float heightDifference = Mathf.Abs(targetHeight - achievedHeight);

        // Quanto menor a diferen�a, melhor o resultado
        // Usamos negativo porque queremos minimizar a diferen�a
        return -heightDifference;

        // Alternativa com margem de erro aceit�vel:
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
