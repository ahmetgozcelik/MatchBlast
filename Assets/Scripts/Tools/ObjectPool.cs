using System;
using System.Collections.Generic;
using UnityEngine;

/* Performans açýsýndan sahnede ihtiyacýmýz kadar obje üretir ve bunlarý tekrar tekrar kullanmamýzý saðlar. */
public abstract class ObjectPool<T> : Singleton<ObjectPool<T>> where T : MonoBehaviour
{
    [SerializeField] protected T prefab; //Üretilecek nesne
    private List<T> pooledObjects; //Bu nesnelerin listesi
    private int amount; //Miktar
    private bool isReady; //Havuz hazýr mý?

    // Belirlenen miktarda nesne içeren havuz oluþtur
    public void PoolObjects(int amount = 0)
    {
        if(amount < 0)
        {
            throw new ArgumentOutOfRangeException("Amount to pool must be non-negative.");
        }

        this.amount = amount;

        // Listeyi baþlat
        pooledObjects = new List<T>(amount);

        // Bir grup T'yi örnekle
        GameObject newObject;

        for(int i = 0; i != amount; ++i)
        {
            newObject = Instantiate(prefab.gameObject, transform);
            newObject.SetActive(false);

            // Her birini listeye ekle
            pooledObjects.Add(newObject.GetComponent<T>());
        }
        // Havuz hazýr
        isReady = true;
    }


    // Havuzdan bir nesne getir
    public T GetPooledObject()
    {
        // Havuzu kontrol et, hazýr deðilse hazýr hale getir.
        if (!isReady)
        {
            PoolObjects(1); // Minimum gerekliliði yerine getirebilmesi için parametre olarak 1 verdik.
        }
        // Aktifliði kapalý olanlarý alýyoruz.
        for (int i = 0; i != amount; ++i)
        {
            if (!pooledObjects[i].isActiveAndEnabled)
            {
                return pooledObjects[i];
            }
        }

        // Yoksa yeni bir tane üretiyoruz.
        GameObject newObject = Instantiate(prefab.gameObject, transform);
        newObject.SetActive(false);
        pooledObjects.Add(newObject.GetComponent<T>());
        ++amount;
        return newObject.GetComponent<T>();
    }

    // Kullanýlmayan nesneleri havuza tekrar döndür. Bu fonksiyon garbage collectorlarýn daha az çalýþmasýný da saðlar.
    public void ReturnObjectToPool(T toBeReturned)
    {
        // Eðer kullanýlýyorsa hiçbir þey yapma
        if(toBeReturned == null)
        {
            return;
        }

        // Havuzu kontrol et, hazýr deðilse hazýr hale getir. Hazýr deðilse zaten hiç oluþturulmamýþtýr. Bu olay havuzun her zaman hazýr olmasýný garanti eder.
        if(!isReady)
        {
            PoolObjects();
            pooledObjects.Add(toBeReturned);
            ++amount;
        }

        // Aktifliði kapat
        toBeReturned.gameObject.SetActive(false);
    }
}
