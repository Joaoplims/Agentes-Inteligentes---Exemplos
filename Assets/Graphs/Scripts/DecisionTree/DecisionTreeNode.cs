using UnityEngine;


public abstract class DecisionTreeNode
{
    public abstract DecisionTreeNode MakeDecision(AIBlackBoard blackboard);
}
