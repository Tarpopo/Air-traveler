using System.Collections.Generic;
using UnityEngine;

public class ComponentContainer : MonoBehaviour
{
    [SerializeField] private List<BaseComponent> _components;

    public void RemoveComponent<T>(T component) where T: BaseComponent
    {
        
    }
    
}
