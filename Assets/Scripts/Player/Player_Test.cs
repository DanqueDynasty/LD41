using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Test : MonoBehaviour {

    private Rigidbody m_rigidBody;

    // MovementParameters
    [SerializeField]
    private float m_forwardSpeed = 2.0f;

    [SerializeField]
    private float m_strafeSpeed = 1.5f;

    [SerializeField]
    private float m_turnSpeed = 0.01f;

    [SerializeField]
    private float MAX_TURN = 0.6f;

    //Prefabs
    [SerializeField]
    private GameObject m_primaryBullet;

    public enum ControlMode : uint {
        WASD ,
        GAMEPAD
    }

    [SerializeField]
    private ControlMode m_controlMode = ControlMode.GAMEPAD;

	// Use this for initialization
	void Start () {
        m_rigidBody = GetComponent<Rigidbody>();
	}

    /// <summary>
    /// Fires the bullet.
    /// </summary>
    /// <returns></returns>
    IEnumerator FireBullet()
    {
        var newBullet = Instantiate(m_primaryBullet).GetComponent<PlayerBullet>();
        newBullet.transform.position = transform.position + new Vector3(0.0f, 1.0f, 0.0f);
        Physics.IgnoreCollision(GetComponent<Collider>(), newBullet.GetComponent<Collider>());
        newBullet.InitialData(transform.forward);
        yield return new WaitForSeconds(0.01f);
    }
	
	// Update is called once per frame
	void Update () {
        var forward = transform.forward;
        var right = transform.right;
        if (m_controlMode == ControlMode.GAMEPAD)
        {
            var movementX = Input.GetAxis("Horizontal");
            var movementZ = Input.GetAxis("Vertical");
            var aimY = Input.GetAxis("Joystick1Horizontal");    //Augments the forward direction

            var movementVector = new Vector3(movementZ * forward.x, 0.0f, movementZ * forward.z);

            Debug.Log("Aim: " + aimY);

            if (movementX == 0.0f)
            {
                m_rigidBody.velocity = new Vector3(0.0f, 0.0f, 0.0f);
            }
            else
            {
                m_rigidBody.AddForce(movementVector, ForceMode.Impulse);
            }


            if (m_rigidBody.angularVelocity.y < MAX_TURN)
            {
                m_rigidBody.AddTorque(new Vector3(0.0f, m_turnSpeed * aimY, 0.0f), ForceMode.Impulse);
            }

            if (Input.GetButtonDown("Fire1")) {
                StartCoroutine(FireBullet());
            }
        }
        else
        {
            //TODO Implement Keyboard control.
        }

	}
}
 