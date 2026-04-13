using System.Collections.Generic;
using PathFinding.BehaviourTrees;
using UnityEngine;


[RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
public class NPCScript : MonoBehaviour

{
    [SerializeField] List<Transform> waypoints = new();
    [SerializeField] GameObject monaLisa;
    [SerializeField] GameObject van;
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
        //Patrolling
        //Leaf patrol = new Leaf("Patrol", new PatrolStrategy(transform, agent, waypoints));
        //tree.AddChild(patrol);
        Leaf isMonaLisaPresent = new Leaf("IsMonaLisaPresent", new Condition(() => monaLisa.activeSelf));
        Leaf moveToMonaLisa = new Leaf("MoveToMonaLisa", new ActionStrategy(() => agent.SetDestination(monaLisa.transform.position)));

        Sequence goToMonaLisa = new Sequence("GoToMonalisa");
        goToMonaLisa.AddChild(isMonaLisaPresent);
        goToMonaLisa.AddChild(moveToMonaLisa);
        tree.AddChild(goToMonaLisa);

    }

    // Update is called once per frame
    void Update()
    {
        Node.Status ns = tree.Process();

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Equals("GetMonaLisa"))
        {
            monaLisa.SetActive(false);
        }
    }
}
