using System.Collections.Generic;
using UnityEngine;

public class GraphNode
{
    public Graph GraphInstance;
    public Vector3 Position;
    public List<GraphEdge> Edges;
    public string Name;

    public GraphNode(Vector3 position, string name = "", List<GraphEdge> edges = null)
    {
        GraphInstance = Graph.Instance;
        Position = position;
        Edges = new List<GraphEdge>(edges);
        Name = name;
    }
}
