using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setActiveOnTrigger : MonoBehaviour
{
    [SerializeField] List<GameObject> objects = new List<GameObject>();
    [SerializeField] bool toSetAs = false;

    [SerializeField] int percentChance = 100;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "player")
        {
            if (percentChance != 100)
            {
                foreach (GameObject obj in objects)
                {
                    obj.SetActive(!toSetAs);
                }
            }

            if (Random.Range(0, 101) > percentChance)
                return;

            foreach (GameObject obj in objects)
            {
                obj.SetActive(toSetAs);
            }
        }
    }
}
