using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Road and Player references
    [Header("Gameobjects")]
    public GameObject Player;

    // Traps
    private GameObject[] traps = new GameObject[2];
    // Roads
    private GameObject[] roads = new GameObject[2];
    // Vector reference for adding new roads
    private Vector3 addedRoadV = new Vector3(-4.546509f, -9.150965f, 84.2f);

    private bool isAdded = false;
    
    void Start()
    {
        // Get Traps from Resources and add as Gameobject to traps array.
        traps[0] = Resources.Load("Prefabs/Traps/SpikeTrapD") as GameObject;
        traps[1] = Resources.Load("Prefabs/Traps/SwingTrapD") as GameObject;

        // Get Roads from Resources and add as Gameobject to roads array.
        roads[0] = Resources.Load("Prefabs/StartRoad") as GameObject;
        roads[1] = Resources.Load("Prefabs/FallRoad") as GameObject;
        
    }

    // Reset Player Position when he exceeds
    void Update()
    {
        // Added Random Road
        if (Mathf.FloorToInt(Player.transform.position.z) % 15 != 0) return;
        else addRandomRoad();
    }

    // ---------------------------------ADDING RANDOM ROADS-----------------------------------------
    private void addRandomRoad()
    {
        GameObject rndRoad = Instantiate(roads[0],addedRoadV,roads[0].transform.rotation);
        addedRoadV = new Vector3(-4.546509f, -9.150965f, addedRoadV.z+59.3f);
        addTrapsToRoadRandomly(rndRoad);
    }


    // ---------------------------------ADDING TRAPS------------------------------------------
    // Add traps to Roads
    private void addTrapsToRoadRandomly(GameObject randomRoad)
    {
        GameObject trap = Instantiate(traps[Random.Range(0, 2)], randomRoad.transform, false);
        //trap.transform.localPosition = new Vector3(Random.Range(2, 6), 10f, trap.transform.position.z);
    }

}
