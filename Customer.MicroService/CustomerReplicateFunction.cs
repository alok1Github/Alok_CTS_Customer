﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.Documents;

namespace Customer.MicroService
{
    class CustomerReplicateFunction
    {
        private static readonly CosmosClient client;
        static CustomerReplicateFunction()
        {
            var connStr = Environment.GetEnvironmentVariable("CosmosDbConnectionString");
            client = new CosmosClient(connStr);
        }

        [FunctionName("ReplicateCustomer")]
        public static async Task ReplicateCustomer(
            [CosmosDBTrigger(
                databaseName: "customers",
                collectionName: "customers",
                ConnectionStringSetting = "CosmosDbConnectionString",
                LeaseCollectionName = "lease",
                LeaseCollectionPrefix = "ReplicateCustomer"
            )] IReadOnlyList<Document> documents, ILogger logger)
        {
            var container = client.GetContainer("customers", "customersByDOB");

            foreach (var document in documents)
            {
                try
                {
                    await container.UpsertItemAsync(document);
                    logger.LogWarning($"Upserted document id {document.Id} in replica container");

                }
                catch (Exception ex)
                {
                    logger.LogError($"Error processing change for document id {document.Id}: {ex.Message}");
                }
            }
        }

    }
}
