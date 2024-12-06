using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class NavManager : MonoBehaviour
{
    public Transform cafeteria;
    public Transform enterance;
    public Transform garbageDump;
    NavMeshAgent nmAgent;

    // Start is called before the first frame update
    void Start()
    {
        nmAgent = GetComponent<NavMeshAgent>();
    }

    public void ToCafeteria()
    {
        nmAgent.SetDestination(cafeteria.position);
    }
    public void ToEnterance()
    {
        nmAgent.SetDestination(enterance.position);
    }
    public void ToGarbageDump()
    {
        nmAgent.SetDestination(garbageDump.position);
    }
}
