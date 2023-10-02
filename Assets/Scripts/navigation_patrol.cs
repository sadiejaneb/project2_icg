// Patrol.cs
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;


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


    void GotoNextPoint()
    {
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
        if (isChasing)
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
        GotoNextPoint();
    }


}
