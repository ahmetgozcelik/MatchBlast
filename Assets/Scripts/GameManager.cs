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

        pool.PoolObjects(10);

        // create the grid
        grid.InitializeGrid(dimensions);

        StartCoroutine(Demo());
    }

    private IEnumerator Demo()
    {
        // display the grid
        gridOutput.text = grid.ToString();
        yield return new WaitForSeconds(2);

        // take matchables from the pool 
        Matchable m1 = pool.GetPooledObject();
        m1.gameObject.SetActive(true);
        m1.gameObject.name = "a";

        Matchable m2 = pool.GetPooledObject();
        m2.gameObject.SetActive(true);
        m2.gameObject.name = "b";

        // put hem on the grid
        grid.PutItemAt(m1, 0, 1);
        grid.PutItemAt(m2, 2, 3);

        // display the grid
        gridOutput.text = grid.ToString();
        yield return new WaitForSeconds(2);

        // swap the matchables
        grid.SwapItemsAt(0, 1, 2, 3); 
        gridOutput.text = grid.ToString();
        yield return new WaitForSeconds(2);

        // remove the matchables from the grid
        grid.RemoveItemAt(0, 1);
        grid.RemoveItemAt(2, 3);
        gridOutput.text = grid.ToString();
        yield return new WaitForSeconds(2);

        // return the matchables to the pool
        pool.ReturnObjectToPool(m1);
        pool.ReturnObjectToPool(m2);

        yield return null;
    }
}
