using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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
            if (chance % 5 == 0)
            {
                m_enemyAIMode = EnemyAIMode.ROAM;
                Vector3 randomDirection = Random.insideUnitSphere * 5.0f; 
                randomDirection += transform.position;

                NavMeshHit hit;
                NavMesh.SamplePosition(randomDirection, out hit, 3.0f, 1);
                m_desiredPosition = hit.position;

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
            Vector3 direction = Vector3.zero;
            switch (m_enemyAIMode)
            {
                case EnemyAIMode.ROAM:
                    
                    if (Vector3.Distance(transform.position, m_desiredPosition) < 0.001f)
                    {
                        m_actionComplete = true;
                    }
                    else
                    {
                        direction = (transform.position - m_desiredPosition);
                        direction.Normalize();
                        m_rigidBody.velocity = direction * 0.5f;
                    }
                    break;
                case EnemyAIMode.GUARD:
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
