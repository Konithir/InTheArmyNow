using System;
using UnityEngine;

[CreateAssetMenu(menuName = "In the army now/Unit System/Unit Type")]
public class UnitType : ScriptableObject
{
    [SerializeField]
    private UnitBasicStats _basicUnitStats;

    public UnitBasicStats BasicUnitStats
    {
        get { return _basicUnitStats; }
    }
}