using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * This class will set up the scene and initialize objects
 * 
 * This class inherits from Singleton so any other script can access it easily through GameManager.Instance
 */
public class GameManager : Singleton<GameManager>
{
    private MatchablePool pool;
    private void Start()
    {
        pool = (MatchablePool) MatchablePool.Instance;
        pool.PoolObjects(10);

        StartCoroutine(Demo());
    }

    private IEnumerator Demo()
    {
        Matchable m = pool.GetPooledObject();
        m.gameObject.SetActive(true);
        Vector3 randomPosition;
        for(int i = 0;i != 7; ++i)
        {
            randomPosition = new Vector3(Random.Range(-6f, 6f), Random.Range(-4f, 4f));
            yield return StartCoroutine(m.MoveToPosition(randomPosition));
        }
    }
}
