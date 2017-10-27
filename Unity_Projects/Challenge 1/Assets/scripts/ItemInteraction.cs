using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInteraction : MonoBehaviour {

    private GameObject _keyCard, _postItNote;
    private GameObject _HELDKeycard;
    private bool _keycardHeld = false;

    public Animator _gateAnim;

    void Interaction()
    {
        Ray l_Ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit l_RayHit;
        float l_RayReach = 3.0f;

        if (Input.GetKeyDown("e"))
        {
            if (Physics.Raycast(l_Ray, out l_RayHit, l_RayReach))
            {
                var item = l_RayHit.collider.gameObject.name;
                Debug.Log(item);

                PickupUseItems(item);
            }
        }
    }

    void PickupUseItems(string item)
    {
        if (item == _keyCard.name)
        {
            Debug.Log("Open Gate");
            _keyCard.SetActive(false);
            KeycardToggle();
        }
        if (item == (GameObject.Find("Box008").name))
        {
            Debug.Log(item);
            KeycardToggle();
            _gateAnim.Play("Take 001", -1);
        }
    }

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

    // Use this for initialization
    void Start () {
        _keyCard = GameObject.Find("keycard");
        _postItNote = GameObject.Find("post_It");
        _HELDKeycard = GameObject.Find("HELDkeycard");

        _HELDKeycard.SetActive(false);

        _gateAnim = GameObject.Find("first_Gate").GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        Interaction();

    }
}
