using System;
using System.Collections.Generic;
using UnityEngine;

public class HillClimbingOptimizer
{
    // Tamanho do passo para ajuste dos parâmetros
    public const float STEP = 0.1f;

    // Função para otimizar parâmetros locais
    public static float[] OptimizeParameters(float[] parameters, Func<float[], float> function)
    {
        // Armazena o melhor ajuste de parâmetro encontrado
        int bestParameterIndex = -1;
        float bestTweak = 0f;

        // O melhor valor inicial é o valor atual dos parâmetros
        float bestValue = function(parameters);

        // Percorre cada parâmetro
        for (int i = 0; i < parameters.Length; i++)
        {
            // Armazena o valor atual do parâmetro
            float currentParameter = parameters[i];

            // Ajusta para cima e para baixo
            foreach (float tweak in new float[] { -STEP, STEP })
            {
                // Aplica o ajuste
                parameters[i] += tweak;

                // Obtém o valor da função
                float value = function(parameters);

                // Verifica se é o melhor até agora
                if (value > bestValue)
                {
                    // Armazena os valores
                    bestValue = value;
                    bestParameterIndex = i;
                    bestTweak = tweak;
                }

                // Restaura o valor original do parâmetro
                parameters[i] = currentParameter;
            }
        }

        // Verifica se encontrou um conjunto melhor
        if (bestParameterIndex >= 0)
        {
            // Torna o ajuste permanente
            parameters[bestParameterIndex] += bestTweak;
        }

        // Retorna os parâmetros modificados (ou originais se não houve melhora)
        return parameters;
    }

    public static float OptimizeSingleParameter(float parameter, Func<float, float> function)
    {
        float bestTweak = 0f;
        float bestValue = function(parameter);
        float current = parameter;

        foreach (float tweak in new float[] { -STEP, STEP })
        {
            parameter += tweak;
            float value = function(parameter);

            if (value > bestValue)
            {
                bestValue = value;
                bestTweak = tweak;
            }

            parameter = current;
        }

        return current + bestTweak;
    }

    // Função de busca em colina (Hill Climbing)
    public static float[] HillClimb(float[] initialParameters, int steps, Func<float[], float> function)
    {
        // Define os parâmetros iniciais
        float[] parameters = (float[])initialParameters.Clone();

        // Obtém o valor inicial
        float value = function(parameters);

        // Executa o número especificado de passos
        for (int i = 0; i < steps; i++)
        {
            // Obtém os novos parâmetros
            float[] newParameters = OptimizeParameters(parameters, function);

            // Obtém o novo valor
            float newValue = function(newParameters);

            // Se não houve melhora, termina
            if (newValue <= value)
            {
                break;
            }

            // Armazena os novos valores para a próxima iteração
            parameters = newParameters;
            value = newValue;
        }

        // Retorna os parâmetros otimizados
        return parameters;
    }
    public static float HillClimbSingle(float initialParameter, int steps, Func<float, float> evaluationFunc)
    {
        float value = evaluationFunc(initialParameter);
        float parameter = initialParameter;
        // Executa o número especificado de passos
        for (int i = 0; i < steps; i++)
        {
            // Obtém os novos parâmetros
            float newParameter = OptimizeSingleParameter(parameter, evaluationFunc);

            // Obtém o novo valor
            float newValue = evaluationFunc(newParameter);

            // Se não houve melhora, termina
            if (newValue <= value)
            {
                break;
            }

            // Armazena os novos valores para a próxima iteração
            parameter = newParameter;
            value = newValue;
        }

        return parameter;
    }
}