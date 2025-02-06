using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ProBuilder.AutoUnwrapSettings;

public class switchAnchorScript : MonoBehaviour
{
    [SerializeField] Transform LeftHallAnchor;
    [SerializeField] Transform RightHallAnchor;

    [SerializeField] hallwayTriggerScript HTS;
    [SerializeField] teleportScript TPS;

     private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "player")
        {
            if (Random.Range(0, 2) == 0)
            {
                HTS.setLoopAnchor(RightHallAnchor);
                TPS.assignAnchors(RightHallAnchor, TPS.getAnchor());
            }
            else
            {
                HTS.setLoopAnchor(LeftHallAnchor);
                TPS.assignAnchors(LeftHallAnchor, TPS.getAnchor());
            }
        }
    }
}
