using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class closeGame : MonoBehaviour
{
    [SerializeField] KeyCode exitKey = KeyCode.Escape;

    void Update()
    {
        if(Input.GetKeyDown(exitKey))
        {
            Application.Quit();
        }
    }
}
