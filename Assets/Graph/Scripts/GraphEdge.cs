using System;

public class GraphEdge
{
    public Graph GraphInstance;
    public GraphNode Node1;
    public GraphNode Node2;
    public float Distance
    {
        get
        {
            if (float.IsNaN(_distance))
                _distance = (Node1.Position - Node2.Position).magnitude;
            
            return _distance;
        }
    }
    
    private float _distance = float.NaN;
    public float Weight { get; set; }
    public float Price => Distance * Weight;
    public float ModelWeight { get; set; }
    public float ModelPrice => Distance * ModelWeight;

    public GraphEdge(GraphNode node1, GraphNode node2, float weight = 1.0f, float modelWeight = 1.0f)
    {
        GraphInstance = Graph.Instance;
        Node1 = node1;
        Node2 = node2;
        Weight = weight;
        ModelWeight = modelWeight;
    }

    public GraphNode OtherNode(GraphNode node)
    {
        if (node == Node1)
            return Node2;
        
        if (node == Node2)
            return Node1;
        
        throw new ArgumentException("node is not incedent to the edge");
    }
}
