using System;
using System.Collections.Generic;
using Game.Scripts.PointWindows;
using SquareDino;
using UnityEngine;
using UnityEngine.Events;

public class UpgradeWindow : PointWindow
{
    [SerializeField] private UnityEvent _onSuccesUpgrade;
    [SerializeField] private List<UpgradeItem> _upgradeItems;

    protected override bool IsButtonEnable()
    {
        return _upgradeItems.Count != 0 &&
               ResourceCollector.Instance.HaveResources(_upgradeItems[0]._resourceKits);
    }

    public void TryUpgrade()
    {
        ResourceCollector.Instance.ReduceResources(_upgradeItems[0]._resourceKits);
        MyVibration.Haptic(MyHapticTypes.Selection);
        _upgradeItems[0].OnUpgrade();
        _upgradeItems.RemoveAt(0);
        SetButton();
        _onSuccesUpgrade?.Invoke();
    }
}

[Serializable]
public class UpgradeItem
{
    [SerializeField] private UnityEvent _onUpgrade;

    public void OnUpgrade() => _onUpgrade?.Invoke();

    public ResourceKit[] _resourceKits;
}