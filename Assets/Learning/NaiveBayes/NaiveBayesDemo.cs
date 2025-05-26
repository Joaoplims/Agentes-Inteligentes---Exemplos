using UnityEngine;
public class NaiveBayesDemo : MonoBehaviour
{
    void Start()
    {
        // 1. Cria o classificador (3 atributos)
        var classifier = new NaiveBayesClassifier(3);
        
        // 2. Treina com exemplos
        // Atributos: [Chovendo, Frio, Nublado]
        // Label: Brincar ao ar livre? (True = Sim, False = Não)
        classifier.Update(new bool[] {true, false, true}, false);
        classifier.Update(new bool[] {false, true, false}, false);
        classifier.Update(new bool[] {false, true, true}, false);
        classifier.Update(new bool[] {true, false, false}, true);
        classifier.Update(new bool[] {false, false, false}, true);
        
        // 3. Faz previsões
        bool[] hoje = new bool[] {false, false, true}; // Não chove, Não está frio, Nublado
        bool vamosBrincar = classifier.Predict(hoje);
        
        Debug.Log($"Vamos brincar ao ar livre hoje? {vamosBrincar}");
        
        // Com suavização
        bool vamosBrincarSmooth = classifier.PredictWithSmoothing(hoje);
        Debug.Log($"Com suavização: {vamosBrincarSmooth}");
    }
}