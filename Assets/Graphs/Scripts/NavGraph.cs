using AIEngine.Graphs;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NavGraph : SparseGraph
{
    public bool shouldDebug = true;
    [SerializeField] private PositionNode pNodePrefab;

    protected override void Start()
    {
        base.Start();
        Initialize();
    }

    private void Initialize()
    {

        AddNewNode();
        AddNewNode();
        var fst = nodes[0];
        var snd = nodes[1];
        edges.Add(new GraphEdge(fst.GetIndice(), snd.GetIndice()));
    }

    private void OnDrawGizmos()
    {
        if (shouldDebug == false) return;


        Gizmos.color = Color.yellow;
        foreach (GraphEdge e in edges)
        {
            var nodeFrom = GetNode(e.from) as PositionNode;
            var nodeTo = GetNode(e.to) as PositionNode;
            Gizmos.DrawLine(nodeFrom.transform.position, nodeTo.transform.position);
        }
    }

    [ContextMenu("AddNode")]
    public void AddNewNode()
    {
        var n = GameObject.Instantiate(pNodePrefab, transform);
        AddNode(n);
        n.name = "Node " + nextGraphFreeIndce;
    }
    [ContextMenu("AddEdge")]
    public void AddNewEdge()
    {
        if (nodes.Count >= 2)
        {
            var fromNode = nodes[nodes.Count - 2].GetIndice(); // Pegue o índice do penultimo no

            var toNode = nodes[nodes.Count - 1].GetIndice();   // Pegue o índice do ultimo nó
            AddEdge(fromNode, toNode);
        }
    }
}
