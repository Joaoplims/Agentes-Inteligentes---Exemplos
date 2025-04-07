using System;


public class FloatDecisionNode: DecisionNode
{
    public float minValue;
    public float maxValue;

    public FloatDecisionNode(float min, float max,
                        DecisionTreeNode trueBranch, DecisionTreeNode falseBranch)
    {
        minValue = min;
        maxValue = max;
        trueNode = trueBranch;
        falseNode = falseBranch;
    }

    public override DecisionTreeNode MakeDecision(AIBlackBoard blackBoard)
    {
        float value = blackBoard.heath;
        return (value >= minValue && value <= maxValue) ? trueNode : falseNode;
    }
}
