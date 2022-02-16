using System.Collections.Generic;
using Game.Scripts.PointWindows;
using UnityEngine;

public class EventWindow : PointWindow
{
    [SerializeField] private List<UpgradeItem> _upgradeItems;
    private bool _isActionComplete;

    protected override bool IsButtonEnable()
    {
        return _upgradeItems.Count != 0 && ResourceCollector.Instance.HaveResources(_upgradeItems[0]._resourceKits) &&
               _isActionComplete;
    }

    public void TryUpgrade()
    {
        ResourceCollector.Instance.ReduceResources(_upgradeItems[0]._resourceKits);
        _upgradeItems[0].OnUpgrade();
        _upgradeItems.RemoveAt(0);
        SetButton();
    }

    public void CompleteEvent()
    {
        _isActionComplete = true;
        //SetButton();
    }
}