using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [Header("Spawn Settings")]
    [SerializeField] Transform spawnedProjectiles;
    [SerializeField] GameObject spawnPointGroup;
    [SerializeField] Transform[] spawnPoints;
    Transform currentSpawnPoint;
    Vector2 currentFacingDir;

    [Header("Attack Settings")]
    [SerializeField] GameObject projectilePrefab;
    Quaternion projectileRotation;
    private List<GameObject> projectilePool;
    [SerializeField] float shotCooldown;
    [SerializeField] float shotTimer;
    [SerializeField] bool canAttack;

    PlayerStats stats;

    private void Awake()
    {
        stats = GetComponent<PlayerStats>();
    }

    private void OnEnable()
    {
        stats.PlayerStatsChanged += SetShotCooldown;
    }

    private void OnDisable()
    {
        stats.PlayerStatsChanged -= SetShotCooldown;
    }

    // Start is called before the first frame update
    void Start()
    {
        InitializeSpawnPointArray();
        projectilePool = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if(shotTimer < shotCooldown)
        {
            shotTimer += Time.deltaTime;
            canAttack = false;
        }
        else
        {
            canAttack = true;
        }
    }

    void InitializeSpawnPointArray()
    {
        int childCount = spawnPointGroup.transform.childCount;
        spawnPoints = new Transform[childCount];

        for (int i = 0; i < childCount; i++)
        {
            spawnPoints[i] = spawnPointGroup.transform.GetChild(i);
        }
    }

    public void HandleAttack(Vector2 dir)
    {
        //If direction needs to change, change direction and set relevant spawn point
        if (currentFacingDir != dir)
        {
            currentFacingDir = dir;

            switch (dir)
            {
                case Vector2 v when v.Equals(Vector2.up):
                    currentSpawnPoint = spawnPoints[0];
                    projectileRotation = Quaternion.Euler(0, 0, 0);
                    break;

                case Vector2 v when v.Equals(Vector2.right):
                    currentSpawnPoint = spawnPoints[1];
                    projectileRotation = Quaternion.Euler(0, 0, 270);
                    break;

                case Vector2 v when v.Equals(Vector2.down):
                    currentSpawnPoint = spawnPoints[2];
                    projectileRotation = Quaternion.Euler(0, 0, 180);
                    break;

                case Vector2 v when v.Equals(Vector2.left):
                    currentSpawnPoint = spawnPoints[3];
                    projectileRotation = Quaternion.Euler(0, 0, 90);
                    break;
            }
        }

        if (canAttack)
        {
            shotTimer = 0f;
            SpawnProjectile(currentSpawnPoint, projectileRotation);
        }
    }

    void SpawnProjectile(Transform spawnPoint, Quaternion startingRot)
    {
        // Search for an inactive projectile in the pool
        for (int i = 0; i < projectilePool.Count; i++)
        {
            if (!projectilePool[i].activeInHierarchy)
            {
                // Reuse the inactive projectile
                projectilePool[i].transform.position = spawnPoint.position;
                projectilePool[i].transform.rotation = startingRot;
                projectilePool[i].SetActive(true);
                return;
            }
        }

        // If no inactive projectile is found, instantiate a new one and add it to the pool
        GameObject newProjectile = Instantiate(projectilePrefab, spawnPoint.position, startingRot);
        newProjectile.transform.parent = spawnedProjectiles;
        projectilePool.Add(newProjectile);
    }

    private void SetShotCooldown(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        shotCooldown = stats.AttackSpeed;
    }
}
