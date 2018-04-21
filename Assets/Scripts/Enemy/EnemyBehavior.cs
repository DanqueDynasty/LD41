using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour {
    public enum EnemyAIMode : uint
    {
        ROAM,
        GUARD,
        CHASE,
        ATTACK_MELEE,
        ATTACK_SHOOT
    };

    [SerializeField]
    private EnemyAIMode m_enemyAIMode;

    [SerializeField]
    private int m_HEALTH;

    [SerializeField]
    private GameObject m_bullet;

    [SerializeField]
    private bool isIncited = false;

    [SerializeField]
    private float m_desiredRotation;

    [SerializeField]
    private Vector3 m_desiredPosition;

    private bool m_actionComplete = true;

    private Rigidbody m_rigidBody;

	// Use this for initialization
	void Start () {
        m_enemyAIMode = EnemyAIMode.GUARD;
        m_rigidBody = GetComponent<Rigidbody>();
	}

	// Update is called once per frame
	void Update () {
        if (!isIncited && m_actionComplete)
        {
            int chance = (int)(Random.Range(0.0f, 1.0f) * 10.0f);
            if (chance % 3 == 0)
            {
                m_enemyAIMode = EnemyAIMode.ROAM;
                //TODO Switch this with some valid point on navmesh. 
                float x, y, z;
                x = Random.Range(-5f, 5f);
                z = Random.Range(-5f, 5f);
                m_desiredPosition = new Vector3(x, 0.0f, z);
                m_actionComplete = false;
            }
            else
            {
                m_enemyAIMode = EnemyAIMode.GUARD;
                m_desiredRotation = Random.Range(0.0f, 90.0f);
                m_actionComplete = false;
            }
        }
        if (!m_actionComplete)
        {
            switch (m_enemyAIMode)
            {
                case EnemyAIMode.ROAM:
                    //TODO Implement this.
                    if ((transform.position - m_desiredPosition).magnitude < 0.001f)
                    {
                        m_actionComplete = true;
                    }
                    else
                    {
                        Debug.Log("Should be moving to new point");
                        var direction = transform.position - m_desiredPosition;
                        m_rigidBody.AddForce(direction * 0.05f, ForceMode.Impulse);
                    }
                    break;
                case EnemyAIMode.GUARD:
                    //TODO Implement this
                    if ((Mathf.Abs(transform.rotation.eulerAngles.y) - Mathf.Abs(m_desiredRotation)) < 0.001f)
                    {
                        m_actionComplete = true;
                    }
                    else
                    {
                        m_rigidBody.AddTorque(new Vector3(0.0f, 0.5f, 0.0f), ForceMode.Impulse);
                    }
                    break;
                //If Player is incited    
                case EnemyAIMode.CHASE:
                    //TODO Implement this. 

                    break;
                default:

                    break;
            }
        }
	}
}
