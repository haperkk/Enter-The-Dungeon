using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class PoolManager : SingletonMonobehaviour<PoolManager>
{
    [System.Serializable]
    public struct Pool
    {
        public int poolSize;
        public GameObject prefab;
        public string componentType;
    }

    [SerializeField]public Pool[] poolArray = null;
    private Transform ObjectPoolTransform;
    // key is instance ID, value is queue of Components
    private Dictionary<int, Queue<Component>> poolDictionary = new Dictionary<int, Queue<Component>>();

    private void Start()
    {
        ObjectPoolTransform = this.gameObject.transform;
        
        for (int i = 0; i < poolArray.Length; i++)
        {
            CreatePool(poolArray[i].prefab, poolArray[i].poolSize, poolArray[i].componentType);
        }
    }

    private void CreatePool(GameObject prefab, int poolSize, string componentType)
    {
        int poolKey = prefab.GetInstanceID();
        
        //set tranform relation
        string prefabName = prefab.name;
        GameObject parentGameObject = new GameObject(prefabName + " Anchor");
        parentGameObject.transform.SetParent(ObjectPoolTransform);
        
        // create queue
        if (!poolDictionary.ContainsKey(poolKey))
        {
            poolDictionary.Add(poolKey, new Queue<Component>());
            for (int i = 0; i < poolSize; ++i)
            {
                GameObject newObject = Instantiate(prefab, parentGameObject.transform) as GameObject;
                newObject.SetActive(false);
                
                //todo: why not stores gameobject? but component?
                // 1.类型安全 2.存gameobject需要每次都getcomponent
                poolDictionary[poolKey].Enqueue(newObject.GetComponent(Type.GetType(componentType)));
            }
        }
    }

    public Component ReuseComponent(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        int poolKey = prefab.GetInstanceID();

        if (poolDictionary.ContainsKey(poolKey))
        {
            Component componentToReuse = GetComponentFromPool(poolKey);

            ResetObject(position, rotation, componentToReuse, prefab);

            return componentToReuse;
        }
        else
        {
            Debug.Log("No Object Pool for " + prefab.name);
            return null;
        }
    }

    private void ResetObject(Vector3 position, Quaternion rotation, Component componentToReuse, GameObject prefab)
    {
        componentToReuse.transform.position = position;
        componentToReuse.transform.rotation = rotation;
        componentToReuse.gameObject.transform.localScale = prefab.transform.localScale;
    }

    //todo: why turn the active off here ? the instance would disappear and it's weird
    private Component GetComponentFromPool(int poolKey)
    {
        Component componentToReuse = poolDictionary[poolKey].Dequeue();
        poolDictionary[poolKey].Enqueue(componentToReuse);
        if (componentToReuse.gameObject.activeSelf == true)
        {
            componentToReuse.gameObject.SetActive(false);
        }

        return componentToReuse;
    }
    
    #region Validation

#if UNITY_EDITOR

    private void OnValidate()
    {
        HelperUtilities.ValidateCheckEnumerableValues(this, nameof(poolArray), poolArray);
    }

#endif

    #endregion Validation
}
