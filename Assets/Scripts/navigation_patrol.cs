// Patrol.cs
using UnityEngine;
using UnityEngine.AI;

public class navigation_patrol : MonoBehaviour
{

    public Transform[] points;
    private int destPoint = 0;
    private NavMeshAgent agent;
    private Animator anim;
    private Transform player;
    private bool isChasing = false;
    public float chasingDistance = 10f; // Distance within which NPC will chase the player
    private const float rotSpeed = 20f;
    public bool keepChasing = false;
    public bool isTreasureCollected = false;

    void Start()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();

        // Disabling auto-braking allows for continuous movement
        // between points (ie, the agent doesn't slow down as it
        // approaches a destination point).
        agent.autoBraking = false;
        agent.updateRotation = false;

        GotoNextPoint();
    }
    void OnEnable()
    {
        // Restart the patrol when the NPC is enabled
        GotoNextPoint();
    }

    void OnDisable()
    {
        // Stop the NavMeshAgent when the NPC is disabled
        if (agent != null)
        {
            agent.isStopped = true;
        }
    }


    void GotoNextPoint()
    {
        if (points.Length == 0 || agent == null || !agent.isActiveAndEnabled)
            return;
        // Returns if no points have been set up
        if (points.Length == 0)
            return;

        // Set the agent to go to the currently selected destination.
        agent.destination = points[destPoint].position;

        // This the sample code from the original Unity Manual. 
        // Choose the next point in the array as the destination,
        // cycling to the start if necessary.
        // destPoint = (destPoint + 1) % points.Length;

        // I added the code below.
        // Randomly pick the next destination
        int newDestPoint = 0;

        // Randomly pick the next destination from the list of destinations.
        // If the next destination happens to be the current location, try again. 
        do
        {
            newDestPoint = Random.Range(0, points.Length);
        } while (destPoint == newDestPoint);

        destPoint = newDestPoint;
    }


    void Update()
    {
        InstantlyTurn(agent.destination);
        if (ObjectCollision.PlayerIsPoweredUp || TreasureManager.permanentlyPoweredUp) // Check if the player is powered up
        {
            RunFromPlayer();
        }
        else if (isChasing || keepChasing)
        {
            agent.destination = player.position;
        }
        else
        {
            if (!agent.pathPending && agent.remainingDistance < 0.5f)
            {
                GotoNextPoint();
            }
        }
    }
    void RunFromPlayer()
    {
        if (player == null) return;

        float maxDistance = 0;
        Transform bestFleePoint = null;

        // Iterate through each patrol point to find the one furthest from the player
        foreach (Transform point in points)
        {
            float currentDistance = Vector3.Distance(player.position, point.position);
            if (currentDistance > maxDistance)
            {
                maxDistance = currentDistance;
                bestFleePoint = point;
            }
        }

        // If a valid point is found, set it as the destination
        if (bestFleePoint != null)
        {
            agent.destination = bestFleePoint.position;
        }
    }



    private void InstantlyTurn(Vector3 destination)
    {
        //When on target -> dont rotate!
        if ((destination - transform.position).magnitude < 0.1f) return;

        Vector3 direction = (destination - transform.position).normalized;
        Quaternion qDir = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, qDir, Time.deltaTime * rotSpeed);
    }
    public void StartChasing(Transform target)
    {
        isChasing = true;
        player = target;
    }

    public void StopChasing()
    {
        isChasing = false;
        player = null;
        if (agent != null && agent.isActiveAndEnabled)
        {
            GotoNextPoint();
        }
    }
    public bool IsKeepingChase()
    {
        return keepChasing;
    }
    public void NotifyTreasureCollected()
    {
        isTreasureCollected = true;
    }

}
