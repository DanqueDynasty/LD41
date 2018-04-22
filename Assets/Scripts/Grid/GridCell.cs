using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCell : MonoBehaviour {
    [SerializeField]
    private GameObject North;

    [SerializeField]
    private GameObject South;

    [SerializeField]
    private GameObject East;

    [SerializeField]
    private GameObject West;

    [SerializeField]
    private bool isSelected;

    [SerializeField]
    private bool isSearched = false;

    [SerializeField]
    private bool isValid = true;


    private int m_depth = 5;

	// Use this for initialization
	void Start () {
        var instanceMaterial = Instantiate(GetComponent<Renderer>().sharedMaterial) as Material;
        GetComponent<Renderer>().sharedMaterial = instanceMaterial;
	}


    /// <summary>
    /// Sets the select.
    /// </summary>
    /// <param name="level">The level.</param>
    public void SetSelect(int level)
    {
        isSearched = true;
        SearchNorth(level);
        SearchSouth(level);
    }

    /// <summary>
    /// Resets the cell.
    /// </summary>
    public void ResetCell()
    {
        isSearched = false;
        isSelected = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (isSearched)
        {
            GetComponent<Renderer>().sharedMaterial.color = new Color(0.2f, 1, 0.6f);
        }
        else if (!isValid)
        {
            GetComponent<Renderer>().sharedMaterial.color = new Color(1.0f, 0.0f, 0.0f);
        }
        else
        {
            GetComponent<Renderer>().sharedMaterial.color = new Color(0.78f, 0.78f, 0.78f);
        }
	}

    /// <summary>
    /// Searches the North Node if it is valid
    /// </summary>
    /// <param name="levels">The levels.</param>
    public void SearchNorth(int levels)
    {
        if (levels > 0 && isValid)
        {
            int nextLevel = levels - 1;
            isSearched = true;

            SearchWest(nextLevel);
            SearchEast(nextLevel);
            if (North != null) {
                North.GetComponent<GridCell>().SearchNorth(--levels);
            }
        }
    }

    /// <summary>
    /// Searches the south.
    /// </summary>
    /// <param name="levels">The levels.</param>
    public void SearchSouth(int levels)
    {
        if (levels > 0 && isValid) {
            var nextLevel = levels - 1;
            isSearched = true;

            SearchWest(nextLevel);
            SearchEast(nextLevel);

            if (South != null)
            {
                South.GetComponent<GridCell>().SearchSouth(--levels);
            }
        }
    }

    /// <summary>
    /// Searches the east.
    /// </summary>
    /// <param name="levels">The levels.</param>
    public void SearchEast(int levels)
    {
        if (levels >= 0 && isValid)
        {
            isSearched = true;
            if (East != null)
            {
                East.GetComponent<GridCell>().SearchEast(--levels);
            }
        }
    }

    /// <summary>
    /// Searches the west.
    /// </summary>
    /// <param name="levels">The levels.</param>
    public void SearchWest(int levels) {
        if (levels >= 0 && isValid)
        {
            isSearched = true;
            if (West != null)
            {
                West.GetComponent<GridCell>().SearchWest(--levels);
            }
        }
    }
}
