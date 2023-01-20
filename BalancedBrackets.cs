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
    }
    class Stack //implementation of basic stack data structure using my LinkedList class
    {
        private LinkedList list;
        public Node top;
        public Stack()
        {
            list = new LinkedList();
            top = null;
        }
        public void Add(int value)
        {
            Node node = new Node(value);
            list.AddFirst(node);
            top = node;
        }
        public void Remove(out int x)
        {
            if (top == null)
            {
                x = -1;
                return;
            }
            x = top.value;
            list.first.Setlink(top.link);
            top = top.link;
        }
        public bool IsEmpty()
        {
            if (list.IsEmpty()) return true;
            return false;
        }
    }
    class Program
    {
        static public bool IsBalanced(string str) //a function that determines if a sequence of brackets are balanced* or not using stack data structure (*balanced here means that if each open bracket closes before a previous opened bracket closes)
        {
            Stack stack = new Stack();
            for (int i = 0; i < str.Length; i++) //this loop goes through each character of the given string
            {
                switch (Convert.ToInt32(str[i])) //for convinience each character is converted to its askii code
                {
                    //if the input is an open bracket, it gets pushed in the stack
                    case 40:
                    case 91:
                    case 123:
                        stack.Add(Convert.ToInt32(str[i]));
                        break;
                    //if the input is a closed bracket, last open bracket that got added to the stack gets pulled to be checked if it's the same kind of bracket as the input or not
                    //if it was a different kind of bracket, the sequence is unbalanced, if it was the same kind, the loop goes on
                    case 41:
                        {
                            stack.Remove(out int x);
                            if (x != 40) return false;
                            break;
                        }
                    case 93:
                        {
                            stack.Remove(out int x);
                            if (x != 91) return false;
                            break;
                        }
                    case 125:
                        {
                            stack.Remove(out int x);
                            if (x != 123) return false;
                            break;
                        }
                }
            }
            /*if (stack.IsEmpty()) return true;
            return false;*/
            //the lower line replaces the two upper lines
            return stack.IsEmpty(); //at the end, if the stack was empty (meaning every opened bracket closed), the sequence was balanced
        }

        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("enter the sequence of brackets:");
                if (IsBalanced(Console.ReadLine())) Console.WriteLine("the sequence is balanced\n");
                else Console.WriteLine("the sequence is NOT balanced\n");
            }
        }
    }
}