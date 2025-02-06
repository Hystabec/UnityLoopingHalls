using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnWithLoopCount : MonoBehaviour
{
    [SerializeField] int loopCountToSpawnMin;
    [SerializeField] bool showAfterPassingMax = true;
    [SerializeField] int loopCountToSpawnMax;

    public int getLoopCountMin() { return loopCountToSpawnMin; }
    public int getLoopCountMax() { return loopCountToSpawnMax; }
    public bool getShowAfterPassingMax() { return showAfterPassingMax; }
}
