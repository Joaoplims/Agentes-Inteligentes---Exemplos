
using System.Collections.Generic;
using UnityEngine;
namespace GOAP
{
    public abstract class GoalEvaluator
    {
        public abstract float CalculateDesirability(GameObject agent);
        public abstract IGoapElement CreateGoal(GameObject agent);
    }

    // Sistema GOAP principal
    public class GoapPlanner : MonoBehaviour
    {
        private IGoapElement currentGoal;
        private List<GoalEvaluator> evaluators = new List<GoalEvaluator>();

        void Start()
        {
            // Adiciona avaliadores (poderia ser configurável)
            evaluators.Add(new CollectItemEvaluator());
            evaluators.Add(new ExploreEvaluator());

            // Exemplo: Define uma meta inicial
            Arbitrate();
        }

        void Update()
        {
            if (currentGoal != null)
            {
                var status = currentGoal.Process();

                if (status == GoalStatus.Completed || status == GoalStatus.Failed)
                {
                    Debug.Log($"Current goal {currentGoal.GetType().Name} finished with status: {status}");
                    Arbitrate(); // Escolhe nova meta
                }
            }
        }

        public void Arbitrate()
        {
            IGoapElement bestGoal = null;
            float bestDesirability = 0f;

            foreach (var evaluator in evaluators)
            {
                float desirability = evaluator.CalculateDesirability(gameObject);
                if (desirability > bestDesirability)
                {
                    bestDesirability = desirability;
                    bestGoal = evaluator.CreateGoal(gameObject);
                }
            }

            if (bestGoal != null)
            {
                if (currentGoal != null)
                {
                    currentGoal.Terminate();
                }

                currentGoal = bestGoal;
                currentGoal.Activate();
                Debug.Log($"New goal selected: {currentGoal.GetType().Name} with desirability {bestDesirability}");
            }
        }
    }

    // Exemplo de avaliador para coletar itens
    public class CollectItemEvaluator : GoalEvaluator
    {
        public override float CalculateDesirability(GameObject agent)
        {
            // Simula cálculo de desejo baseado em fatores do jogo
            float health = 0.8f; // Supõe que o agente tem 80% de saúde
            float distanceToItem = 0.3f; // Distância normalizada (0 perto, 1 longe)

            // Fórmula simples: quanto mais saúde e mais perto, mais desejável
            return health * (1 - distanceToItem);
        }

        public override IGoapElement CreateGoal(GameObject agent)
        {
            // Posições de exemplo
            Vector3 itemPos = new Vector3(5, 0, 5);
            Vector3 storagePos = new Vector3(0, 0, 0);
            return new CollectItem(agent, itemPos, storagePos);
        }
    }

    // Exemplo de avaliador para explorar
    public class ExploreEvaluator : GoalEvaluator
    {
        public override float CalculateDesirability(GameObject agent)
        {
            // Exploração tem desejo constante baixo
            return 0.2f;
        }

        public override IGoapElement CreateGoal(GameObject agent)
        {
            // Meta simples de explorar (poderia ser mais complexa)
            return new MoveToPosition(agent, new Vector3(10, 0, 10));
        }
    }
}
