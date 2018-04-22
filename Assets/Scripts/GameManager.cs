using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager Instance = null;

    //Which Control Scheme
    public enum ControlMode : byte {
        WASD,
        GAMEPAD
    }

    public enum Turn : byte {
        HOST,
        GUEST
    }

    [SerializeField]
    private PlayerController m_player;

    [SerializeField]
    private ControlMode m_controlMode;

    [SerializeField]
    private Turn m_currentTurn = Turn.HOST;

    public PlayerController Player { get { return m_player; } }
    public ControlMode InputMode { get { return m_controlMode; } }
    public Turn CurrentTurn { get { return m_currentTurn; } }

    public enum GameState : uint
    {
        START,
        GAME,
        PAUSE
    }

    /// <summary>
    /// Nexts the turn.
    /// </summary>
    public void NextTurn()
    {
        if (m_currentTurn == Turn.HOST)
        {
            m_currentTurn = Turn.GUEST;
        }
        else
        {
            m_currentTurn = Turn.HOST;
        }
    }

    [SerializeField]
    private GameState m_state;

    [SerializeField]
    private bool isPaused;

    private void Awake()
    {
        if (Instance == null)
        {
            Debug.Log("Assigned Game Manager");
            m_state = GameState.START;
            m_controlMode = ControlMode.GAMEPAD;
            Instance = this;
        }
    }

    // Use this for initialization
    void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
	    
	}
}
