using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.JudgeSystem.ThirdPersonAction;

public class NoobController : BaseAgent
{
    // Start is called before the first frame update
    private NavMeshAgent agent;
    private ThirdPersonAction CharactorAction;

    public float radius;

    [SerializeField]
    private float speed;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        CharactorAction = GetComponent<ThirdPersonAction>();

        RandomNewLocation();
    }

    // Update is called once per frame
    void Update()
    {
        speed = agent.speed;

        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            RandomNewLocation();
        }

        CharactorAction.UpdateAction(agent);
    }

    private void RandomNewLocation()
    {
        Vector3 targetRandomPosition = UnityEngine.Random.onUnitSphere * radius;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(targetRandomPosition, out hit, 1.0f, NavMesh.AllAreas))
        {
            agent.SetDestination(hit.position);
        }
        
    }
}
