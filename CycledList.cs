using System;

namespace DataStructureProject
{
    class Node
    {
        public int value;
        private Node _link;
        public Node link
        {
            get { return _link; }
        }
        public Node(int value, Node link)
        {
            this.value = value;
            this._link = link;
        }
        public Node(int value) : this(value, null) { }
        public Node() : this(0, null) { }
        public void Setlink(Node n)
        {
            this._link = n;
        }
    }

    class LinkedList
    {
        public Node first;

        public LinkedList()
        {
            first = new Node(-1);
            first.Setlink(first);
        }

        public bool IsEmpty()
        {
            if (first.link == first) return true;
            return false;
        }
        public void Add(Node node)
        {
            Node curr = first;
            while (curr.link != first && curr.link != null)
            {
                curr = curr.link;
            }
            curr.Setlink(node);
        }
        public void Add(int value)
        {
            Add(new Node(value, first));
        }
        public void RemoveNode(int index)
        {
            Node node = GetNodeI(index-1);
            if (node.link != null)
            {
                node.Setlink(node.link.link);
            }
        }
        public void PrintList()
        {
            if (IsEmpty()) Console.WriteLine("The list is empty!");
            Node curr = first.link;
            while (curr != first && curr != null)
            {
                Console.Write("{0} -> ", curr.value);
                curr = curr.link;
            }
            if (curr == first) Console.Write("first");
            else if (curr == null) Console.Write("null");
            Console.WriteLine();
        }
        public Node GetNodeI(int i)
        {
            int j = 0;
            Node curr = first.link;
            while (j != i)
            {
                j++;
                curr = curr.link;
                if (curr == null || curr==first) return null;
            }
            return curr;
        }
        public Node GetLastNode()
        {
            Node curr = first.link;
            while (curr.link != null && curr.link != first) curr = curr.link;
            return curr;
        }
        public bool IsCycled()
        {
            Node fast = first.link;
            Node slow = first;
            while (slow != null && fast != null)
            {
                if (slow == fast) return true;
                slow = slow.link;
                if (fast.link == null) return false;
                fast = fast.link.link;
            }
            return false;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            LinkedList list = new LinkedList();
            int input = 1;
            while (input != 0)
            {
                Console.WriteLine("1-Add to list\n2-Remove from list\n3-Set link of the last node\n4-check if list is cycled\n5-Print List\n0-end");
                input = Int32.Parse(Console.ReadLine());
                switch (input)
                {
                    case 1:
                        Console.WriteLine("enter the value to add to the list: ");
                        list.Add(Int32.Parse(Console.ReadLine()));
                        Console.Clear();
                        Console.WriteLine("added successfully.");
                        break;
                    case 2:
                        Console.WriteLine("enter the index of the node you want to remove from list: ");
                        list.RemoveNode(Int32.Parse(Console.ReadLine()));
                        Console.Clear();
                        Console.WriteLine("Removed successfully.");
                        break;
                    case 3:
                        Console.WriteLine("enter the index of the node you want last node refrence to: ");
                        list.GetLastNode().Setlink(list.GetNodeI(Int32.Parse(Console.ReadLine())));
                        Console.Clear();
                        Console.WriteLine("linked successfully.");
                        break;
                    case 4:
                        Console.Clear();
                        if (list.IsCycled()) Console.WriteLine("list is cycled.");
                        else Console.WriteLine("list is NOT cycled.");
                        break;
                    case 5:
                        list.PrintList();
                        break;
                    case 0:
                        break;

                }
            }
        }
    }
}
