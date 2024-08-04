using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/*
* This class will set up the scene and initialize objects
* 
* This class inherits from Singleton so any other script can access it easily through GameManager.Instance
*/
public class GameManager : Singleton<GameManager>
{
    private MatchablePool pool;
    private MatchableGrid grid;

    [SerializeField] private Vector2Int dimensions;
    [SerializeField] private Text gridOutput;
    private void Start()
    {
        pool = (MatchablePool) MatchablePool.Instance;
        grid = (MatchableGrid) MatchableGrid.Instance;

        // create the grid
        grid.InitializeGrid(dimensions);

        StartCoroutine(Setup());
    }

    private IEnumerator Setup()
    {
        // it's a good idea to put a loading screen here

        // pool the matchables
        pool.PoolObjects(dimensions.x * dimensions.y * 2);

        // create the grid
        grid.InitializeGrid(dimensions);

        yield return null;

        StartCoroutine(grid.PopulateGrid());

        // then remove the loading screen down here
    }
}
