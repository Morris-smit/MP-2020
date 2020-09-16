using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Computer : MonoBehaviour
{
    Dictionary<string, GameObject> Hangar;
    Dictionary<string, GameObject> Fleet;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void Awake()
    {

    }

    public Dictionary<string, GameObject> GetFleet()
    {
        if (Fleet == null)
        {
            Fleet = new Dictionary<string, GameObject>();
        }
        return Fleet;
    }

    public void AddShipToHangar(GameObject s)
    {
        if (Hangar == null)
        {
            Hangar = new Dictionary<string, GameObject>();
        }

        Hangar.Add(s.name, s);
        print(s.name + " Added for: " + this);
    }

    public void AddShipToFleet(GameObject s)
    {
        if (Fleet == null)
        {
            Fleet = new Dictionary<string, GameObject>();
        }

        Fleet.Add(s.name, s);
    }

    public int getShipCount()
    {
        return Fleet.Count;
    }

    public void moveShipToFleet(GameObject s)
    {
        Fleet.Add(s.name, s);
        Hangar.Remove(s.name);
    }
    public void moveShipToHangar(GameObject s)
    {
        Hangar.Add(s.name, s);
        Fleet.Remove(s.name);
    }
}
