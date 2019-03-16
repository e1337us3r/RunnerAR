using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // Road and Player references
    [Header("Gameobjects")]
    public GameObject Player;

    [Header("UI")]
    public Text runningTxt;

    // Traps
    private GameObject[] traps = new GameObject[2];
    // Roads
    private GameObject[] roads = new GameObject[2];
    // Vector reference for adding new roads
    private Vector3 addedRoadV = new Vector3(-4.546509f, -9.150965f, 84.2f);
    // Vector reference for starting position
    private Vector3 startingPosition;

    void Start()
    {
        // Assing Starting Position
        startingPosition = Player.transform.position;
        runningTxt.text = "Distance : 0";

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

        runningTxt.text = "Distance : " + Mathf.FloorToInt((Vector3.Distance(startingPosition, Player.transform.position)/10));
    }

    // ---------------------------------ADDING RANDOM ROADS-----------------------------------
    // Add Random Roads
    private void addRandomRoad()
    {
        GameObject rndRoad = Instantiate(roads[0],addedRoadV,roads[0].transform.rotation);
        addedRoadV = new Vector3(-4.546509f, -9.150965f, addedRoadV.z+59.3f);
        addTrapsToRoadRandomly(rndRoad,0);
        addTrapsToRoadRandomly(rndRoad,15);
        addTrapsToRoadRandomly(rndRoad,30);
    }


    // ---------------------------------ADDING TRAPS------------------------------------------
    // Add traps to Roads
    private void addTrapsToRoadRandomly(GameObject randomRoad,int range)
    {
        GameObject trap = Instantiate(traps[Random.Range(0, 2)], randomRoad.transform, true);
        trap.transform.localPosition = new Vector3(Random.Range(2, 8), trap.transform.position.y, trap.transform.position.z - Random.Range(range*1, range+3));
    }

}
