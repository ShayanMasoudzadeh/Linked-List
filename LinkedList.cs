using System;

namespace DataStructureProject
{
    class Node
    {
        public int value; //the variable each node holds in itself
        private Node _link; //next node which is held in the node
        public Node link //making link read-only
        {
            get { return _link; }
        }
        public void Setlink(Node n) //method for setting link (can be improved for more security)
        {
            this._link = n;
        }

        public Node(int value, Node link) //default constructor
        {
            this.value = value;
            this._link = link;
        }
        public Node(int value) : this(value, null) { } //constructor with default values for link
        public Node() : this(0, null) { } //constructor with default values for value and link
    }

    class LinkedList
    {
        public Node first; //holds the first node of the list

        public LinkedList() //constructor
        {
            first = new Node(-1); //adding an empty node (sentinel node)
            first.Setlink(first); //making it circular (first node's link refrences to itself)
        }

        public bool IsEmpty() //method for checking if a list is empty or not
        {
            if (first.link == first) return true; //if first node's link was refrencing the first node itself, the list is empty
            return false;
        }

        public void Add(Node node) //default method for adding a node to the end of a list
        {
            Node curr = first;
            while (curr.link != first) //while loop ends when curr reaches the last node
            {
                curr = curr.link;
            }
            curr.Setlink(node); //puts the new node in last node's link
        }
        public void Add(int value) //a variation of the Add method that receives an int and passess a node with that value to the default method
        {
            Add(new Node(value, first));
        }
        public void AddFirst(Node node) //default method for adding a node as the first node of the list (after the sentinel node)
        {
            node.Setlink(first.link); //puts the first (un-sentinel) node in the new nodes link
            first.Setlink(node); //sets the new node as the sentinel node's link
        }
        public void AddFirst(int value) //a variation of the AddFirst method that receives an int and passess a node with that value to the default method
        {
            AddFirst(new Node(value));
        }

        public void Delete(int value) //a method that deletes all the nodes in the list containing the given value
        {
            Node curr = first.link; //curr goes through the nodes of the list one by one
            Node prev = first; //prev is always one node behind the curr (we need to keep two nodes, because for deleting a node, we should change its previous node's link)
            while (curr != first) //the loop ends when curr reaches the last node
            {
                if (curr.value == value) //if curr's value was equal to the given value, it gets deleted
                {
                    prev.Setlink(curr.link);
                }
                else prev = prev.link; //(prev shouldn't go to the next node if the curr node got deleted)
                curr = curr.link;
            }
        }

        public void PrintList() //method for printing nodes of the list
        {
            if (IsEmpty()) //first checks if the list is empty using IsEmpty method
            {
                Console.WriteLine("The list is empty!");
                return;
            }
            Node curr = first.link;
            while (curr != first)
            {
                Console.Write("{0} -> ", curr.value);
                curr = curr.link;
            }
            Console.WriteLine();
        }

        //method for printing the nodes of the list in reverse using recursive function
        private void PrintReverseList(Node curr) //the recursive function
        {
            if (curr.link != first) PrintReverseList(curr.link); //if the current node isn't the last node, calls the method again
            Console.Write(" <- {0}", curr.value);
        }
        public void PrintReverseList() //the method that starts the recursive function of printing in reverse (this is the method that can be accessed from the main)
        {
            if (IsEmpty()) //checks if the list is empty using IsEmpty method
            {
                Console.WriteLine("The list is empty!"); 
                return;
            }
            PrintReverseList(first.link); //if list wasn't empty, starts the recursive function
            Console.WriteLine();
        }

        public int Size() //returns the number of nodes in a list (not counting the sentinel node)
        {
            if (IsEmpty()) return 0;
            int i = 0; //i holds the number of nodes counted
            Node curr = first.link;
            while (curr != first)
            {
                i++;
                curr = curr.link;
            }
            return i;
        }

