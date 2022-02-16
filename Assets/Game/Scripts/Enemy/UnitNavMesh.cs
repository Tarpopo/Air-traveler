using System;
using UnityEngine;
using UnityEngine.AI;

[Serializable]
public class UnitNavMesh
{
    [SerializeField] private NavMeshAgent _navMeshAgent;
    public void SetDestination(Vector3 point) => _navMeshAgent.SetDestination(point);

    public void ResetDestination() => _navMeshAgent.ResetPath();

    public void SetSpeed(float speed) => _navMeshAgent.speed = speed;
}