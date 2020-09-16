using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SuperScene : MonoBehaviour
{
    protected Turnmanager tm;

    protected Player p;
    protected Computer c;

    protected GameObject attShip;
    protected GameObject defShip;


    protected Dictionary<string, GameObject> pShips;
    protected Dictionary<string, GameObject> cShips;

    protected Transform[] PShipPos;
    protected Transform[] CShipPos;





    // Start is called before the first frame update
    void Awake()
    {
        
    }

    #region ship functionality
    protected void makeComputerFleet(int defShips, int attShips)
    {

        //fill the computer fleet with random ships
        for (int i = 0; i < defShips; i++)
        {
            GameObject ship = defShip;
            ship.name = "dShip" + i.ToString();
            c.AddShipToFleet(ship);
        }
        for (int i = 0; i < attShips; i++)
        {
            GameObject ship = attShip;
            ship.name = "aShip" + i.ToString();
            c.AddShipToFleet(ship);
        }
    }

    protected void makePlayerFleet(int defShips, int attShips)
    {
        for (int i = 0; i < defShips; i++)
        {
            GameObject ship = defShip;
            ship.name = "defship" + i.ToString();
            p.AddShipToFleet(ship);
        }
        for (int i = 0; i < attShips; i++)
        {
            GameObject ship = attShip;
            ship.name = "attship" + i.ToString();
            p.AddShipToFleet(ship);
        }
    }

    protected void loadShips(Player p, Computer c)
    {
        Dictionary<string, GameObject> Pdictionary = p.GetFleet();

        //load all the ships active in the players fleet at the dedicated positions
        for (int i = 0; i < Pdictionary.Count; i++)
        {
            var item = Pdictionary.ElementAt<KeyValuePair<string, GameObject>>(i);
            var shipToLoad = item.Value;

            GameObject.Instantiate(shipToLoad, PShipPos[i].transform.position, PShipPos[i].transform.rotation);
        }


        Dictionary<string, GameObject> Cdictionary = c.GetFleet();

        //load all the ships active in the computers fleet at the dedicated positions
        for (int i = 0; i < Cdictionary.Count; i++)
        {
            var item = Cdictionary.ElementAt(i);
            var shipToLoad = item.Value;

            GameObject.Instantiate(shipToLoad, CShipPos[i].transform.position, CShipPos[i].transform.rotation);
        }

    }


    protected void RemoveShip(Ship s)
    {
        
    }

    #endregion

    protected Ship SelectTarget()
    {
        //to do make a canvas to set the target for a ship to attac
        Ship target = new Ship();
        return target;
    }

    // Update is called once per frame
    void Update()
    {

    }


}
