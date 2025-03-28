using UnityEngine;
using UnityEngine.AI;

public class MoveMouse : MonoBehaviour
{
    public Transform player; // Referencia al jugador
    public float detectionRadius = 5f; // Distancia para huir
    public float moveRadius = 10f; // Área en la que camina aleatoriamente
    public float speed = 2f; // Velocidad normal
    public float fleeSpeed = 5f; // Velocidad al huir
    public Animator animator;

    private NavMeshAgent agent;
    private Vector3 originalPosition;
    private bool isFleeing = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        originalPosition = transform.position;

        if (animator == null)
            animator = GetComponent<Animator>();

        Wander(); // Comienza moviéndose
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer < detectionRadius)
        {
            Flee();
        }
        else if (!agent.hasPath && !isFleeing)
        {
            Wander();
        }

        // **Animaciones**
        bool isMoving = agent.velocity.magnitude > 0.1f;
        animator.SetBool("isRunning", isMoving); // Usa Run si se mueve, Idle si no
    }

    void Wander()
    {
        Vector3 randomDirection = Random.insideUnitSphere * moveRadius;
        randomDirection += originalPosition;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomDirection, out hit, moveRadius, 1);
        agent.speed = speed;
        agent.SetDestination(hit.position);
    }

    void Flee()
    {
        isFleeing = true;
        Vector3 fleeDirection = (transform.position - player.position).normalized * moveRadius;
        Vector3 fleePosition = transform.position + fleeDirection;

        NavMeshHit hit;
        if (NavMesh.SamplePosition(fleePosition, out hit, moveRadius, 1))
        {
            agent.speed = fleeSpeed;
            agent.SetDestination(hit.position);
        }

        Invoke("StopFleeing", 3f); // Deja de huir después de 3 segundos
    }

    void StopFleeing()
    {
        isFleeing = false;
        Wander();
    }
}
