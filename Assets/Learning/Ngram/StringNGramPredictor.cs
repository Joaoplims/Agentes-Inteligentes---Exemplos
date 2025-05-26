using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class StringNGramPredictor
{
    private readonly Dictionary<string, Dictionary<string, int>> nGramData;
    private readonly int nValue;

    public StringNGramPredictor(int nValue = 2)
    {
        this.nValue = nValue;
        this.nGramData = new Dictionary<string, Dictionary<string, int>>();
    }

    public void RegisterSequence(params string[] sequence)
    {
        if (sequence.Length < nValue) return;

        // Converte para lista para facilitar manipulação
        var tokens = sequence.ToList();

        // Adiciona tokens de início/fim para melhorar previsões
        tokens.Insert(0, "<start>");
        tokens.Add("<end>");

        for (int i = 0; i <= tokens.Count - nValue; i++)
        {
            string context = string.Join(" ", tokens.GetRange(i, nValue - 1));
            string nextToken = tokens[i + nValue - 1];

            if (!nGramData.ContainsKey(context))
            {
                nGramData[context] = new Dictionary<string, int>();
            }

            if (!nGramData[context].ContainsKey(nextToken))
            {
                nGramData[context][nextToken] = 0;
            }

            nGramData[context][nextToken]++;
        }
    }

    public string PredictNextToken(string inputContext)
    {
        // Preprocessa o contexto de entrada
        string context = inputContext.Trim();

        // Se o contexto for vazio, usa o token de início
        if (string.IsNullOrEmpty(context))
        {
            context = "<start>";
        }
        // Se o contexto tiver mais tokens que nValue-1, pega os últimos
        else
        {
            var tokens = context.Split(' ');
            if (tokens.Length > nValue - 1)
            {
                context = string.Join(" ", tokens.Skip(tokens.Length - (nValue - 1)));
            }
        }

        if (nGramData.TryGetValue(context, out var possibleTokens))
        {
            return possibleTokens
                .OrderByDescending(kvp => kvp.Value)
                .FirstOrDefault().Key;
        }

        // Fallback: retorna um token padrão ao invés de null
        return "<unk>"; // unknown token
    }

    public void DebugPrintModel()
    {
        foreach (var context in nGramData)
        {
            Debug.Log($"Contexto: '{context.Key}'");
            foreach (var prediction in context.Value.OrderByDescending(x => x.Value))
            {
                Debug.Log($"- {prediction.Key}: {prediction.Value} ocorrências");
            }
        }
    }
}