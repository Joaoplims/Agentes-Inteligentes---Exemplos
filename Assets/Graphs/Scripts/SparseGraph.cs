using System.Collections.Generic;
using UnityEngine;
namespace AIEngine.Graphs
{
    public abstract class SparseGraph : MonoBehaviour
    {
        [SerializeField] protected List<INodeGraph> nodes;
        [SerializeField] protected List<GraphEdge> edges;
        [SerializeField] protected bool isDigraph = false;
        [SerializeField] protected int nextGraphFreeIndce = 0;
        protected virtual void Start()
        {
            nodes = new List<INodeGraph>(); edges = new List<GraphEdge>();
        }
        public virtual INodeGraph GetNode(int indice)
        {
            foreach (INodeGraph node in nodes)
            {
                if (node.GetIndice() == indice)
                {
                    return node;
                }
            }
            return null;
        }
        public virtual GraphEdge GetEdge(int from, int to)
        {
            foreach (GraphEdge edge in edges)
            {
                if (edge.from == from && edge.to == to)
                {

                    return edge;
                }
            }
            return null;
        }
        public int GetNextFreeIndice() => nextGraphFreeIndce;
        public int AddNode(INodeGraph node)
        {
            nodes.Add(node);
            node.SetIndice(nextGraphFreeIndce);
            return nextGraphFreeIndce++;
        }
        public void RemoveNode(INodeGraph node)
        {
            foreach (INodeGraph n in nodes)
            {
                if (n.GetIndice() == node.GetIndice())
                {
                    nodes.Remove(n);
                }
            }
            foreach (GraphEdge edge in edges)
            {
                if (edge.from == node.GetIndice())
                {
                    edge.from = -1;
                }
                else if (edge.to == node.GetIndice())
                {
                    edge.to = -1;
                }
            }
        }
        public bool AddEdge(int from, int to)
        {
            foreach (GraphEdge edge in edges)
            {
                if ((edge.from == from) && (edge.to == to))
                {
                    Debug.LogError("Está tentando adicionar um edge que já existe!");
                    return false;
                }
            }
            edges.Add(new GraphEdge(from, to));
            return true;
        }
        public GraphEdge RemoveEdge(int from, int to)
        {
            foreach (GraphEdge edge in edges)
            {
                if ((edge.from == from) && (edge.to == to))
                {
                    return edge;
                }
            }
            return null;
        }

    }
}
