using UnityEngine;

public class NgramExample : MonoBehaviour
{

    void Start()
    {
        var predictor = new StringNGramPredictor(nValue: 3); // Trigram model

        // 1. Treinar com sequ�ncias reais
        //predictor.RegisterSequence("atacar", "com", "espada");
        //predictor.RegisterSequence("atacar", "com", "magia");
        //predictor.RegisterSequence("defender", "com", "escudo");
        //predictor.RegisterSequence("usar", "po��o", "de", "cura");
        TrainPredictor(predictor);

        // 2. Verificar o modelo (para debug)
        predictor.DebugPrintModel();

        // 3. Fazer previs�es CORRETAMENTE
        string context = "atacar com"; // Contexto deve ter nValue-1 tokens
        string prediction = predictor.PredictNextToken(context);

        Debug.Log($"Previs�o para '{context}': {prediction}");
    }

    // Exemplo com dados mais robustos:
    void TrainPredictor(StringNGramPredictor predictor)
    {
        predictor.RegisterSequence("ir", "para", "norte");
        predictor.RegisterSequence("ir", "para", "sul");
        predictor.RegisterSequence("atacar", "o", "inimigo");
        predictor.RegisterSequence("pegar", "o", "tesouro");
        predictor.RegisterSequence("abrir", "o", "ba�");
    }
}