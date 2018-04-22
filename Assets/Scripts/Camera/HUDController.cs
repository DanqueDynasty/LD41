using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour {

    [SerializeField]
    private PlayerGridController m_player;

    [SerializeField]
    private Text m_turnText;

    [SerializeField]
    private Text m_Type;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        m_turnText.text = GameManager.Instance.CurrentTurn.ToString();
        m_Type.text = m_player.CurrentAction.ToString();
	}
}
