using System.Collections.Generic;
using PathFinding.BehaviourTrees;
using UnityEngine;


[RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
public class NPCScript : MonoBehaviour

{
    [SerializeField] List<Transform> waypoints = new();
    UnityEngine.AI.NavMeshAgent agent;
    BehaviourTree tree;

    void Awake()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();

    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        tree = new BehaviourTree("NPC");
        Leaf patrol = new Leaf("Patrol", new PatrolStrategy(transform, agent, waypoints));
        tree.AddChild(patrol);
    }

    // Update is called once per frame
    void Update()
    {
        Node.Status ns = tree.Process();

        //Debug.Log($"The process returned {ns}");
    }
}
