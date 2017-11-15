using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemInteraction : MonoBehaviour {

    private GameObject _keyCard, _postItNote;
    private GameObject _HELDKeycard;
    private bool _keycardHeld = false;
    private bool _postitHeld = false;

    private Text _thoughtText;

    public Animator _gateAnim;
    public Animator _doorAnim;
    public Animator _keypadAnim;

    //Uses raycast to find item hit on keypress
    void Interaction()
    {
        //Raycast variables
        Ray l_Ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit l_RayHit;
        float l_RayReach = 3.0f;

        if (Input.GetKeyDown("e"))
        {
            //Raycast from cursor (centre of screen)
            if (Physics.Raycast(l_Ray, out l_RayHit, l_RayReach))
            {
                var item = l_RayHit.collider.gameObject.name;
                Debug.Log(item);

                KeyItems(item);
            }
        }
    }

    //Check if any interactable items have been hit by raycast
    void KeyItems(string item)
    {
        //keycard item
        if (item == _keyCard.name)
        {
            KeycardCheck();
            FadeOut();
        }
        //Gate cardslot
        if (item == (GameObject.Find("Box008").name))
        {
            GateCheck();
            FadeOut();
        }
        //Postit Note
        if (item == (GameObject.Find("post_It").name))
        {
            PostitCheck();
            FadeOut();
        }
        //Door keypad
        if (item == (GameObject.Find("keypad").name))
        {
            DoorCheck();
            FadeOut();
        }
    }

    //START INTERACTIONS
    //Interact with the keycard
    void KeycardCheck()
    {
        _keyCard.SetActive(false);
        KeycardToggle();

        _thoughtText.text = "This must be for the gate";
    }

    //Interact with the gate
    void GateCheck()
    {
        if (_keycardHeld == true)
        {
            KeycardToggle();
            _gateAnim.Play("Take 001", -1);

            _thoughtText.text = "Looks like it worked";
        }
        else if (_keycardHeld == false)
        {
            Debug.Log("Keycard needed to open this door");

            _thoughtText.text = "Looks like I need somthing to open this gate";
        }
    }

    //Interact with the postit note
    void PostitCheck()
    {
        _postitHeld = true;
        Debug.Log("Postit found");

        _thoughtText.text = "This must be the code for the door";
    }

    //Interact with the main door
    void DoorCheck()
    {
        if (_postitHeld == true)
        {
            _keypadAnim.Play("1337", -1);
            _doorAnim.Play("openDoor", -1);

            _thoughtText.text = "It worked, the door opened";
        }
        else if (_postitHeld == false)
        {
            _keypadAnim.Play("1234", -1);
            Debug.Log("code needed to open this door");

            _thoughtText.text = "I don't know the code...";
        }
    }
    //END

    //Toggles if the player is holding the keycard
    void KeycardToggle()
    {
        if (_keycardHeld == false)
        {
            Debug.Log("Hold Keycard");
            _HELDKeycard.SetActive(true);
            _keycardHeld = true;
        }
        else if (_keycardHeld == true)
        {
            Debug.Log("Drop Keycard");
            _HELDKeycard.SetActive(false);
            _keycardHeld = false;
        }
    }

    //Makes the thought text fade out over a number of seconds
    void FadeOut()
    {      
        _thoughtText.GetComponent<CanvasRenderer>().SetAlpha(1f);
        _thoughtText.CrossFadeAlpha(0f, 3.0f, false);
    }

    // Use this for initialization
    void Start () {
        _keyCard = GameObject.Find("keycard");
        _postItNote = GameObject.Find("post_It");
        _HELDKeycard = GameObject.Find("HELDkeycard");

        _thoughtText = GameObject.Find("ThoughtText").GetComponent<Text>();

        _gateAnim = GameObject.Find("first_Gate").GetComponent<Animator>();
        _doorAnim = GameObject.Find("main_Door").GetComponent<Animator>();
        _keypadAnim = GameObject.Find("keypad").GetComponent<Animator>();

        _HELDKeycard.SetActive(false);        
    }
	
	// Update is called once per frame
	void Update () {
        Interaction();

    }
}
