using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class UnitManager : MonoBehaviour
{
    public static UnitManager Singleton { get; private set; }

    [SerializeField]
    private List<UnitType> _unitTypes;

    [SerializeField]
    private List<SizeType> _sizeTypes;

    [SerializeField]
    private List<ShapeType> _shapeTypes;

    [SerializeField]
    private List<ColorType> _colorTypes;

    [SerializeField]
    private int unitsInLine = 10;

    [SerializeField]
    private int unitsInTeam = 20;

    [SerializeField]
    private GameObject _basicSoldierPrefab;

    private List<UnitController> _team1 = new List<UnitController>();

    private List<UnitController> _team2 = new List<UnitController>();

    public UnityEvent OnUnitDeath;

    private void Awake()
    {
        Singleton = this;
    }

    void Start()
    {
        GenerateArmy(_team1, Vector3.zero,_team2," team 1");
        GenerateArmy(_team2, new Vector3(10,0,0),_team1," team 2");

        OnUnitDeath.AddListener(CheckForEndOfBattle);
    }

    private void GenerateArmy(List<UnitController> team, Vector3 offset, List<UnitController> opposingTeam, string teamName)
    {
        for(int i=0;i<unitsInTeam;i++)
        {
            for (int j = 0; j < unitsInLine && team.Count < unitsInTeam; j++)
            {
                team.Add(CreateRandomSoldier(new Vector3(i,0,j)+ offset,opposingTeam,teamName));
            }
        }
    }

    public void RandomizeGame()
    {
        if (GameManager.Singleton.GameHasStarted)
            return;

        RandomizeArmy(_team1);
        RandomizeArmy(_team2);
    }

    private void RandomizeArmy(List<UnitController> team)
    {
        for (int i = 0; i < unitsInTeam; i++)
        {
              CreateRandomSoldier(team[i].transform.position,team[i].OpposingTeam, team[i].gameObject.name, team[i]);
        }
    }

    private UnitController CreateRandomSoldier(Vector3 position, List<UnitController> opposingTeam,string teamName, UnitController soldier = null)
    {
        GameObject soldierGameObject;

        if (soldier == null)
        {
            //Instanciate main soldier prefab and get references;
            soldierGameObject = Instantiate(_basicSoldierPrefab);
            soldierGameObject.name = $"Soldier{ teamName}";
            soldier = soldierGameObject.GetComponent<UnitController>();
        }
        else
        {
            soldierGameObject = soldier.gameObject;
        }
       

        //Generate random soldier
        soldier.UnitType = _unitTypes[Random.Range(0, _unitTypes.Count)];
        soldier.SizeType = _sizeTypes[Random.Range(0, _sizeTypes.Count)];
        soldier.ShapeType = _shapeTypes[Random.Range(0, _shapeTypes.Count)];
        soldier.ColorType = _colorTypes[Random.Range(0, _colorTypes.Count)];

        //Instantiate shape and set up transforms
        //It would be better to use Object Pooling
        if(soldier.ShapePrefab != null)
        {
            Destroy(soldier.ShapePrefab);
        }

        soldier.ShapePrefab = Instantiate(soldier.ShapeType.Prefab);
        soldier.ShapePrefab.transform.parent = soldierGameObject.transform;
        soldier.ShapePrefab.transform.localPosition = Vector3.zero;
        soldier.ShapePrefab.transform.localScale = soldier.SizeType.Size;
        soldier.ShapePrefab.GetComponent<MeshRenderer>().material.color = soldier.ColorType.Color;

        soldierGameObject.transform.position = position;

        //Calculate final stats
        if (soldier.FinalStats == null)
            soldier.FinalStats = new UnitFinalStats();

        soldier.FinalStats.Health = soldier.UnitType.BasicUnitStats.Health + soldier.SizeType.BasicUnitStats.Health + soldier.ShapeType.BasicUnitStats.Health + soldier.ColorType.BasicUnitStats.Health;
        soldier.FinalStats.MaxHealth = soldier.FinalStats.Health;
        soldier.FinalStats.Speed = soldier.UnitType.BasicUnitStats.Speed + soldier.SizeType.BasicUnitStats.Speed + soldier.ShapeType.BasicUnitStats.Speed + soldier.ColorType.BasicUnitStats.Speed;
        soldier.FinalStats.Attack = soldier.UnitType.BasicUnitStats.Attack + soldier.SizeType.BasicUnitStats.Attack + soldier.ShapeType.BasicUnitStats.Attack + soldier.ColorType.BasicUnitStats.Attack;
        soldier.FinalStats.AttackSpeed = soldier.UnitType.BasicUnitStats.AttackSpeed + soldier.SizeType.BasicUnitStats.AttackSpeed + soldier.ShapeType.BasicUnitStats.AttackSpeed + soldier.ColorType.BasicUnitStats.AttackSpeed;
        soldier.FinalStats.Range = soldier.UnitType.BasicUnitStats.Range + soldier.SizeType.BasicUnitStats.Range + soldier.ShapeType.BasicUnitStats.Range + soldier.ColorType.BasicUnitStats.Range;
        soldier.FinalStats.TargettingTypeEnum = soldier.ShapeType.TargettingTypeEnum;

        //Set Up Combat Logic
        soldier.Alive = true;
        soldier.OpposingTeam = opposingTeam;

        soldier.Agent.speed = (float)soldier.FinalStats.Speed/2;
        soldier.Agent.angularSpeed = (float)soldier.FinalStats.Speed/2;
        soldier.Agent.acceleration = (float)soldier.FinalStats.Speed/2;

        //Update UI
        soldier.UpdateUI();

        return soldier;
    }

    private void CheckForEndOfBattle()
    {
        bool team1Dead = false;
        bool team2Dead = false;

        team1Dead = CheckTeamAlive(_team1);
        team2Dead = CheckTeamAlive(_team2);

        if (team1Dead || team2Dead)
        {
            Invoke(nameof(LoadMainMenu), 3);
        }
    }

    private bool CheckTeamAlive(List<UnitController> team)
    {
        for (int i = 0; i < team.Count; i++)
        {
            if (team[i].Alive)
            {
                return false;
            }
        }

        return true;
    }

    private void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
