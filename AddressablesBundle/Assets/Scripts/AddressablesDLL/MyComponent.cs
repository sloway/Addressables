using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyComponent : MonoBehaviour
{
    static readonly string message = "Message from bundle project";

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(GetMessage());
    }

    string GetMessage()
    {
        return message;
    }
}
