using UnityEngine;

public class EnemyStateMachine : MonoBehaviour
{
    public int maxHP = 100;
    public int currentHP;
    public ParticleSystem deathParticle;
    public GameObject objectPool;

    //will be fixed
    private enum State
    {
        Alive,
        Dead
    }

    private State state;

    void Start()
    {
        deathParticle = GameObject.Find("Particles").transform.Find("DeathParticle").GetComponent<ParticleSystem>();
        currentHP = maxHP;
        state = State.Alive;
    }

    void Update()
    {
        switch (state)
        {
            case State.Alive:
                if (currentHP <= 0)
                {
                    state = State.Dead;
                }
                break;
            case State.Dead:
                PlayDeathParticle();
                MoveToPool();
                break;
            default:
                break;
        }
    }

    void PlayDeathParticle()
    {
        if (deathParticle != null)
        {
            deathParticle.gameObject.SetActive(true);
            deathParticle.Play();
        }
    }

    void MoveToPool()
    {
        if (objectPool != null)
        {
            //gameObject.SetActive(false);
            //transform.position = objectPool.transform.position;
            //objectPool.GetComponent<ObjectPool>().AddToPool(gameObject);
        }
    }

    public void TakeDamage(int damageAmount)
    {
        currentHP -= damageAmount;
    }
}
