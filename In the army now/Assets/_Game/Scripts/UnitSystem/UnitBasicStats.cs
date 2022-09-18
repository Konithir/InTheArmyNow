using UnityEngine;

[CreateAssetMenu(menuName = "In the army now/Unit System/Unit Basic Stats")]
public class UnitBasicStats : ScriptableObject
{
    [SerializeField]
    private int _health;

    [SerializeField]
    private int _speed;

    [SerializeField]
    private int _attack;

    [SerializeField]
    private int _attackSpeed;

    [SerializeField]
    private float _range;

    public int Health
    {
        get { return _health; }
    }

    public int Speed
    {
        get { return _speed; }
    }

    public int Attack
    {
        get { return _attack; }
    }

    public int AttackSpeed
    {
        get { return _attackSpeed; }
    }

    public float Range
    {
        get { return _range; }
        set { _range = value; }
    }
}
