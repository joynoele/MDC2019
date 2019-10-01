using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebReference.Common
{
    public interface ISecretRetriever
    {
        string GetValue(string key);
    }

    public class SecretRetriever : ISecretRetriever
    {
        public string GetValue(string key)
        {
            var value = string.Empty;

            // Fake retrieve value from secret store of choice
            switch (key)
            {
                case "username":
                    value = "user_name";
                    break;
                case "password":
                    value = "pa$$w0rd";
                    break;
                default:
                    value = "foobar";
                    break;
            }

            return value;
        }
    }
}
