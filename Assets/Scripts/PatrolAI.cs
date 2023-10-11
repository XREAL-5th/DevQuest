using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class PatrolAI : MonoBehaviour
{

    //NavMeshAgent agent = null;

    //// 맵에 설정한 wayPoint 순서대로 순찰함
    //[SerializeField] Transform[] wayPoints;
    //public float time;
    //int count = 0;

    //Transform target;

    //// 순찰 중단하고 _target 쫓으러 감
    //public void SetTarget(Transform _target)
    //{
    //    CancelInvoke();
    //    target = _target;
    //}

    //// target 없애고 다시 순찰 시작
    //public void RemoveTarget()
    //{
    //    target = null;
    //    InvokeRepeating("MoveToNextWayPoint", 0f, time);
    //}
    //void MoveToNextWayPoint()
    //{
    //    if (target == null)
    //    {
    //        if (agent.velocity == Vector3.zero)
    //        {
    //            print("next");
    //            agent.SetDestination(wayPoints[count++].position);

    //            if (count >= wayPoints.Length)
    //                count = 0;
    //        }

    //    }

    //}

    //private void Start()
    //{
    //    agent = GetComponent<NavMeshAgent>();
    //    InvokeRepeating("MoveToNextWayPoint", 0f, time);
    //}

    //void Update()
    //{
    //    if (target != null)
    //    {
    //        agent.SetDestination(target.position);
    //    }
    //}
    public Transform target;
    NavMeshAgent nmAgent;

    // Start is called before the first frame update
    void Start()
    {
        nmAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        nmAgent.SetDestination(target.position);
    }
}
