using System;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;
using System.Linq;

namespace Customer.DataAccess.Data
{
    public static class CosmoDB
    {
        public static CosmosClient client { get; private set; }

        private static Container containerobj;
        public static Container container
        {
            get => containerobj ?? GetOrCreateContainer().GetAwaiter().GetResult();
            private set => containerobj = value;
        }

        private const string customers = "custome";
        static CosmoDB()
        {
            var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

            var endpoint = config["cosmoEndPoint"];
            var masterKey = config["cosmoMasterKey"];

            client = new CosmosClient(endpoint, masterKey);
        }

        private static async Task<Container> GetOrCreateContainer()
        {
            var dbItrator = client.GetDatabaseQueryIterator<DatabaseProperties>();
            var database = await dbItrator.ReadNextAsync();

            var IscustomerAlradyExist = database.Any(d => d.Id == customers);

            return !IscustomerAlradyExist ? await CreateContainer() : client.GetContainer(customers, customers);
        }

        private static async Task<Container> CreateContainer()
        {
            var customerDb = await CosmoDB.client.CreateDatabaseAsync(customers);
            var newdatabase = client.GetDatabase(customerDb.Database.Id);

            var container = new ContainerProperties { Id = "customer3", PartitionKeyPath = "/pk/ZipCode" };

            return await newdatabase.CreateContainerAsync(container);
        }
    }
}
