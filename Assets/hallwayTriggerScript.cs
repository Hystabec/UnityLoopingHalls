using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hallwayTriggerScript : MonoBehaviour
{
    [SerializeField] loopingCamera lcs;
    [SerializeField] Transform anchor;
    [SerializeField] Transform loopHallAnchor;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "player")
        {
            lcs.ActivateLoop(anchor, loopHallAnchor);
        }
    }
}
