using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StatsUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI damageText;
    [SerializeField] TextMeshProUGUI attackSpeedText;
    [SerializeField] TextMeshProUGUI critChanceText;
    [SerializeField] TextMeshProUGUI rangeText;
    [SerializeField] TextMeshProUGUI knockbackText;

    PlayerStats stats;

    private void Awake()
    {
        stats = FindObjectOfType<PlayerStats>();
    }

    private void OnEnable()
    {
        stats.PlayerStatsChanged += UpdateUIStats;
    }

    private void OnDisable()
    {
        stats.PlayerStatsChanged -= UpdateUIStats;
    }

    private void UpdateUIStats(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        switch (e.PropertyName)
        {
            case "Damage":
                damageText.text = "Damage: " + stats.Damage;
                break;

            case "AttackSpeed":
                attackSpeedText.text = "Attack Speed: " + stats.AttackSpeed;
                break;

            case "CritChance":
                critChanceText.text = "Crit Chance: " + stats.CritChance;
                break;

            case "Range":
                rangeText.text = "Range: " + stats.Range;
                break;

            case "Knockback":
                knockbackText.text = "Knockback: " + stats.Knockback;
                break;
        }
    }
}
