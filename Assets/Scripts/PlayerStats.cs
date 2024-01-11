using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] float damage;
    public float Damage { get { return damage; } private set { damage = value; OnPlayerStatsChanged(); } }

    [SerializeField] float attackSpeed;
    public float AttackSpeed { get { return attackSpeed; } private set { attackSpeed = value; OnPlayerStatsChanged(); } }

    [SerializeField] float critChance;
    public float CritChance { get { return critChance; } private set { attackSpeed = value; OnPlayerStatsChanged(); } }

    [SerializeField] float range;
    public float Range { get { return range; } private set { range = value; OnPlayerStatsChanged(); } }

    [SerializeField] float knockback;
    public float Knockback { get { return knockback; } private set { knockback = value; OnPlayerStatsChanged(); } }

    //----------------------------------------------------------------------------------------------------------------------

    public event PropertyChangedEventHandler PlayerStatsChanged;

    protected virtual void OnPlayerStatsChanged([System.Runtime.CompilerServices.CallerMemberName] string name = null)
    {
        PlayerStatsChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }

    private void Start()
    {
        Damage = damage;
        AttackSpeed = attackSpeed;
        CritChance = critChance;
        Range = range;
        Knockback = knockback;
    }

    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.Escape))
        {
            Damage++;
        }
    }
}
