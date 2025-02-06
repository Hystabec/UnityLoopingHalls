using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class loopCounterScript : MonoBehaviour
{
    [SerializeField] List<spawnWithLoopCount> roomItems = new List<spawnWithLoopCount>();

    public int loopsFowards { get; private set; } = 0;
    public int loopsBackwards { get; private set; } = 0;

    public void incrementFowards() { loopsFowards++; checkRoomItems(); }
    public void incrementBackwards() { loopsBackwards++; checkRoomItems(); }
    public int getDifference() { return loopsFowards - loopsBackwards; }

    void checkRoomItems()
    {
        foreach(var item in roomItems)
        {
            if(getDifference() >= item.getLoopCountMin())
            {
                if(item.getShowAfterPassingMax() || getDifference() < item.getLoopCountMax())
                {
                    item.gameObject.SetActive(true);
                }
                else
                {
                    item.gameObject.SetActive(false);
                }
            }
            else
            {
                item.gameObject.SetActive(false);
            }
        }
    }

    private void Start()
    {
        checkRoomItems();
    }
}
