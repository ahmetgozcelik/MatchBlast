using UnityEngine;

/*
 * Singleton class tasar�m�. Sahnede yaln�zca bir �rne�e izin verir.
 * Abstact yap�s� bu s�n�f� �rneklendirilemez hale getirir.
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

    // OnDestroy methodu otomatik olarak �a�r�lan bir methoddur. E�er obje destroy edilirse �rnek de yok edilir.
    protected void OnDestory()
    {
        if(this == instance)
        {
            instance = null;
        }
    }

    // Awake kullanm�yorsan Init kullan�labilir? Anlamad�m tam olarak bu noktay�.
    /*
     * ��yle a��kl�yor -> Awake methodu �a�r�ld�ktan sonra, �zel ba�lang�� i�lemlerimiz i�in Init methodunu kullan�yoruz. Bu farkl� bir class i�erisinde �zelle�tirilebilir.
     */
    protected virtual void Init(){  }
}
