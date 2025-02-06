using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ProBuilder.AutoUnwrapSettings;

public class switchAnchorScript : MonoBehaviour
{
    [SerializeField] List<Transform> anchors = new List<Transform>();

    [SerializeField] hallwayTriggerScript HTS;
    [SerializeField] teleportScript TPS;

     private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "player")
        {
            int ranNum = Random.Range(0, anchors.Count);

            HTS.setLoopAnchor(anchors[ranNum]);
            TPS.assignAnchors(anchors[ranNum], TPS.getAnchor());
        }
    }
}
