using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building: MonoBehaviour
{
    private bool _isDestroyed = false;
    public bool IsDestroyed => _isDestroyed;
    
    public void DestroyBuilding()
    {
        _isDestroyed = true;
    }
}
