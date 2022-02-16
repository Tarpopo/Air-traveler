using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class BackStack : MonoBehaviour
{
    private readonly Dictionary<ResourceType, List<Transform>>
        _blocks = new Dictionary<ResourceType, List<Transform>>();

    public void AddToStack(ResourceType type, Transform block)
    {
        block.SetParent(transform);
        if (_blocks.ContainsKey(type) == false) _blocks.Add(type, new List<Transform>());
        _blocks[type].Add(block);
    }

    // public void SpawnAndAdd(ResourceType type, Transform block)
    // {
    //     ResourcesPrefabs.Instance.SpawnResource();
    //     AddToStack(type, block);
    // }

    public void ReduceResource(IEnumerable<ResourceKit> resourceKits)
    {
        foreach (var resourceKit in resourceKits)
        {
            for (int i = 0; i < resourceKit.Count; i++)
                ManagerPool.Instance.Despawn(PoolType.Entities, _blocks[resourceKit.ResourceType][i].gameObject);
            _blocks[resourceKit.ResourceType].RemoveRange(0, resourceKit.Count);
        }
    }

    public void Clear()
    {
        var children = transform.GetAllChildren();
        foreach (var child in children)
        {
            child.SetParent(null);
            ManagerPool.Instance.Despawn(PoolType.Entities, child.gameObject);
        }
    }
}