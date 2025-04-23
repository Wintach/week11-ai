using System;
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.JudgeSystem.ThirdPersonAction;
using static JudgeSystem;

// Don't forget to rename your class!
public class AgentController1650700402 : BaseAgent
{
    private NavMeshAgent NavAgent;
    private ThirdPersonAction CharactorAction;

    private void OnEnable()
    {
        JudgeSystem.OnMapEvent += OnMapEvent;
    }

    private void OnDisable()
    {
        JudgeSystem.OnMapEvent -= OnMapEvent;
    }

    // OnMapEvent is called once per event signal
    void OnMapEvent(EventTypes type, Vector3 pos)
    {
        Debug.Log(transform.name + " recived event: " + type);
    }

    private void Start()
    {
        NavAgent = GetComponent<NavMeshAgent>();
        CharactorAction = GetComponent<ThirdPersonAction>();
    }

    // Update is called once per frame
    void Update()
    {
        if(NavAgent.remainingDistance < 0.1f)
        {
            Vector3 newPos = RandomNavSphere(transform.position, 1.0f, -1);
            NavAgent.SetDestination(newPos);
        }

        // Do not remove this!
        CharactorAction.UpdateAction(NavAgent); 
    }

    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
     {
        Vector3 randDirection = UnityEngine.Random.insideUnitSphere * dist;
        randDirection += origin;
        NavMeshHit navHit;
        NavMesh.SamplePosition (randDirection, out navHit, dist, layermask);
        return navHit.position;
    }
}