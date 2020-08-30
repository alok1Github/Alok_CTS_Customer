using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace Customer.DataAccess.Data
{
    public static class CosmoDB
    {
        public static CosmosClient Client { get; private set; }
        public static Container Container
        {
            get => containerobj ?? GetOrCreateContainer().GetAwaiter().GetResult();
            private set => containerobj = value;
        }

        private static Container containerobj;
        private const string Customers = "customers";
        private const string Key = "/id";

        static CosmoDB()
        {
            var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

            Client = new CosmosClient(config["cosmoEndPoint"], config["cosmoMasterKey"]);
        }

        private static async Task<Container> GetOrCreateContainer()
        {
            var dbItrator = Client.GetDatabaseQueryIterator<DatabaseProperties>();
            var database = await dbItrator.ReadNextAsync();

            var IscustomerAlradyExist = database.Any(d => d.Id == Customers);

            return !IscustomerAlradyExist ? await CreateContainer() : Client.GetContainer(Customers, Customers);
        }

        private static async Task<Container> CreateContainer()
        {
            var customerDb = await CosmoDB.Client.CreateDatabaseAsync(Customers);
            var newdatabase = Client.GetDatabase(customerDb.Database.Id);

            var container = new ContainerProperties { Id = Customers, PartitionKeyPath = Key };

            return await newdatabase.CreateContainerAsync(container);
        }
    }
}
