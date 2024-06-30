using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputFieldGrabber : MonoBehaviour
{
    // Start is called before the first frame update
    public string Input;

    public void GrabFromInputField(string input)
    {
        Input = input;
    }
}
