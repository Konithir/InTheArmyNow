using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitFinalStats
{
    private int _health;
    private int _maxHealth;
    private int _speed;
    private int _attack;
    private int _attackSpeed;
    private float _range;
    private TargettingTypeEnum _targettingTypeEnum;

    public int Health
    {
        get { return _health; }
        set { _health = value; }
    }

    public int MaxHealth
    {
        get { return _maxHealth; }
        set { _maxHealth = value; }
    }

    public int Speed
    {
        get { return _speed; }
        set { _speed = value; }
    }

    public int Attack
    {
        get { return _attack; }
        set { _attack = value; }
    }

    public int AttackSpeed
    {
        get { return _attackSpeed; }
        set { _attackSpeed = value; }
    }

    public float Range
    {
        get { return _range; }
        set { _range = value; }
    }

    public TargettingTypeEnum TargettingTypeEnum
    {
        get { return _targettingTypeEnum; }
        set { _targettingTypeEnum = value; }
    }
}
