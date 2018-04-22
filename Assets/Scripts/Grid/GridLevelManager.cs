using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridLevelManager : MonoBehaviour {

    [SerializeField]
    GameObject LevelContainer;

	// Use this for initialization
	void Start () {
		
	}

    /// <summary>
    /// Resets the scene.
    /// </summary>
    void ResetScene()
    {
        var gridCells = LevelContainer.GetComponentsInChildren<GridCell>();
        foreach (var grid in gridCells)
        {
            grid.ResetCell();
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0)) {
            RaycastHit hitInfo = new RaycastHit();
            bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo);
            if (hit)
            {
                var grid = hitInfo.transform.GetComponent<GridCell>();
                if (grid != null)
                {
                    ResetScene();
                    Debug.Log("Found Grid Cell");
                    grid.SearchNorth(3);
                    grid.SearchSouth(3);
                }
                else {
                    Debug.Log("ERROR");
                }
            }
        }
	}
}
