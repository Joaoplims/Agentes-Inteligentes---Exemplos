using System;
using UnityEngine;

public class MultiDecisionNode : DecisionNode
{
    public DecisionTreeNode[] childrenNodes;

    public MultiDecisionNode(DecisionTreeNode[] nodes)
    {
        childrenNodes = nodes;
    }



    public override DecisionTreeNode MakeDecision(AIBlackBoard blackboard)
    {
        int branchIndex = blackboard.enemyCount;
        if (branchIndex >= 0 && branchIndex < childrenNodes.Length)
        {
            ActionNode a = childrenNodes[branchIndex] as ActionNode; ;
            a.Execute();
        }
        return null;
    }
}
