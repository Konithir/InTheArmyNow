using System;
using UnityEngine;

[CreateAssetMenu(menuName = "In the army now/Unit System/Shape Type")]
public class ShapeType : ScriptableObject
{
    [SerializeField]
    private UnitBasicStats _basicUnitStats;

    [SerializeField]
    private TargettingTypeEnum _targettingTypeEnum;

    [SerializeField]
    private GameObject _prefab;

    public UnitBasicStats BasicUnitStats
    {
        get { return _basicUnitStats; }
    }

    public TargettingTypeEnum TargettingTypeEnum
    {
        get { return _targettingTypeEnum; }
    }

    public GameObject Prefab
    {
        get { return _prefab; }
    }
}