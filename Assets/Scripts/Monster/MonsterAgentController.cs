using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterAgentController : MonoBehaviour
{

    private const float f_graceTime = 45f;

    [SerializeField] private NavMeshAgent meshAgent;
    [SerializeField] private bool seenPlayer = false;
    [SerializeField] private SpriteRenderer monsterSprite;
    [SerializeField] private PlayerMovementController playerMovementController;

    [SerializeField] private Camera m_Camera;
    float graceTimer = 0;

    private void Awake() { m_Camera = Camera.main; }

    private void Start()
    {
        StartCoroutine(GoToRandomPosition());
    }


    private void Update()
    {
        if (graceTimer >= 0)
        {
            graceTimer -= Time.deltaTime;
        }

    }

    //ToDo: This function should be called after each game Fight
    public void OnDanceFightCompletedAndWon()
    {
        graceTimer = f_graceTime;
        StopAllCoroutines();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (graceTimer >= 0) { return; }

        if (other.gameObject.CompareTag("Player"))
        {
            seenPlayer = true;
            StartCoroutine(LetsFollowBack());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (graceTimer >= 0) { return; }

        if (other.gameObject.CompareTag("Player")) { seenPlayer = false; }
        StartCoroutine(GoToRandomPosition());
    }


    private IEnumerator LetsFollowBack()
    {
        while (seenPlayer)
        {
            meshAgent.destination = GameObject.FindGameObjectWithTag("Player").transform.position;

            yield return new WaitForSeconds(Random.Range(0.15f, .45f));
        }
    }


    private IEnumerator GoToRandomPosition()
    {
        while (!seenPlayer)
        {
            meshAgent.destination = RandomNavmeshLocation(26);

            yield return new WaitForSeconds(Random.Range(0.4f, 1f));
        }
    }

    void LateUpdate()
    {
        transform.LookAt(transform.position + m_Camera.transform.rotation * Vector3.forward,
            m_Camera.transform.rotation * Vector3.up);
    }

    private void OnValidate()
    {
        if (meshAgent == null) { meshAgent = GetComponent<NavMeshAgent>(); }
        if (monsterSprite == null) { monsterSprite = GetComponentInChildren<SpriteRenderer>(); }
    }

    public Vector3 RandomNavmeshLocation(float radius)
    {
        Vector3 randomDirection = Random.insideUnitSphere * radius;
        randomDirection += transform.position;
        NavMeshHit hit;
        Vector3 finalPosition = Vector3.zero;
        if (NavMesh.SamplePosition(randomDirection, out hit, radius, 1))
        {
            finalPosition = hit.position;
        }
        return finalPosition;
    }

}
