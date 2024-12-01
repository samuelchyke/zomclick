using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> where T : Component
{
    private readonly T _prefab;
    private readonly Queue<T> _objects = new Queue<T>();
    private readonly Transform _parent;

    public ObjectPool(T prefab, int initialCapacity = 10, Transform parent = null)
    {
        _prefab = prefab;
        _parent = parent;
        
        // Pre-instantiate a few objects
        for (int i = 0; i < initialCapacity; i++)
        {
            AddObject();
        }
    }

    private T AddObject(bool isActiveByDefault = false)
    {
        var newObject = GameObject.Instantiate(_prefab, _parent);
        newObject.gameObject.SetActive(isActiveByDefault);
        _objects.Enqueue(newObject);
        return newObject;
    }

    public T Get()
    {
        if (_objects.Count == 0)
        {
            AddObject(isActiveByDefault: true);
        }

        var instance = _objects.Dequeue();
        instance.gameObject.SetActive(true);
        return instance;
    }

    public void ReturnToPool(T instance)
    {
        instance.gameObject.SetActive(false);
        _objects.Enqueue(instance);
    }
}