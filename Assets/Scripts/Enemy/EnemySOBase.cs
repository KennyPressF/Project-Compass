using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy SO", menuName = "Scriptable Objects/Enemy")]
public class EnemySOBase : ScriptableObject
{
    public float health;
    public float moveSpeed;
    public float scoreValue;

    public string animMoveUp;
    public string animMoveRight;
    public string animMoveDown;
    public string animMoveLeft;
}
