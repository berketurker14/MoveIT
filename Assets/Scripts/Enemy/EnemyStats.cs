using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemyStats", menuName = "Enemy Stats")]
public class EnemyStats : ScriptableObject
{
    public string enemyName;
    public GameObject enemyPrefab;
    public float health;
    public int experience;
    public float damage;
    public ParticleSystem deathParticleEffect;
    public EnemyStateMachine esm;
}