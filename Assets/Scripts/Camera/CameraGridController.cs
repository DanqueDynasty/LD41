using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraGridController : MonoBehaviour {
    [SerializeField]
    private float slerpFactor = 0.8f;

    private Vector3[] m_rotations = {
        new Vector3(0f, 0f, 0f),
        new Vector3(0f, 90f, 0f),
        new Vector3(0f, 180f, 0f),
        new Vector3(0f, 270f, 0f)
    };

    private Vector3[] m_Forwards = {
        Vector3.forward,
        Vector3.right,
        -Vector3.forward,
        -Vector3.right
    };

    private Vector3[] right = {
        Vector3.right,
        Vector3.right,
        Vector3.right,
        Vector3.right
    };

    [SerializeField]
    private int currentIndex = 0;

    private bool isRotating = false;

    public Vector3 CurrentForward = Vector3.forward;
    public Vector3 CurrentRight = Vector3.right;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        var joystickAxis = Input.GetAxis("Joystick1Horizontal");
        if (joystickAxis >= 0.8f && !isRotating)
        {
            currentIndex++;
            isRotating = true;
        }
        else if (joystickAxis <= -0.8f && !isRotating)
        {
            currentIndex--;
            isRotating = true;
        }

        int idx = (int)Mathf.Abs(currentIndex % 4);
        var targetQuaternion = Quaternion.Euler(m_rotations[idx]);
        if ( isRotating && Vector3.Distance(transform.rotation.eulerAngles, m_rotations[idx]) > 0.001f)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, targetQuaternion, slerpFactor);
        }
        else {
            if (joystickAxis == 0.0f)
            {
                isRotating = false;
            }
        }


    }
}
