using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitController : MonoBehaviour
{
    [SerializeField]
    private NavMeshAgent _agent;

    [SerializeField]
    private UnitUiController _uiController;

    private UnitType _unitType;
    private ShapeType _shapeType;
    private SizeType _sizeType;
    private ColorType _colorType;

    private UnitFinalStats _finalStats;
    private List<UnitController> _opposingTeam;

    private UnitController _target;
    private bool _alive = true;
    private float _lastAttackTime = 0;

    private GameObject _shapePrefab;

    public UnitType UnitType
    {
        get { return _unitType; }
        set { _unitType = value; }
    }

    public ShapeType ShapeType
    {
        get { return _shapeType; }
        set { _shapeType = value; }
    }

    public SizeType SizeType
    {
        get { return _sizeType; }
        set { _sizeType = value; }
    }

    public ColorType ColorType
    {
        get { return _colorType; }
        set { _colorType = value; }
    }

    public UnitFinalStats FinalStats
    {
        get { return _finalStats; }
        set { _finalStats = value; }
    }

    public List<UnitController> OpposingTeam
    {
        get { return _opposingTeam; }
        set { _opposingTeam = value; }
    }

    public NavMeshAgent Agent
    {
        get { return _agent; }
    }

    public bool Alive
    {
        get { return _alive; }
        set { _alive = value; }
    }

    public GameObject ShapePrefab
    {
        get { return _shapePrefab; }
        set { _shapePrefab = value; }
    }

    private void Update()
    {
        if (!GameManager.Singleton.GameHasStarted)
            return;

        TargetAcquisition();
        TargetAttack();
    }

    public void UpdateUI()
    {
        _uiController.UpdateUI($"{FinalStats.Health} / {FinalStats.MaxHealth}", FinalStats.Speed.ToString(), FinalStats.Attack.ToString(), FinalStats.AttackSpeed.ToString(), FinalStats.Range.ToString());
    }

    private void TargetAcquisition()
    {
        if(_target == null)
        {
            switch (_finalStats.TargettingTypeEnum)
            {
                case TargettingTypeEnum.Closest:
                    FindClosestEnemy();
                    break;
                case TargettingTypeEnum.Weakest:
                    FindWeakestEnemy();
                    break;
            }

            if(_target == null)
            {
                _agent.ResetPath();
                _agent.isStopped = true;
            }
        }
    }    

    private void TargetAttack()
    {
        if(_target)
        {
            //if in range
            if (Vector3.Distance(_target.transform.position, transform.position) <= FinalStats.Range)
            {
                _agent.ResetPath();

                if (Time.time > _lastAttackTime + FinalStats.AttackSpeed)
                {
                    _target.FinalStats.Health -= FinalStats.Attack;
                    _lastAttackTime = Time.time;
                    _target.UpdateUI();

                    if (_target.FinalStats.Health <= 0)
                    {
                        _target.gameObject.SetActive(false);
                        _target.Alive = false;
                        _target = null;

                        UnitManager.Singleton.OnUnitDeath?.Invoke();
                    }
                }
            }
            else
            {
                _agent.SetDestination(_target.transform.position);
            } 
        }
    }

    private void FindClosestEnemy()
    {
        float closestDistance = float.MaxValue;

        for (int i = 0; i < _opposingTeam.Count; i++)
        {
            if (_opposingTeam[i].Alive == false)
                continue;

            float distance = Vector3.Distance(gameObject.transform.position, _opposingTeam[i].transform.position);

            if (distance < closestDistance)
            {
                closestDistance = distance;
                _target = _opposingTeam[i];
            }
        }
    }

    private void FindWeakestEnemy()
    {
        float lowestHp = float.MaxValue;

        for (int i = 0; i < _opposingTeam.Count; i++)
        {
            if (_opposingTeam[i].Alive == false)
                continue;


            if (_opposingTeam[i].FinalStats.Health < lowestHp)
            {
                lowestHp = _opposingTeam[i].FinalStats.Health;
                _target = _opposingTeam[i];
            }
        }
    }
}
