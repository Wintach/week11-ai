using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class atx : MonoBehaviour
{
    private GameObject judgeObject;
    private JudgeSystem judge;
    private NavMeshAgent agent;

    public float fSpeed;
    public int iScore;
    public int iIndex;
    public int iTimeStamp = int.MaxValue;
    public JudgeSystem.AgentStatus eStatus;
    [SerializeField]
    private Vector3 vPos = Vector3.zero;

    [SerializeField]
    private List<Vector3> speedRecoder = new List<Vector3>();

    // Start is called before the first frame update
    void Start()
    {
        judgeObject = GameObject.FindGameObjectWithTag("Judge");
        judge = judgeObject.GetComponent<JudgeSystem>();
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {

    }

    // Update is called once per frame
    void LateUpdate()
    {
        Tracking();
    }

    void Tracking()
    {
        if (agent.speed != fSpeed)
        {
            Debug.LogError("Speed Violation(" + agent.speed + "|" + fSpeed + ") : " + transform.name);
            judge.IssueViolation(transform.gameObject);
        }
        else
        {
            if (vPos != Vector3.zero)
            {
                float mag = Vector3.Magnitude(vPos - transform.position);
                if ((mag > fSpeed+1.0f) && (fSpeed >= 0.1f))
                {
                    Debug.LogError("Warp Violation(" + mag + "|" + fSpeed + ") : " + transform.name);
                    Debug.Break();
                    judge.IssueViolation(transform.gameObject);
                }
            }
        }
        
        vPos = transform.position;
        speedRecoder.Add(vPos);
        if (speedRecoder.Count >= 10)
        {
            speedRecoder.RemoveAt(0);
        }
    }
}
