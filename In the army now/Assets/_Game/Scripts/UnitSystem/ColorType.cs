using System;
using UnityEngine;

[CreateAssetMenu(menuName = "In the army now/Unit System/Color Type")]
public class ColorType : ScriptableObject
{
    [SerializeField]
    private UnitBasicStats _basicUnitStats;

    [SerializeField]
    private Color _color;

    public UnitBasicStats BasicUnitStats
    {
        get { return _basicUnitStats; }
    }

    public Color Color
    {
        get { return _color; }
    }
}