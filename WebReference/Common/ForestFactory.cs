using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebReference.Models;

namespace WebReference.Common
{
    public interface IForestFactory
    {
        IEnumerable<Tree> CreateForest(int numberTrees);
    }
    public class ForestFactory : IForestFactory
    {
        private Random _random = new Random();
        private ILogger<ForestFactory> _log;
        public ForestFactory(ILogger<ForestFactory> log)
        {
            _log = log ?? throw new ArgumentNullException(nameof(log));
        }

        public IEnumerable<Tree> CreateForest(int numberTrees)
        {
            var forest = new List<Tree>();

            for (int i = 0; i < numberTrees; i++)
            {
                forest.Add(new Tree(GetRandomTreeType(), _random.Next(5, 100)));
            }

            return forest;
        }

        private TreeType GetRandomTreeType()
        {
            Array values = Enum.GetValues(typeof(TreeType));

            TreeType randomType = (TreeType)values.GetValue(_random.Next(values.Length));
            return randomType;
        }
    }
}
