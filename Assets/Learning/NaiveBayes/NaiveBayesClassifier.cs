using System;

public class NaiveBayesClassifier
{
    private int positiveExamples;    // Número de exemplos positivos (m)
    private int negativeExamples;   // Número de exemplos negativos (n)
    private int[] trueCountsPositive; // Contagem de atributos verdadeiros na classe positiva
    private int[] trueCountsNegative; // Contagem de atributos verdadeiros na classe negativa
    private readonly int numAttributes;

    public NaiveBayesClassifier(int numberOfAttributes)
    {
        numAttributes = numberOfAttributes;
        trueCountsPositive = new int[numAttributes];
        trueCountsNegative = new int[numAttributes];
        positiveExamples = 0;
        negativeExamples = 0;
    }

    public void Update(bool[] attributes, bool isPositive)
    {
        if (attributes.Length != numAttributes)
        {
            throw new ArgumentException($"Número incorreto de atributos. Esperado: {numAttributes}, Recebido: {attributes.Length}");
        }

        if (isPositive)
        {
            for (int i = 0; i < numAttributes; i++)
            {
                if (attributes[i])
                {
                    trueCountsPositive[i]++;
                }
            }
            positiveExamples++;
        }
        else
        {
            for (int i = 0; i < numAttributes; i++)
            {
                if (attributes[i])
                {
                    trueCountsNegative[i]++;
                }
            }
            negativeExamples++;
        }
    }

    public bool Predict(bool[] attributes)
    {
        if (positiveExamples == 0 || negativeExamples == 0)
        {
            throw new InvalidOperationException("O classificador precisa de exemplos de ambas as classes para prever");
        }

        double probPositive = CalculateProbability(attributes, trueCountsPositive, positiveExamples, negativeExamples);
        double probNegative = CalculateProbability(attributes, trueCountsNegative, negativeExamples, positiveExamples);

        return probPositive >= probNegative;
    }

    private double CalculateProbability(bool[] attributes, int[] trueCounts, int classExamples, int otherClassExamples)
    {
        double prior = CalculatePrior(classExamples, classExamples + otherClassExamples);
        double likelihood = 1.0;

        for (int i = 0; i < numAttributes; i++)
        {
            double probability;
            
            if (classExamples == 0)
            {
                probability = 0.5; // Fallback para evitar divisão por zero
            }
            else if (attributes[i])
            {
                probability = (double)trueCounts[i] / classExamples;
            }
            else
            {
                probability = (double)(classExamples - trueCounts[i]) / classExamples;
            }

            likelihood *= probability;
        }

        return prior * likelihood;
    }

    private float CalculatePrior(int classExamples, int totalExamples)
    {
        return (float)classExamples / totalExamples;
    }

    // Versão com suavização de Laplace (k=1 por padrão)
    public bool PredictWithSmoothing(bool[] attributes, double k = 1.0)
    {
        double probPositive = CalculateProbabilityWithSmoothing(attributes, trueCountsPositive, 
                                                             positiveExamples, negativeExamples, k);
        double probNegative = CalculateProbabilityWithSmoothing(attributes, trueCountsNegative, 
                                                             negativeExamples, positiveExamples, k);

        return probPositive >= probNegative;
    }

    private double CalculateProbabilityWithSmoothing(bool[] attributes, int[] trueCounts, 
                                                  int classExamples, int otherClassExamples, double k)
    {
        float prior = CalculatePrior(classExamples + k, classExamples + otherClassExamples + 2*k);
        float likelihood = 1.0f;

        for (int i = 0; i < numAttributes; i++)
        {
            float probability;
            
            if (attributes[i])
            {
                probability = (trueCounts[i] + k) / (classExamples + 2*k);
            }
            else
            {
                probability = (classExamples - trueCounts[i] + k) / (classExamples + 2*k);
            }

            likelihood *= probability;
        }

        return prior * likelihood;
    }
}