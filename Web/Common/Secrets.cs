using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Common
{
    public interface ISecrets
    {
        string MongoReadUsername { get; }
        string MongoReadPassword { get; }
        //... and other secrets
    }

    public class Secrets
    {
        private readonly ISecretRetriever _retriever;
        public Secrets(ISecretRetriever retriever)
        {
            _retriever = retriever ?? throw new ArgumentNullException(nameof(retriever));
        }
        public string MongoUsername => _retriever.GetValue("mongoUsername");
        public string MongoPassword => _retriever.GetValue("mongoPassword");
        //... and other secrets
    }
}
