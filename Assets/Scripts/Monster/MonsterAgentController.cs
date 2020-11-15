using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterAgentController : MonoBehaviour
{
    [SerializeField] private NavMeshAgent meshAgent;
    [SerializeField] private bool seenPlayer = false;

    private void Awake()
    {

    }

    void Start()
    {
        StartCoroutine(LetsFollowBack());
    }

    private IEnumerator LetsFollowBack()
    {
        while (true)
        {
            meshAgent.destination = GameObject.FindGameObjectWithTag("Player").transform.position;

            yield return new WaitForSeconds(1);
        }
    }
    private void OnValidate()
    {
        if (meshAgent == null) { meshAgent = GetComponent<NavMeshAgent>(); }
    }

}
