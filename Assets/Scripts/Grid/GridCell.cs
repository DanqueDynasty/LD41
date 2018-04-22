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

    private int m_depth = 5;

	// Use this for initialization
	void Start () {
        var instanceMaterial = Instantiate(GetComponent<Renderer>().sharedMaterial) as Material;
        GetComponent<Renderer>().sharedMaterial = instanceMaterial;
	}

    public void ResetCell()
    {
        isSearched = false;
        isSelected = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (isSearched)
        {
            GetComponent<Renderer>().sharedMaterial.color = new Color(1, 1, 0);
        }
        else {
            GetComponent<Renderer>().sharedMaterial.color = new Color(0.78f, 0.78f, 0.78f);
        }
	}

    public void SearchNorth(int levels)
    {
        if (levels > 0)
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

    public void SearchSouth(int levels)
    {
        if (levels > 0) {
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

    public void SearchEast(int levels)
    {
        if (levels >= 0)
        {
            isSearched = true;
            if (East != null)
            {
                East.GetComponent<GridCell>().SearchEast(--levels);
            }
        }
    }

    public void SearchWest(int levels) {
        if (levels >= 0)
        {
            isSearched = true;
            if (West != null)
            {
                West.GetComponent<GridCell>().SearchWest(--levels);
            }
        }
    }
}
