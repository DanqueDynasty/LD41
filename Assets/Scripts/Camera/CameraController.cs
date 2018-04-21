using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    [SerializeField]
    private PlayerController m_Player;

    [SerializeField]
    private float m_rotationFactor;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        var cameraYaw = Input.GetAxis("Joystick1Vertical");
        var oldRotEuler = transform.rotation.eulerAngles;

        transform.position = m_Player.transform.position;
        var playerRot = m_Player.transform.rotation;
        var interpolation = Quaternion.Slerp(transform.rotation, playerRot, m_rotationFactor);
        transform.rotation = interpolation;
	}
}
