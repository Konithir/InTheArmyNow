using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UnitUiController : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _healthText;

    [SerializeField]
    private TextMeshProUGUI _speedText;

    [SerializeField]
    private TextMeshProUGUI _attackText;

    [SerializeField]
    private TextMeshProUGUI _attackSpeedText;

    [SerializeField]
    private TextMeshProUGUI _rangeText;

    public void UpdateUI(string health, string speed, string attack, string attackSpeed, string range)
    {
        _healthText.text = health;
        _speedText.text = speed;
        _attackText.text = attack;
        _attackSpeedText.text = attackSpeed;
        _rangeText.text = range;
    }
}
