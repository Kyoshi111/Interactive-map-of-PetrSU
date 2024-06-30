using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UiManager : MonoBehaviour
{
    public InputFieldGrabber inputFieldGrabberFrom;
    public InputFieldGrabber inputFieldGrabberTo;
    public Button buttonCalculatePath;
    public Button buttonFloor1;
    public Button buttonFloor2;
    public Button buttonTest;
    private Graph _graphInstance;
    private Camera _mainCamera;

    private void Awake()
    {
        _graphInstance = Graph.Instance;
        _mainCamera = Camera.main;

        buttonCalculatePath.onClick.AddListener(CalculatePath);
        buttonFloor1.onClick.AddListener(GoToFloor1);
        buttonFloor2.onClick.AddListener(GoToFloor2);
        buttonTest.onClick.AddListener(Test);
    }

    public void CalculatePath()
    {
        _graphInstance.StartNodeName = inputFieldGrabberFrom.Input;
        _graphInstance.EndNodeName = inputFieldGrabberTo.Input;
        _graphInstance.Dijkstra();
    }

    public void GoToFloor1()
    {
        var pos = _mainCamera.transform.position;
        _mainCamera.transform.position = new Vector3(pos.x, pos.y, -101.0f);
    }

    public void GoToFloor2()
    {
        var pos = _mainCamera.transform.position;
        _mainCamera.transform.position = new Vector3(pos.x, pos.y, -201.0f);
    }

    public void Test()
    {
        _graphInstance.StartNodeName = "В1";
        _graphInstance.EndNodeName = "152";
        _graphInstance.Dijkstra();
    }
}
