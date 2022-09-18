using System;
using UnityEngine;

[CreateAssetMenu(menuName = "In the army now/Unit System/Size Type")]
public class SizeType : ScriptableObject
{
    [SerializeField]
    private UnitBasicStats _basicUnitStats;

    [SerializeField]
    private Vector3 _size;

    public UnitBasicStats BasicUnitStats
    {
        get { return _basicUnitStats; }
    }

    public Vector3 Size
    {
        get { return _size; }
    }
}