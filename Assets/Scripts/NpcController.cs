using UnityEngine;
using UnityEngine.AI;

public class NpcController : MonoBehaviour
{
    private Animator anim;

    public NavMeshAgent agent;

    [Range(0,100)] public float speed;
    [Range(1,500)] public float walkRadius;

    public void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();

        if(agent != null)
        {
            agent.speed = speed;
            agent.SetDestination(RandomNavMeshLocation());
        }
        
        
    }

    public void Update()
    {
        if(agent!= null && agent.remainingDistance <= agent.stoppingDistance)
        {
            agent.SetDestination(RandomNavMeshLocation());
        }
        //Debug.Log(agent.velocity);
        
        if(agent.velocity != Vector3.zero)
        {
            anim.SetInteger("animation", 1);
        }
    }

    public Vector3 RandomNavMeshLocation()
    {
        Vector3 finalPosition = Vector3.zero;
        Vector3 randomPosition = Random.insideUnitSphere * walkRadius;
        randomPosition += transform.position;
        if(NavMesh.SamplePosition(randomPosition, out NavMeshHit hit, walkRadius, 1))
        {
            finalPosition = hit.position;
        }
        return finalPosition;
    }
}
