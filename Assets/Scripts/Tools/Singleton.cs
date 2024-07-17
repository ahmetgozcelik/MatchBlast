using UnityEngine;
/*
 * This is a tool that can be inherited by any Game Object script,
 * will guarantee that there will only be 1 instance allowed in the scene,
 * and provide quick access to that instance through the static getter Instance
 * 
 * If you wish to use the functionality of Awake(), use Init() instead
 */
public abstract class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    private static T instance;

    // getter
    public static T Instance
    {
        get
        {
            if(instance == null)
            {
                Debug.Log("No insntace of " + typeof(T) + " exist in the scene.");
            }
            return instance;
        }
    }

    // create reference in Awake()
    protected void Awake()
    {
        if (instance == null)
        {
            instance = this as T;
            Init();
        }
        else
        {
            Debug.LogWarning("An instance of " + typeof(T) + " already exists in the scene. Self-destructing.");
            Destroy(gameObject);
        }
    }

    // destroy the reference in OnDestroy()
    protected void OnDestory()
    {
        if(this == instance)
        {
            instance = null;
        }
    }

    // Init will replace the functionality of Awake()
    protected virtual void Init(){  }
}
