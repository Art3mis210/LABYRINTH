using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    Dictionary<string,List<GameObject>> pool;
    void Awake()
    {
        pool = new Dictionary<string, List<GameObject>>();
    }
    public void InitPool(int capacity,GameObject poolObject,string key)
    {
        pool.Add(key, new List<GameObject>());
        for(int i=0;i<capacity;i++)
        {
            GameObject pObj = Instantiate(poolObject);
            pool[key].Add(pObj);
        }
    }
    public GameObject GetObjFromPool(string key)
    {
        foreach(GameObject obj in pool[key])
        {
            if(!obj.activeInHierarchy)
            {
                return obj;
            }
        }
        return null;
    }
}
