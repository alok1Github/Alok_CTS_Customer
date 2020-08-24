using System;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;

namespace Customer.DataAccess.Data
{
    public static class CosmoDB
    {
        public static CosmosClient client { get; private set; }
        static CosmoDB()
        {
            var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

            var endpoint = config["cosmoEndPoint"];
            var masterKey = config["cosmoMasterKey"];
            client = new CosmosClient(endpoint, masterKey);


        }
    }
}
