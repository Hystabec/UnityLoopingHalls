using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class teleportScript : MonoBehaviour
{
    [SerializeField] Transform startHallAnchor;
    [SerializeField] Transform thisHallAnchor;

    [SerializeField] GameObject player;

    [SerializeField] GameObject playerCamera;
    [SerializeField] GameObject hallWayCamera;
    [SerializeField] loopingCamera lcs;

    [SerializeField] List<GameObject> gameObjectsToActivate;
    [SerializeField] List<GameObject> gameObjectsToDeactivate;

    [SerializeField] teleportScript backTeleport;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "player")
        {
            lcs.Deactivate();

            foreach (GameObject go in gameObjectsToActivate)
                go.SetActive(true);

            foreach (GameObject go in gameObjectsToDeactivate)
                go.SetActive(false);

            Vector3 hallWayCamPos = hallWayCamera.transform.position;

            hallWayCamera.transform.position = playerCamera.transform.position;

            player.transform.position = startHallAnchor.position - (thisHallAnchor.position - player.transform.position);

            if(backTeleport)
            {
                backTeleport.assignAnchors(thisHallAnchor, startHallAnchor);
            }

            lcs.ReverseLoop();
        }
    }

    public void assignAnchors(Transform start, Transform thisHall)
    {
        startHallAnchor = start;
        thisHallAnchor = thisHall;
    }

    public Transform getAnchor() {  return thisHallAnchor; }
    public Transform getLoopAnchor() { return startHallAnchor; }
}
