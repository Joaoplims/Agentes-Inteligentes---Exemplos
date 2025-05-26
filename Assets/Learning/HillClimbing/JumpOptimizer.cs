using System;
using UnityEngine;
using Random = UnityEngine.Random;
public class JumpOptimizer : MonoBehaviour
{
    public float optimalJumpForce;
    private Rigidbody rb;
    private bool simulationRunning = true;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        float initialParameter = Random.Range(5f, 15f);

        optimalJumpForce = HillClimbingOptimizer.HillClimbSingle(
            initialParameter,
            200,
            param => EvaluateJumpForce(param, 7f));

        Debug.Log($"Força ótima encontrada: {optimalJumpForce}");
    }

    public float EvaluateJumpForce(float parameter, float targetHeight)
    {
        if (!simulationRunning) return 0f;

        // Simulação teórica sem afetar o Rigidbody real
        float achievedHeight = (parameter * parameter) / (2 * Mathf.Abs(Physics.gravity.y));
        float heightDifference = Mathf.Abs(targetHeight - achievedHeight);

        return -heightDifference;
    }

    // Chamar este método quando quiser executar o pulo real
    [ContextMenu("ExecuteOptimalJump")]
    public void ExecuteOptimalJump()
    {
        simulationRunning = false;
        rb.AddForce(Vector3.up * optimalJumpForce, ForceMode.Impulse);
    }
}