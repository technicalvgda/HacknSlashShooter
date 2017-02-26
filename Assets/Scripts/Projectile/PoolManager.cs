using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour {
    //Key = Projectile, Value = Original Prefab.
    static Dictionary<GameObject, GameObject> childPrefabDict = new Dictionary<GameObject, GameObject>();
    //Key = Original Prefab, Value = Each pool
    static Dictionary<GameObject, List<GameObject>> inactivePoolDict = new Dictionary<GameObject, List<GameObject>>();

    public static GameObject Create(GameObject obj, Vector3 position, Quaternion rotation)
    {
        GameObject newObj = Create(obj);
        newObj.transform.position = position;
        newObj.transform.rotation = rotation;
        return newObj;
    }
    public static GameObject Create(GameObject obj, Vector3 position)
    {
        GameObject newObj = Create(obj);
        newObj.transform.position = position;
        return newObj;
    }
    public static GameObject Create(GameObject obj)
    {
        GameObject newObj = null;
        List<GameObject> pool;
        //Check to see if a pool exists for the prefab
        if (inactivePoolDict.TryGetValue(obj, out pool))
        {
            //If the pool exists, check to see if theres an object in it
            if (pool.Count > 0)
            {
                //Retrieve the first index, remove it from the pool, and activate it
                newObj = pool[0];
                pool.RemoveAt(0);
                newObj.SetActive(true);
            }
        }
        //If the pool doesn't exist or is empty, create a new object and add it to the
        //childPrefabDictionary
        if (newObj == null)
        {
            newObj = Instantiate(obj);
            childPrefabDict.Add(newObj, obj);
        }
        //Returns the object either retrieved from the pool or instantiated.
        return newObj;
    }
    public static bool Destroy(GameObject obj)
    {
        GameObject prefab = null;
        //Check to see if the object is handled by the poolmanager
        //and to retrieve the prefab associated with the object
        if (childPrefabDict.TryGetValue(obj, out prefab))
        {
            List<GameObject> pool;
            //Check to see if pool exists for the prefab
            if (!inactivePoolDict.TryGetValue(prefab, out pool))
            {
                //If pool doesn't exist, make a pool and add the pool to the dictionary
                pool = new List<GameObject>();
                inactivePoolDict.Add(prefab, pool);
            }
            //Adds the object to either the pool retrieved by the TryGetValue() call or the newly
            //created pool and then disables the object
            pool.Add(obj);
            obj.SetActive(false);
        }
        else
        {
            //This part of the code is run if the object being destroyed is
            //not actually handled by the PoolManager (and thus doesn't have
            //an actual pool associated with the object). This will return false
            //so that the code calling this destroy function will be able to handle
            //this case.
            return false;
        }
        //Return true if the Destroy was successful.
        return true;
    }
}
