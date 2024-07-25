using UnityEngine;

/*
 * Singleton class tasarýmý. Sahnede yalnýzca bir örneðe izin verir.
 * Abstact yapýsý bu sýnýfý örneklendirilemez hale getirir.
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

    // OnDestroy methodu otomatik olarak çaðrýlan bir methoddur. Eðer obje destroy edilirse örnek de yok edilir.
    protected void OnDestory()
    {
        if(this == instance)
        {
            instance = null;
        }
    }

    // Awake kullanmýyorsan Init kullanýlabilir? Anlamadým tam olarak bu noktayý.
    /*
     * Þöyle açýklýyor -> Awake methodu çaðrýldýktan sonra, özel baþlangýç iþlemlerimiz için Init methodunu kullanýyoruz. Bu farklý bir class içerisinde özelleþtirilebilir.
     */
    protected virtual void Init(){  }
}
