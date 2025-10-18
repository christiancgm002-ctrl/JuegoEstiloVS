using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Enemy enemyPrefab;
    public Transform player;
    public float spawnRadius = 12f;
    public float spawnsPerSecond = 1f;

    float acc;

    void Update()
    {
        acc += spawnsPerSecond * Time.deltaTime;
        while (acc >= 1f) { Spawn(); acc -= 1f; }
    }

    void Spawn()
    {
        Vector2 pos = (Vector2)player.position + Random.insideUnitCircle.normalized * spawnRadius;
        var e = Instantiate(enemyPrefab, pos, Quaternion.identity);
        e.Init(player);
    }
}