using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharController : MonoBehaviour {

    [SerializeField]
    private float _speed = 10.0f;

	// Use this for initialization
	void Start () {
        //Turns off cursor, locked in game window
        Cursor.lockState = CursorLockMode.Locked;
	}
	
	// Update is called once per frame
	void Update () {

        float m_VertiMovement = (Input.GetAxis("Vertical") * _speed) * Time.deltaTime;
        float m_HorizMovement = (Input.GetAxis("Horizontal") * _speed) * Time.deltaTime;

        transform.Translate(m_HorizMovement, 0, m_VertiMovement);

        //If esc, turn mouse back on
        if (Input.GetKeyDown("escape"))
            Cursor.lockState = CursorLockMode.None;
	}
}