        public void Swap(int i, int j) //method for swapping two nodes (swaps "i"th node with "j"th node)
        {
            //needs to hold each node and their previous node
            Node prevI;
            Node nodeI;
            Node prevJ;
            Node nodeJ;
            if (i == 0) //setting prevI and nodeI
            {
                prevI = first;
                nodeI = first.link;
            }
            else
            {
                prevI = GetNodeI(i - 1);
                nodeI = prevI.link;
            }
            if (j == 0) //setting prevJ and nodeJ
            {
                prevJ = first;
                nodeJ = first.link;
            }
            else
            {
                prevJ = GetNodeI(j - 1);
                nodeJ = prevJ.link;
            }
            //changing links to swap the nodes
            prevI.Setlink(nodeJ);
            prevJ.Setlink(nodeI);
            Node temp = nodeI.link;
            nodeI.Setlink(nodeJ.link);
            nodeJ.Setlink(temp);
        }
        private Node GetNodeI(int i) //receives an int as the index and returns the node of that index (is used in the Swap method)
        {
            if (i >= Size() || i < 0) return null; //checks if the given index isn't bigger than the size of the list or smaller than zero
            int j = 0;
            Node curr = first.link;
            while (j != i)
            {
                j++;
                curr = curr.link;
            }
            return curr;
        }
    }

    class Queue //implementing a Queue data structure using my LinkedList class
    {
        public LinkedList list;
        public Node front;
        public Node rear;
        public Queue()
        {
            list = new LinkedList();
            rear = front = null;
        }
        public void Add(int value)
        {
            Node node = new Node(value, list.first);
            list.Add(node);
            if (front == null) front = rear = node;
            else
            {
                rear.Setlink(node);
                rear = node;
            }
        }
        public void Remove(out int output)
        {
            if (front != null)
            {
                output = front.value;
                front = front.link;
                list.first.Setlink(front);
            }
            else output = -1;
        }
        public void Remove()
        {
            Remove(out int i);
        }
        public void Display()
        {
            list.PrintList();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            int input = 1;
            while (input != 0)
            {
                Console.Clear();
                Console.WriteLine("1-create new list\n2-create new queue\n0-end");
                input = Int32.Parse(Console.ReadLine());
                switch (input)
                {
                    case 1:
                        Console.Clear();
                        LinkedList list = new LinkedList();
                        while (input != -1)
                        {
                            Console.WriteLine("1-Add node to end\n2-Add to the beginning\n3-Delete a number from list\n4-print the list\n5-print the list in reverse\n6-return the size of the list\n7-swap two nodes\n0-end");
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
                                    Console.WriteLine("enter the value to add to the begginning of the list: ");
                                    list.AddFirst(Int32.Parse(Console.ReadLine()));
                                    Console.Clear();
                                    Console.WriteLine("added successfully.");
                                    break;
                                case 3:
                                    Console.WriteLine("enter the value to remove from the list: ");
                                    list.Delete(Int32.Parse(Console.ReadLine()));
                                    Console.Clear();
                                    Console.WriteLine("removed successfully.");
                                    break;
                                case 4:
                                    Console.Clear();
                                    list.PrintList();
                                    break;
                                case 5:
                                    Console.Clear();
                                    list.PrintReverseList();
                                    break;
                                case 6:
                                    Console.Clear();
                                    Console.WriteLine("list's size: {0}", list.Size());
                                    break;
                                case 7:
                                    Console.WriteLine("enter first node's index: ");
                                    int i = Int32.Parse(Console.ReadLine());
                                    Console.WriteLine("enter second node's index: ");
                                    list.Swap(i, Int32.Parse(Console.ReadLine()));
                                    Console.Clear();
                                    Console.WriteLine("swapped successfully.");
                                    break;
                                case 0:
                                    input = -1;
                                    break;
                            }
                        }
                        break;
                    case 2:
                        Console.Clear();
                        Queue queue = new Queue();
                        while (input != -1)
                        {
                            Console.WriteLine("1-Push to queue\n2-pull from queue\n3-display queue\n0-end");
                            input = Int32.Parse(Console.ReadLine());
                            switch (input)
                            {
                                case 1:
                                    Console.Write("Enter the value to be pushed to the queue: ");
                                    queue.Add(Int32.Parse(Console.ReadLine()));
                                    Console.Clear();
                                    Console.WriteLine("pushed successfully.");
                                    break;
                                case 2:
                                    queue.Remove(out int pull);
                                    Console.Clear();
                                    Console.WriteLine("pulled successfully: {0}",pull);
                                    break;
                                case 3:
                                    Console.Clear();
                                    queue.Display();
                                    break;
                                case 0:
                                    input = -1;
                                    break;
                            }
                        }
                        Console.Clear();
                        break;
                    case 0:
                        break;
                }
            }
        }
    }
}
