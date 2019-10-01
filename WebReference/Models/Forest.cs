using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebReference.Common;

namespace WebReference.Models
{
    public interface IForest
    {
        IEnumerable<Tree> Trees { get; }
        Tree FellTree(Guid id);
    }

    public class Forest : IForest
    {
        private ILogger<Forest> _log;
        public IEnumerable<Tree> Trees => _trees.Where(t => !t.IsCutDown);
        private static IEnumerable<Tree> _trees;

        public Forest(ILogger<Forest> log, IForestFactory factory)
        {
            _log = log ?? throw new ArgumentNullException(nameof(log));
            if (factory == null) throw new ArgumentNullException(nameof(factory));
            _trees = factory.CreateForest(15); // make a forest of this many trees
        }

        public Tree FellTree(Guid id)
        {
            var target = _trees.Where(t => Guid.Equals(t.Id, id)).FirstOrDefault();
            if (target.IsCutDown)
                _log.LogWarning("cannot cut down tree that has already been felled.");

            if (target == null)
            {
                _log.LogWarning("Cannot cut down tree with id {treeid} that does not exist in the forest", id);
            }
            else
            {
                target.CutDown();
                _log.LogDebug("Cut down {tree}", target);
            }
            return target;
        }
    }
}
