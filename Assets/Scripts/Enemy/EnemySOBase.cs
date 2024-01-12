using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy SO", menuName = "Scriptable Objects/Enemy")]
public class EnemySOBase : ScriptableObject
{
    public string enemyName;

    public float health;
    public float moveSpeed;
    public float scoreValue;
}
