using System;
using Game.Scripts;
using UnityEngine;

[Serializable]
public class PlayerData : BaseUnitData<UnitAnimations>
{
    public Weapon Weapon => _weapon;
    public PlayerInput PlayerInput => _playerInput;
    public float AngleOffset => _angleOffset;
    public float IdleTime => _idleTime;
    public float UpgradeTime => _upgradeTime;
    [SerializeField] private float _upgradeTime;
    [SerializeField] private float _angleOffset;
    [SerializeField] private float _idleTime;
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private Weapon _weapon;
}