using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyComponent2 : MonoBehaviour
{
    public string message = "Message from viewer project";
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(LoadMessage());
    }

    string LoadMessage()
    {
        return message;
    }
}
