using System.Collections.Generic;
using System.Globalization;
using System.Xml;
using UnityEngine;

public class Graph : MonoBehaviour
{
    public static Graph Instance;

    public List<TextAsset> Blueprints;
    public float MaxX => _maxX;
    [SerializeField] private float _maxX = 2000.0f;
    public float MaxY => _maxY;
    [SerializeField] private float _maxY = 2000.0f;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        
        Instance = this;
        DontDestroyOnLoad(gameObject);

        LocalAwake();
    }

    private void LocalAwake()
    {
    }

    private void Start()
    {
        Parse();
    }

    private void Parse()
    {
        foreach (var blueprint in Blueprints)
            ParseBlueprint(blueprint.text);
    }

    private void ParseBlueprint(string svgText, float z)
    {
        var nodeByXY = new Dictionary<(float, float), GraphNode>();

        var doc = new XmlDocument();
        doc.LoadXml(svgText);

        var viewBox = doc.DocumentElement["svg"].Attributes["viewBox"].Value.Split(' ');
        var viewBoxMaxX = float.Parse(viewBox[2], CultureInfo.InvariantCulture.NumberFormat);
        var viewBoxMaxY = float.Parse(viewBox[3], CultureInfo.InvariantCulture.NumberFormat);

        foreach (XmlNode node in doc.DocumentElement["g"].ChildNodes)
        {
            switch (node.Name)
            {
                case "line":
                    ParseLine(node);
                    break;

                case "circle":
                    ParseCircle(node);
                    break;

                case "text":
                    ParseText(node);
                    break;

                default:
                    break;
            }
        }

        void ParseLine(XmlNode node)
        {
            var x1 = ParseFloatAttribute(node, "x1") / viewBoxMaxX * MaxX - (MaxX / 2);
            var y1 = -(ParseFloatAttribute(node, "y1") / viewBoxMaxX * MaxY - (MaxY / 2));
            var x2 = ParseFloatAttribute(node, "x2") / viewBoxMaxX * MaxX - (MaxX / 2);
            var y2 = -(ParseFloatAttribute(node, "y2") / viewBoxMaxX * MaxY - (MaxY / 2));
        }

        void ParseCircle(XmlNode node)
        {
        }

        void ParseText(XmlNode node)
        {
        }

        float ParseFloatAttribute(XmlNode node, string attributeName) => 
            float.Parse(node.Attributes[attributeName].Value, CultureInfo.InvariantCulture.NumberFormat);
        
        
    }
}
