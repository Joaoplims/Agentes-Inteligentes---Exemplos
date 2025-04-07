using UnityEngine;

public class DecisionNode: DecisionTreeNode
{
    public DecisionTreeNode trueNode;
    public DecisionTreeNode falseNode;
    

    protected virtual DecisionTreeNode GetBranch(){ return null;}

    public override DecisionTreeNode MakeDecision(AIBlackBoard blackboard)
    {
        // Make the decision and recurse based on the result
        DecisionTreeNode branch = GetBranch();
        return branch.MakeDecision(blackboard);
    }
}
