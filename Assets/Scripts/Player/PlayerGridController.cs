using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGridController : MonoBehaviour {

    public enum Action : byte {
        MOVE,
        ATTACK,
        STALL
    }

    [SerializeField]
    private int m_health = 100;

    [SerializeField]
    private int m_searchDepth;

    [SerializeField]
    private int m_depthAvailability;

    [SerializeField]
    private GridCell m_gridCell;
    private GridCell m_tempGridCell;

    [SerializeField]
    bool m_optionSelected = false;  //Display Possible navigations

    [SerializeField]
    bool m_moveInvoked = false;
    bool m_verticalMoveInvoked = false;

    [SerializeField]
    private GameObject m_cursor;

    private Action m_action;

	// Use this for initialization
	void Start () {
        m_searchDepth = m_depthAvailability = 3;
        m_tempGridCell = m_gridCell;
        m_action = Action.MOVE;
	}
	
	// Update is called once per frame
	void Update () {
        if (!m_optionSelected)
        {
            if (GameManager.Instance.InputMode == GameManager.ControlMode.GAMEPAD)
            {
                //Handle Game Controller Logic
                var horizontalAxis = Input.GetAxis("Horizontal");
                var verticalAxis = Input.GetAxis("Vertical");

                #region Navigation
                //Handle Horizontal Traversal
                if (horizontalAxis <= -0.8f)
                {
                    //Move to West cell
                    var WestCell = m_tempGridCell.WestCell;
                    if (WestCell != null && WestCell.Searched && WestCell.SearchDepth >= 0 && !m_moveInvoked)
                    {
                        m_tempGridCell = m_tempGridCell.WestCell;
                        m_moveInvoked = true;
                    }
                }
                else if (horizontalAxis >= 0.8f)
                {
                    //Move to East Cell
                    var EastCell = m_tempGridCell.EastCell;
                    if (EastCell != null && EastCell.Searched && EastCell.SearchDepth >= 0 && !m_moveInvoked)
                    {
                        m_tempGridCell = m_tempGridCell.EastCell;
                        m_moveInvoked = true;
                    }
                }
                else
                {
                    m_moveInvoked = false;
                }

                if (verticalAxis >= 0.8f)
                {
                    var NorthCell = m_tempGridCell.NorthCell;
                    if (NorthCell != null && NorthCell.Searched && NorthCell.SearchDepth >= 0 && !m_verticalMoveInvoked)
                    {
                        m_tempGridCell = NorthCell;
                        m_verticalMoveInvoked = true;
                    }
                }
                else if (verticalAxis <= -0.8f)
                {
                    var SouthCell = m_tempGridCell.SouthCell;
                    if (SouthCell != null && SouthCell.Searched && SouthCell.SearchDepth >= 0 && !m_verticalMoveInvoked)
                    {
                        m_tempGridCell = SouthCell;
                        m_verticalMoveInvoked = true;
                    }
                }
                else
                {
                    m_verticalMoveInvoked = false;
                }
                #endregion

                if (Input.GetButtonDown("Fire1"))
                {
                    Debug.Log("Moving!");
                    m_optionSelected = true;
                }

            }
            else
            {
                //Handle Keyboard Input Logic

            }

            if (m_gridCell != null)
            {
                m_gridCell.Search(m_depthAvailability);
                Physics.IgnoreCollision(m_gridCell.GetComponent<Collider>(), m_cursor.GetComponent<Collider>());
                m_cursor.transform.position = m_tempGridCell.transform.position + new Vector3(0, 0.1f, 0);
            }
        }
        else {
            
            switch (m_action) {
                case Action.MOVE:
                    m_gridCell = m_tempGridCell;
                    GridLevelManager.Instance.ResetScene();
                    transform.position = new Vector3(m_gridCell.transform.position.x, transform.position.y, m_gridCell.transform.position.z);
                    break;
                case Action.ATTACK:

                    break;
                case Action.STALL:

                    break;
            }
            m_optionSelected = false;
        }
	}
}
