using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGridAI : MonoBehaviour {

    [SerializeField]
    private int m_ammoCount;

    [SerializeField]
    private GridCell m_currentGrid;

    [SerializeField]
    private GameObject m_enemyBulletPrefab;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (GameManager.Instance.CurrentTurn == GameManager.Turn.GUEST)
        {
            //Run Guest Behavior

        }
	}
}
