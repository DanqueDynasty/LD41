using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager Instance = null;

    public enum GameState : uint
    {
        START,
        GAME,
        PAUSE
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
