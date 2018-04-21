using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour {

    private Vector3 m_initialDirection;

    [SerializeField]
    private int damage;

    private float m_speed = 100.0f;

    private Rigidbody m_rigidBody;

	// Use this for initialization
	void Start () {
        m_rigidBody = GetComponent<Rigidbody>();
	}

    /// <summary>
    /// Initializes the data.
    /// </summary>
    /// <param name="direction">The direction.</param>
    public void InitialData(Vector3 direction)
    {
        m_initialDirection = direction * m_speed;
    }
	
	// Update is called once per frame
	void Update () {
        m_rigidBody.AddForce(m_initialDirection);
	}

    /// <summary>
    /// Called when [became invisible].
    /// </summary>
    private void OnBecameInvisible()
    {
        Destroy(this);
    }
}
