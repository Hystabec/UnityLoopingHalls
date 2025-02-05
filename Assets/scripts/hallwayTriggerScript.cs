using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hallwayTriggerScript : MonoBehaviour
{
    [SerializeField] loopingCamera lcs;
    [SerializeField] Transform anchor;
    [SerializeField] Transform loopHallAnchor;

    [SerializeField] bool getAnchorsFromTeleportScript = false;
    [SerializeField] teleportScript linkedTPScript;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "player")
        {
            if(getAnchorsFromTeleportScript && linkedTPScript)
            {
                loopHallAnchor = linkedTPScript.getLoopAnchor();
                anchor = linkedTPScript.getAnchor();
            }

            lcs.ActivateLoop(anchor, loopHallAnchor);
        }
    }

    public void setAnchor(Transform anchor) { this.anchor = anchor; }
    public Transform getAnchor() { return anchor; }
    public void setLoopAnchor(Transform loopAnchor) { this.loopHallAnchor = loopAnchor; }
    public Transform getLoopHallAnchor() { return loopHallAnchor; }
}
