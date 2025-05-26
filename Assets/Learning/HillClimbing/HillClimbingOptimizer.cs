using System;
using System.Collections.Generic;
using UnityEngine;

public class HillClimbingOptimizer
{
    public const float STEP = 0.1f;

    public static float OptimizeSingleParameter(float parameter, Func<float, float> EvaluateFunc)
    {
        float bestTweak = 0f;
        float bestValue = EvaluateFunc(parameter);
        float currentParameter = parameter;

        foreach (float tweak in new float[] { -STEP, STEP })
        {
            float testParameter = parameter + tweak; // Não modifica o original
            float value = EvaluateFunc(testParameter);

            if (value > bestValue)
            {
                bestValue = value;
                bestTweak = tweak;
            }
        }

        return parameter + bestTweak; // Retorna o parâmetro otimizado
    }

    public static float HillClimbSingle(float initialParameter, int steps, Func<float, float> evaluationFunc)
    {
        float value = evaluationFunc(initialParameter);
        float parameter = initialParameter;

        for (int i = 0; i < steps; i++)
        {
            float newParameter = OptimizeSingleParameter(parameter, evaluationFunc);
            float newValue = evaluationFunc(newParameter);

            // Adiciona um pequeno limiar para evitar parada prematura
            if (newValue <= value + 0.001f) // Limiar de 0.001
            {
                Debug.Log($"Parando na iteração {i} - Melhora insuficiente");
                break;
            }

            parameter = newParameter;
            value = newValue;
            Debug.Log($"Iteração {i}: Força = {parameter}, Altura = {-newValue}");
        }

        return parameter;
    }
}