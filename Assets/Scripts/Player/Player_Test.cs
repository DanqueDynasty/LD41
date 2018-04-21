using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Test : MonoBehaviour {

    private Rigidbody m_rigidBody;

    // MovementParameters
    [SerializeField]
    private float m_forwardSpeed = 50.0f;

    [SerializeField]
    private float m_strafeSpeed = 50.0f;

    [SerializeField]
    private float m_turnSpeed = 0.5f;

    [SerializeField]
    private float MAX_TURN = 2.0f;

    private Vector3 m_movementVector;

    private bool hasJumped = false;

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
        m_movementVector = Vector3.forward;
	}

    /// <summary>
    /// Fires the bullet.
    /// </summary>
    /// <returns></returns>
    IEnumerator FireBullet()
    {
        var newBullet = Instantiate(m_primaryBullet).GetComponent<PlayerBullet>();
        newBullet.transform.position = (transform.position + new Vector3(0.0f, 1.0f, 0.0f)) + (transform.forward * 0.9f) ;
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

            var forwardVector = new Vector3(movementZ * forward.x, 0.0f, movementZ * forward.z) * m_forwardSpeed;
            var strafeVector = new Vector3(movementX * right.x, 0.0f, movementX * right.z) * m_strafeSpeed;

            var movementVector = forwardVector + strafeVector;
            if (hasJumped)
            {
                //Maintain direction;
                movementVector = m_movementVector;
            }
            else
            {
                //Update Cached Direction
                m_movementVector = movementVector;
            }

            Debug.Log(movementVector);

            m_rigidBody.AddForce(movementVector, ForceMode.Impulse);


            if (Mathf.Abs(m_rigidBody.angularVelocity.y) <= MAX_TURN)
            {
                m_rigidBody.AddTorque(new Vector3(0.0f, m_turnSpeed * aimY, 0.0f), ForceMode.Impulse);
            }

            if (Input.GetButtonDown("Fire2") && !hasJumped)
            {
                Debug.Log("Should be jumping");
                m_rigidBody.AddForce(Vector3.up * 1200.0f, ForceMode.Impulse);
                hasJumped = true;
            }

            if (Input.GetButtonDown("Fire1"))
            {
                StartCoroutine(FireBullet());
            }
        }
        else
        {
            //TODO Implement Keyboard control.
        }
	}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground") {
            hasJumped = false;
        }   
    }
}
 