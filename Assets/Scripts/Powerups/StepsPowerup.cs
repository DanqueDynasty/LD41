using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepsPowerup : MonoBehaviour {

    public enum Type : byte {
        STEPS,
        AMMO
    }

    [SerializeField]
    private int m_value;

    [SerializeField]
    private Type m_type;

    public int Value { get { return m_value; } }
    public Type PowerupType { get { return m_type; }  }

	// Use this for initialization
	void Start () {
        var rand = Random.Range(0.0f, 10.0f);
        if (rand % 5 == 0)
        {
            m_type = Type.AMMO;
            m_value = 1;
        }
        else
        {
            m_type = Type.STEPS;
            m_value = (rand % 2 == 0) ? 3 : 2;
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
