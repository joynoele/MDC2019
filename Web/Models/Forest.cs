using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Common;

namespace Web.Models
{
    public interface IForest
    {
        IEnumerable<Tree> Trees { get; }
        Tree FellTree(Guid id);
    }

    public class Forest : IForest
    {
        public IEnumerable<Tree> Trees => _trees.Where(t => !t.IsCutDown);
        private static IEnumerable<Tree> _trees;

        public Forest(IForestFactory factory)
        {
            if (factory == null) throw new ArgumentNullException(nameof(factory));
            _trees = factory.CreateForest(15); // make a forest of this many trees
        }

        public Tree FellTree(Guid id)
        {
            var target = _trees.Where(t => Guid.Equals(t.Id, id)).FirstOrDefault();
            if (target.IsCutDown)
            {
                // throw error or something
            }

            if (target == null)
            {
            }
            else
            {
                target.CutDown();
            }
            return target;
        }
    }
}
