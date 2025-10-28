using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Audio;

public class NPC : MonoBehaviour
{
    [Header("SYSTEM")]
    private NavMeshAgent agent;
    [SerializeField] private NavMeshSurface surface;
    [SerializeField] private Transform[] points;
    [SerializeField] private Transform player;

    public enum ENEMY_STATE
    {
        Idle,
        Walking,
        onPlayer
    }
    [SerializeField] public ENEMY_STATE currentState;

    [Header("IDLE STATE")]
    [SerializeField] private Vector2 minMaxIdleTime; //random
    [SerializeField] private float idleTime; //contener el random
    [SerializeField] public float elapseIdleTime;

    [Header("onPlayer STATE")]
    [SerializeField] public float onPlayerDuration;
    [SerializeField] public float elpaseOnPlayerTime;

    [Header("ANIMATIONS")]
    [SerializeField] public Animator anim;

    private void Awake()
    {
        agent = GetComponentInChildren<NavMeshAgent>();
    }

    private void Start()
    {
        idleTime = Random.Range(minMaxIdleTime.x, minMaxIdleTime.y);
    }

    private void Update()
    {
        switch (currentState)
        {
            case ENEMY_STATE.Idle:
                elapseIdleTime += Time.deltaTime;
                anim.SetBool("isWalking", false);
                if (elapseIdleTime >= idleTime)
                {
                    elapseIdleTime = 0;
                    ChangeEnemyState(ENEMY_STATE.Walking);
                    anim.SetBool("isWalking", true);
                }
                break;

            case ENEMY_STATE.Walking:
                anim.SetBool("isWalking", true);
                if (agent.remainingDistance <= agent.stoppingDistance)
                {
                    ChangeEnemyState(ENEMY_STATE.Idle);
                    anim.SetBool("isWalking", false);
                }
                break;

            case ENEMY_STATE.onPlayer:
                elpaseOnPlayerTime += Time.deltaTime;
             //   anim.SetBool("isWalking", false);
                agent.isStopped = true;
                if (elpaseOnPlayerTime >= onPlayerDuration)
                {
              //      anim.SetBool("isStunned", false);
                    agent.isStopped = false;
                    ChangeEnemyState(ENEMY_STATE.Walking);
                }
                break;
        }
    }

    private void ChangeEnemyState(ENEMY_STATE newState)
    {
        currentState = newState;
        switch (currentState)
        {
            case ENEMY_STATE.Walking:
                agent.SetDestination(points[Random.Range(0, points.Length)].position);
                break;
        }
    }
}
