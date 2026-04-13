using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace PathFinding.BehaviourTrees
{
    public class Node
    {
        public enum Status { Success, Failure, Running }

        public readonly string name;
        public readonly int priority;

        public readonly List<Node> children = new();
        protected int currentChild;

        public Node(string name = "Node", int priority = 0)
        {
            this.name = name;
            this.priority = priority;
        }

        public void AddChild(Node child) => children.Add(child);

        public virtual Status Process() => children[currentChild].Process();

        public virtual void Reset()
        {
            currentChild = 0;
            foreach (var child in children)
            {
                child.Reset();
            }
        }
    }


    public class Leaf : Node
    {
        readonly IStrategy strategy;

        public Leaf(string name, IStrategy strategy, int priority = 0) : base(name, priority)
        {
            this.strategy = strategy;
        }

        public override Status Process() => strategy.Process();

        public override void Reset() => strategy.Reset();
    }
    public class BehaviourTree : Node
    {

        public BehaviourTree(string name) : base(name)
        {
        }

        public override Status Process()
        {
            while (currentChild < children.Count) { 
                var status = children[currentChild].Process();
                if(status != Status.Success)
                {
                    return status;
                }
                currentChild++;

            }
            return Status.Success;


        }

        public void PrintTree()
        {
            StringBuilder sb = new StringBuilder();
            PrintNode(this, 0, sb);
            Debug.Log(sb.ToString());
        }

        static void PrintNode(Node node, int indentLevel, StringBuilder sb)
        {
            sb.Append(' ', indentLevel * 2).AppendLine(node.name);
            foreach (Node child in node.children)
            {
                PrintNode(child, indentLevel + 1, sb);
            }
        }
    }



}
