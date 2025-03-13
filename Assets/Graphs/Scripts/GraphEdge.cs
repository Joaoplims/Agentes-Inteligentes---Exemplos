using UnityEngine;

[System.Serializable]
public class GraphEdge
{
    public int from;
    public int to;
    public float cost;


    public GraphEdge()
    {
        from = to = -1;
        cost = 1;
    }

    public GraphEdge(int from, int to)
    {
        this.from = from;
        this.to = to;
    }

    public GraphEdge(int from, int to, float cost)
    {
        this.from = from;
        this.to = to;
        this.cost = cost;
    }
}
