using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Turnmanager : MonoBehaviour
{
    [SerializeField]
    private Player player;
    [SerializeField]
    private Computer computer;
 

    protected Dictionary<string, GameObject> playerShips;
    protected Dictionary<string, GameObject> computerShips;

    [SerializeField]
    private SuperScene scene;

    [SerializeField]
    private GameObject defButton;
    [SerializeField]
    private GameObject attButton;

    [SerializeField]
    private Button target0Button;
    [SerializeField]
    private Button target1Button;
    [SerializeField]
    private Button target2Button;

    private List<Button> targetButtons;


    [SerializeField]
    private Text text;

    void Start()
    {
        StartCoroutine(StartTurn());
    }

    private void Awake()
    {
        //find the attack and defend buttons
        //attButton = GameObject.FindGameObjectWithTag("AttackButton");
        //defButton = GameObject.FindGameObjectWithTag("DefButton");
        targetButtons = new List<Button>();

        targetButtons.Add(target0Button);
        targetButtons.Add(target1Button);
        targetButtons.Add(target2Button);

        deactivateButtons();

        playerShips = player.GetFleet();
        computerShips = computer.GetFleet();
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator StartTurn()
    {
        //update fleet
        playerShips = player.GetFleet();
        computerShips = computer.GetFleet();

        for (int i = 0; i < playerShips.Count; )
        {
            //TODO: highlight ship for which player has to select action
            var ship = playerShips.ElementAt<KeyValuePair<string, GameObject>>(i).Value;
            if (ship != null)
            {
                Ship _ship;
                _ship = ship.GetComponent<Ship>();
                _ship.SetState(0);
                
                // TO DO: highlight shipToHighlight
                text.text = "what would you like " + _ship.name + " to do?";
                //select state for ship
                activateButtons();
                Button ATB = attButton.GetComponent<Button>();
                ATB.onClick.AddListener(delegate () { selectStateAttack(ship); });
                Button DB = defButton.GetComponent<Button>();
                DB.onClick.AddListener(delegate () { selectStateDefend(ship); });
                while (_ship.GetState() == 0)
                {
                    yield return null;
                }


                deactivateButtons();
                text.text = string.Empty;
                yield return new WaitForSeconds(1);
                i++;
            }
            
        }
        print("selection for player complete");

        for (int i = 0; i < computerShips.Count; i++ )
        {
            var ship = computerShips.ElementAt<KeyValuePair<string, GameObject>>(i).Value;

            int state = Random.Range(0, 1);
            if (state == 0)
            {
                selectStateAttack(ship);
            }
            else
            {
                selectStateDefend(ship);
            }
            //yield return new WaitForSeconds(1);
            
        }

        print("selection for computer complete");

        //do attacks now
        StartCoroutine(Attack()); 
    }

    IEnumerator Attack()
    {
        deactivateButtons();
        for (int i = 0; i <= playerShips.Count; i++)
        {
            GameObject ship = playerShips.ElementAt<KeyValuePair<string, GameObject>>(i).Value;
            Ship _ship = ship.GetComponent<Ship>();

            //change the button and UI text
            //Text attbtnText = attButton.GetComponentInChildren<Text>();
            //attbtnText.text = computerShips.ElementAt<KeyValuePair<string, GameObject>>(0).Value.name;
            
            //Text defbtnText = defButton.GetComponentInChildren<Text>();
            //defbtnText.text = computerShips.ElementAt<KeyValuePair<string, GameObject>>(1).Value.name;

            text.text = "which ship would you like " + ship.name + " to attack?";

            //GameObject targetShip = computerShips.ElementAt<KeyValuePair<string, GameObject>>(0).Value;
            //add the listeners to the buttons


            int count = 0;
            //create buttons for each tagetable ship
            foreach (KeyValuePair<string, GameObject>element in computerShips)
            {
                
                Button button = targetButtons[count];
                GameObject _targetShip = computerShips.ElementAt<KeyValuePair<string, GameObject>>(count).Value;
                
                //add a listener to this button and a targetable ship
                button.onClick.AddListener(delegate () { selectTarget(ship, computerShips.ElementAt<KeyValuePair<string, GameObject>>(count).Value); });
                //activate the button
                button.GetComponent<Text>().text = _targetShip.name;
                button.gameObject.SetActive(true);

                count++;
            }

            //attButton.GetComponent<Button>().onClick.AddListener(delegate () { selectTarget(ship, targetShip);  });
            //targetShip = computerShips.ElementAt<KeyValuePair<string, GameObject>>(1).Value;
            //defButton.GetComponent<Button>().onClick.AddListener(delegate () { selectTarget(ship, targetShip); });

            activateButtons();

            while (_ship.GetTarget() == null)
            {
                yield return null;
            }

            deactivateButtons();
        }

        print("target selection complete");
        yield return new WaitForSeconds(2);

        for (int i = 0; i < playerShips.Count; i++)
        {
            GameObject ship = playerShips.ElementAt<KeyValuePair<string, GameObject>>(i).Value;
            ship.GetComponent<Ship>().Attack();
        }


    }
    #region attack and target selection
    void selectStateAttack(GameObject s)
    {
        if (s == null)
        {
            print("no ship found");
        }
        else
        {
            Ship _s = s.GetComponent<Ship>();
            _s.SetState(1);
            deactivateButtons();
        }
        
    }

    void selectStateDefend(GameObject s)
    {
        if (s == null)
        {
            print("idk what this is");
        }
        else
        {
            Ship _s = s.GetComponent<Ship>();
            _s.SetState(2);
            deactivateButtons();
        }
    }

    void selectTarget(GameObject s, GameObject t)
    {
        Ship ship = s.GetComponent<Ship>(); 
        Ship target = t.GetComponent<Ship>();


        ship.setTarget(target);
        print(ship.name + "'s target set to: " + target.name);
    }

    #endregion

    #region buttons
    //activate buttons
    protected void activateButtons()
    {
        attButton.SetActive(true);
        defButton.SetActive(true);
    }
    //deactivate buttons
    protected void deactivateButtons()
    {
        text.text = string.Empty;
        attButton.SetActive(false);
        defButton.SetActive(false);
    }

    protected void setButtonPos(Vector3 pos)
    {
        attButton.transform.position = pos;
        defButton.transform.position = pos;
    }

    private void createTargetButton()
    {

    }
    #endregion



}


