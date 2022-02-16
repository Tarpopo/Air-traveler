using System;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using TMPro;
using UnityEngine;

public class ResourceCollector : MySingleton<ResourceCollector>
{
    [SerializeField] private ResourceUI[] _resourceUI;
    [SerializeField] private Transform _collectPoint;
    [SerializeField] private BackStack _backStack;
    private readonly Dictionary<ResourceType, int> _resources = new Dictionary<ResourceType, int>();
    public Vector3 CollectPosition => _collectPoint.position;
    public BackStack BackStack => _backStack;
    public bool IsHaveAnyResource => _resources.Any(item => item.Value > 0);

    // private void Start()
    // {
    //     _resources.Add(ResourceType.Kristal, Statistics.Kristals);
    //     _resources.Add(ResourceType.Wood, Statistics.Wood);
    //     if (IsHaveAnyResource == false) return; 
    //     UpdateAllUI();
    // }

    // private void OnDisable()
    // {
    //     Statistics.Kristals = _resources[ResourceType.Kristal];
    //     Statistics.Wood = _resources[ResourceType.Wood];
    // }

    public void AddResource(ResourceType type, int count = 1)
    {
        if (_resources.ContainsKey(type))
        {
            _resources[type] += count;
            _resourceUI[(int) type].UpdateText(_resources[type]);
            return;
        }

        _resources.Add(type, count);
        _resourceUI[(int) type].Enable(true);
        _resourceUI[(int) type].UpdateText(_resources[type]);
    }

    public bool HaveResources(IEnumerable<ResourceKit> resourceKits)
    {
        // return resourceKits.Any(resourceKit =>
        //     _resources.ContainsKey(resourceKit.ResourceType) &&
        //     _resources[resourceKit.ResourceType] - resourceKit.Count >= 0);
        foreach (var resourceKit in resourceKits)
        {
            if (_resources.ContainsKey(resourceKit.ResourceType) == false ||
                _resources[resourceKit.ResourceType] - resourceKit.Count < 0) return false;
        }

        return true;
    }

    public void ReduceResources(IEnumerable<ResourceKit> resourceKits)
    {
        foreach (var resourceKit in resourceKits) _resources[resourceKit.ResourceType] -= resourceKit.Count;
        _backStack.ReduceResource(resourceKits);
        UpdateAllUI();
    }

    public void ClearAllResources()
    {
        foreach (var UI in _resourceUI)
        {
            UI.UpdateText(0);
            UI.Enable(false);
        }

        _backStack.Clear();
        _resources.Clear();
    }

    public void UpdateAllUI()
    {
        foreach (var resource in _resources) _resourceUI[(int) resource.Key].UpdateText(resource.Value);
    }
}

[Serializable]
public struct ResourceUI
{
    [SerializeField] private GameObject _resourceObject;
    [SerializeField] private TMP_Text _text;
    public void Enable(bool isActive) => _resourceObject.SetActive(isActive);
    public void UpdateText(int count) => _text.text = count.ToString();
}

[Serializable]
public struct ResourceKit
{
    [SerializeField] private ResourceType _resourceType;
    [SerializeField] private int _count;
    public ResourceType ResourceType => _resourceType;
    public int Count => _count;
}