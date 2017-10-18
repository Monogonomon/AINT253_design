using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharMouseLook : MonoBehaviour {

    Vector2 _mouseLook;
    Vector2 _smoothLook;

    [SerializeField]
    private float _sensitivity = 5.0f;
    [SerializeField]
    private float _smoothing = 2.0f;

    GameObject character;

    // Use this for initialization
    void Start () {

        character = this.transform.parent.gameObject;
	}
	
	// Update is called once per frame
	void Update () {
        //Get Input
        var m_mouseDelta = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));

        //Multiply change in mouse by smoothing
        m_mouseDelta = Vector2.Scale(m_mouseDelta, new Vector2(_sensitivity * _smoothing, _sensitivity * _smoothing));
        //Linear Interp - moves mouse smoothly between two points
        _smoothLook.x = Mathf.Lerp(_smoothLook.x, m_mouseDelta.x, 1f / _smoothing) ;
        _smoothLook.y = Mathf.Lerp(_smoothLook.y, m_mouseDelta.y, 1f / _smoothing);
        _mouseLook += _smoothLook;

        //no flips; you're not that flexible
        _mouseLook.y = Mathf.Clamp(_mouseLook.y, -90f, 90f);

        //apply
        transform.localRotation = Quaternion.AngleAxis(-_mouseLook.y, Vector3.right);
        character.transform.localRotation = Quaternion.AngleAxis(_mouseLook.x, character.transform.up);
	}
}
