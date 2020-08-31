using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace Customer.DataAccess.Data
{
    public static class CosmoDB
    {
        private static Container containerobj;
        private static Container replicaContainerobj;
        private static Container leaseContainerobj;

        private const string mainContainer = "customers";

        private const string dbId = "customers";
        private const string replicaContainer = "customersByDOB";
        private const string leaseContainer = "lease";

        private const string mainkey = "/customerId";
        private const string leaseKey = "/id";
        private const string replicaKey = "/dob";

        private readonly static int? timetoLive = 90 * 60 * 60 * 24;


        public static CosmosClient Client { get; private set; }
        public static Container MainContainer
        {
            get => containerobj ?? GetOrCreateMainContainer().GetAwaiter().GetResult();
            private set => containerobj = value;
        }

        public static Container LeaseContainer
        {
            get => leaseContainerobj ?? GetOrCreateLeaseontainer().GetAwaiter().GetResult();
            private set => leaseContainerobj = value;
        }

        public static Container ReplicaContainer
        {
            get => replicaContainerobj ?? GetOrCreateReplicaContainer().GetAwaiter().GetResult();
            private set => replicaContainerobj = value;
        }


        static CosmoDB()
        {
            var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

            Client = new CosmosClient(config["cosmoEndPoint"], config["cosmoMasterKey"]);
        }

        private static async Task<Container> GetOrCreateMainContainer()
        {
            await GetOrCreateDatabase();

            return await CreateContainer(dbId, mainContainer, mainkey, timetoLive);

        }

        private static async Task<Container> GetOrCreateReplicaContainer()
        {
            await GetOrCreateDatabase();

            return await CreateContainer(dbId, replicaContainer, replicaKey);
        }

        private static async Task<Container> GetOrCreateLeaseontainer()
        {
            await GetOrCreateDatabase();

            return await CreateContainer(dbId, leaseContainer, leaseKey);
        }

        private static async Task<FeedResponse<DatabaseProperties>> GetOrCreateDatabase()
        {
            var dbItrator = Client.GetDatabaseQueryIterator<DatabaseProperties>();
            var database = await dbItrator.ReadNextAsync();
            return database;
        }

        private static async Task<Container> CreateContainer(string dbId, string containerId, string key, int? ttl = null)
        {
            var customerDb = await CosmoDB.Client.CreateDatabaseIfNotExistsAsync(dbId);
            var newdatabase = Client.GetDatabase(customerDb.Database.Id);

            var container = new ContainerProperties { Id = containerId, PartitionKeyPath = key, DefaultTimeToLive = ttl };

            return await newdatabase.CreateContainerIfNotExistsAsync(container);
        }
    }
}
