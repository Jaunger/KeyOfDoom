using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class WayPointScript : MonoBehaviour
{
    public Transform[] wayPoints;
    public float speed = 5;
    private int currentWayPoint;
    public Vector3 target;
    public Animator animator;
    public Transform player;

    public float radius;
    [Range(0, 360)]
    public float angle;

    public GameObject playerRef;

    public LayerMask targetMask;
    public LayerMask obscructionMask;
    public bool canSeePlayer;

    private NavMeshAgent agent;
    private bool flag = true;
    public bool isDizzy = false;

    private void Start()
    {
        playerRef = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        currentWayPoint = 0;
    }

    private void Update()
    {
        if (!isDizzy) // Only perform actions if not stunned
        {
            MoveToWaypoint();
            FieldOfViewCheck();
        }
        if (!player.GetComponent<FmsScript>().isBeingChased)
        {
            this.gameObject.GetComponent<CapsuleCollider>().isTrigger = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            if (!isDizzy) // Only stun if not already stunned
            {
                StartCoroutine(StunEnemy());
            }
        }
    }

    private IEnumerator StunEnemy()
    {
        isDizzy = true; // Mark as stunned
        agent.isStopped = true; // Stop the NavMeshAgent
        animator.SetTrigger("isHit"); // Trigger "Dizzy Idle" animation
        animator.SetBool("isWalking", false);
        animator.SetBool("isChasing", false);
        animator.SetBool("isIdle", false);

        yield return new WaitForSeconds(3f); // Wait for 1 second
        Debug.Log("stun ober");
        isDizzy = false; // End stun
        agent.isStopped = false; // Resume NavMeshAgent
        animator.SetBool("isIdle", true); // Resume idle state

        // Additional logic to resume patrolling or chasing
        if (canSeePlayer)
        {
            onChase();
        }
        else
        {
            onPatrol();
        }
    }

    private void MoveToWaypoint()
    {

        if (wayPoints.Length == 0 && !canSeePlayer)
            return; // No waypoints set up.

        if (canSeePlayer)
        {
            speed = 11f;
            agent.speed = speed;
            target = playerRef.transform.position;
            onChase();
        }
        else
        {
            speed = 7f;
            agent.speed = speed;
            target = wayPoints[currentWayPoint].position;
            onPatrol();
        }

        agent.SetDestination(target);
        Vector3 moveDirection = target - transform.position;

        if (moveDirection.magnitude < 3 && flag && !canSeePlayer)
        {
            flag = false;
            StartCoroutine(StayForSeconds());
        }

        GetComponent<Rigidbody>().velocity = moveDirection.normalized * speed;
    }

    private IEnumerator StayForSeconds()
    {
        onWait();
        if (!canSeePlayer)
            yield return WaitOrEventCoroutine();
        else
            yield return null;
        currentWayPoint = (currentWayPoint + 1) % wayPoints.Length;
        flag = true;
    }

    IEnumerator WaitOrEventCoroutine()
    {
        float waitTime = 5f;
        float elapsedTime = 0f;

        while (elapsedTime < waitTime && !canSeePlayer)
        {
            yield return null;
            elapsedTime += Time.deltaTime;
        }
    }

    private void FieldOfViewCheck()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, targetMask);
        if (rangeChecks.Length != 0)
        {
            Transform target = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obscructionMask))
                    canSeePlayer = true;
                else
                    canSeePlayer = false;
            }
            else
                canSeePlayer = false;
        }
        else if (canSeePlayer)
            canSeePlayer = false;
    }

    public void onChase()
    {
        animator.SetBool("isChasing", true);
        animator.SetBool("isWalking", false);
        animator.SetBool("isIdle", false);
    }

    public void onPatrol()
    {
        if (flag)
        {
            animator.SetBool("isWalking", true);
            animator.SetBool("isChasing", false);
            animator.SetBool("isIdle", false);
        }
    }

    public void onWait()
    {
        flag = false;
        animator.SetBool("isIdle", true);
        animator.SetBool("isWalking", false);
    }
}
