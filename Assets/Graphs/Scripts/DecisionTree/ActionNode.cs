using UnityEngine;

public class ActionNode: DecisionTreeNode
{
    public string actionName; // Or any other action data you need

    public ActionNode(string name)
    {
        actionName = name;
    }

    public override DecisionTreeNode MakeDecision(AIBlackBoard blackboard)
    {
        // Actions simply return themselves
        return this;
    }

    public void Execute()
    {
        // Here you would implement the actual action logic
        Debug.Log("Executing action: " + actionName);
    }
}
