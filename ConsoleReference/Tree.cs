using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleReference
{
    public enum TreeType
    {
        Unknown,
        Oak,
        Elm,
        Maple,
        Pine
    }

    public class Tree
    {
        public Guid Id { get; private set; }
        public TreeType Type { get; set; }
        public int Height { get; set; }
        public string Description { get; set; }
        public bool IsCutDown { get; private set; }

        public Tree(TreeType tree, int height)
        {
            Id = Guid.NewGuid();
            Type = tree;
            Height = height;
            IsCutDown = false;
        }

        public void CutDown()
        {
            IsCutDown = true;
        }
    }
}
