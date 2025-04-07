using Unity.Burst;
using UnityEngine;

public class DecisionTreeController : MonoBehaviour
{
    public AIBlackBoard blackboard;

    private DecisionTreeNode rootDecision;

    private void Start()
    {
        // Build the decision tree
        BuildDecisionTree();

        // Run the decision tree
        RunDecisionTree();
    }

    private void BuildDecisionTree()
    {
        // Actions
        ActionNode restAction = new ActionNode("Rest (Low Health)");

        // Combat decision (based on enemy count)
        ActionNode[] combatActions = new ActionNode[]
        {
        new ActionNode("Patrol (Healthy)"),
        new ActionNode("Attack (1 Enemy)"),
        new ActionNode("Attack (2 Enemies)"),
        };
        // Health decision (if health is below 30, rest; otherwise, patrol)

        MultiDecisionNode combatDecision = new MultiDecisionNode(combatActions);

        FloatDecisionNode healthDecision = new FloatDecisionNode(
           min: 0f,
           max: 30f,
           trueBranch: restAction,
           falseBranch: combatDecision
       );

        rootDecision = healthDecision;


    }

    private void RunDecisionTree()
    {
        DecisionTreeNode result = rootDecision.MakeDecision(blackboard);

        if (result is ActionNode actionResult)
        {
            actionResult.Execute();
        }
        else if (result is DecisionNode decisionNode)
        {
            Debug.Log("Oie, decision");
            var d = decisionNode.MakeDecision(blackboard);
        }
        else
        {
            Debug.LogWarning("Decision tree didn't resolve to an action");
        }
    }
    // Update is called once per frame
    private void Update()
    {
        // You could run the decision tree each frame or at intervals
        if (Input.GetKeyDown(KeyCode.Space))
        {
            RunDecisionTree();
        }
    }
}
