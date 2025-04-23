using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalAnim : MonoBehaviour
{
    private GameObject judgeObject;
    private JudgeSystem judge;

    public bool randomSpeed = false;
    public float AnimSpeed = 30.0f;

    // Start is called before the first frame update
    void Start()
    {
        judgeObject = GameObject.FindGameObjectWithTag("Judge");
        judge = judgeObject.GetComponent<JudgeSystem>();

        if(randomSpeed)
        {
            AnimSpeed = Random.Range(20.0f,40.0f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, AnimSpeed * Time.deltaTime, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        judge.IssueCollider(other,gameObject);
        //Debug.Log("Target reached : " + other.transform.name);
    }
}
