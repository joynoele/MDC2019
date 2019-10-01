using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebReference.Models
{
    public enum TreeType
    {
        Unknown,
        Oak,
        Elm,
        Aspen,
        Pine
    }

    public class Tree
    {
        public Guid Id { get; private set; }
        private TreeType _type;
        public string Type { get => _type.ToString(); }
        public int Height { get; set; }
        public bool IsCutDown { get; private set; }

        public Tree(TreeType tree, int height)
        {
            Id = Guid.NewGuid();
            _type = tree;
            Height = height;
            IsCutDown = false;
        }

        public void CutDown()
        {

            IsCutDown = true;
        }

        public string ImageUrl
        {
            get
            {
                if (IsCutDown)
                    return @"\content\Stump.jpg";
                return this.Type switch
                {
                    "Elm" => @"\content\Elm.jpg",
                    "Oak" => @"\content\Oak.jpg",
                    "Aspen" => @"\content\Aspen.jpg",
                    "Pine" => @"\content\Pine.jpg",
                    _ => @"\content\Unknown.jpg",
                };
            }
        }
    }

}
