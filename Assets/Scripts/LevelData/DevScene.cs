using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevScene : SuperScene
{
    [SerializeField]
    private int enemyAttShips;
    [SerializeField]
    private int enemyDefShips;

    [SerializeField]
    private int playerAttShips;
    [SerializeField]
    private int playerDefShips;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void Awake()
    {
        this.tm = FindObjectOfType<Turnmanager>();

        p = FindObjectOfType<Player>();
        c = FindObjectOfType<Computer>();

        attShip = Resources.Load<GameObject>("Prefabs/Ships/attShip");
        defShip = Resources.Load<GameObject>("Prefabs/Ships/defShip");


        PShipPos = new Transform[5];
        CShipPos = new Transform[5];



        //find all the positions for the ships in the scene
        GameObject[] playerShipPositions = GameObject.FindGameObjectsWithTag("PlayerShipPositions");
        GameObject[] computerShipPositions = GameObject.FindGameObjectsWithTag("ComputerShipPositions");

        //get the transform.positions from these objet to load the ships from the fleet into the scene
        for (int i = 0; i < playerShipPositions.Length; i++)
        {
            PShipPos[i] = playerShipPositions[i].transform;
            CShipPos[i] = computerShipPositions[i].transform;
        }




       makeComputerFleet(enemyDefShips, enemyAttShips);
       makePlayerFleet(playerDefShips, playerAttShips);


        this.loadShips(p, c);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
