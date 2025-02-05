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
    //[SerializeField] List<GameObject> gameObjectsToDeactivate;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "player")
        {
            lcs.Deactivate();

            foreach (GameObject go in gameObjectsToActivate)
                go.SetActive(true);

            //foreach (GameObject go in gameObjectsToDeactivate)
            //    go.SetActive(false);

            hallWayCamera.transform.position = playerCamera.transform.position;

            /*if(invert)
            {
                Vector3 nonWorldSpace = thisHallAnchor.position - player.transform.position;
                Vector3 newPos = new Vector3(startHallAnchor.position.x + nonWorldSpace.x, startHallAnchor.position.y - nonWorldSpace.y, startHallAnchor.position.z - nonWorldSpace.z);
                player.transform.position = newPos;

                Vector3 newRot = player.transform.rotation.eulerAngles;
                newRot.y += 180;

                player.transform.rotation = Quaternion.Euler(newRot);
            }*/
            
            player.transform.position = startHallAnchor.position - (thisHallAnchor.position - player.transform.position);

            lcs.ReverseLoop();
        }
    }
}
