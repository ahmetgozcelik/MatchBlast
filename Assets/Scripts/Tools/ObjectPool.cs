using System;
using System.Collections.Generic;
using UnityEngine;

/* Performans a��s�ndan sahnede ihtiyac�m�z kadar obje �retir ve bunlar� tekrar tekrar kullanmam�z� sa�lar. */
public abstract class ObjectPool<T> : Singleton<ObjectPool<T>> where T : MonoBehaviour
{
    [SerializeField] protected T prefab; //�retilecek nesne
    private List<T> pooledObjects; //Bu nesnelerin listesi
    private int amount; //Miktar
    private bool isReady; //Havuz haz�r m�?

    // Belirlenen miktarda nesne i�eren havuz olu�tur
    public void PoolObjects(int amount = 0)
    {
        if(amount < 0)
        {
            throw new ArgumentOutOfRangeException("Amount to pool must be non-negative.");
        }

        this.amount = amount;

        // Listeyi ba�lat
        pooledObjects = new List<T>(amount);

        // Bir grup T'yi �rnekle
        GameObject newObject;

        for(int i = 0; i != amount; ++i)
        {
            newObject = Instantiate(prefab.gameObject, transform);
            newObject.SetActive(false);

            // Her birini listeye ekle
            pooledObjects.Add(newObject.GetComponent<T>());
        }
        // Havuz haz�r
        isReady = true;
    }


    // Havuzdan bir nesne getir
    public T GetPooledObject()
    {
        // Havuzu kontrol et, haz�r de�ilse haz�r hale getir.
        if (!isReady)
        {
            PoolObjects(1); // Minimum gereklili�i yerine getirebilmesi i�in parametre olarak 1 verdik.
        }
        // Aktifli�i kapal� olanlar� al�yoruz.
        for (int i = 0; i != amount; ++i)
        {
            if (!pooledObjects[i].isActiveAndEnabled)
            {
                return pooledObjects[i];
            }
        }

        // Yoksa yeni bir tane �retiyoruz.
        GameObject newObject = Instantiate(prefab.gameObject, transform);
        newObject.SetActive(false);
        pooledObjects.Add(newObject.GetComponent<T>());
        ++amount;
        return newObject.GetComponent<T>();
    }

    // Kullan�lmayan nesneleri havuza tekrar d�nd�r. Bu fonksiyon garbage collectorlar�n daha az �al��mas�n� da sa�lar.
    public void ReturnObjectToPool(T toBeReturned)
    {
        // E�er kullan�l�yorsa hi�bir �ey yapma
        if(toBeReturned == null)
        {
            return;
        }

        // Havuzu kontrol et, haz�r de�ilse haz�r hale getir. Haz�r de�ilse zaten hi� olu�turulmam��t�r. Bu olay havuzun her zaman haz�r olmas�n� garanti eder.
        if(!isReady)
        {
            PoolObjects();
            pooledObjects.Add(toBeReturned);
            ++amount;
        }

        // Aktifli�i kapat
        toBeReturned.gameObject.SetActive(false);
    }
}
