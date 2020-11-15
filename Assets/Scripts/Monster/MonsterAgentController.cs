using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterAgentController : MonoBehaviour
{
    [SerializeField] private NavMeshAgent meshAgent;
    [SerializeField] private bool seenPlayer = false;
    [SerializeField] private SpriteRenderer monsterSprite;
    [SerializeField] private PlayerMovementController playerMovementController;
    private Camera m_Camera = null;

    private void Awake()
    {
        m_Camera = Camera.main;
    }

    private void Start() { StartCoroutine(LetsFollowBack()); }
    private void Update()
    {
    }

    private IEnumerator LetsFollowBack()
    {
        while (true)
        {
            meshAgent.destination = GameObject.FindGameObjectWithTag("Player").transform.position;

            yield return new WaitForSeconds(Random.Range(0.15f, .45f));
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

}
