using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;

namespace TableStore_console_app
{
    class Program
    {
        private static string _connectionString = "DefaultEndpointsProtocol=https;AccountName=ACCOUNTNAME;AccountKey=ACCOUNTKEY";

        static void Main(string[] args)
        {
            //Create a storage account object.
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(_connectionString);
            
            // Create the table client.
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            // Retrieve a reference to the table.
            CloudTable table = tableClient.GetTableReference("player");

            // Create the table if it doesn't exist.
            table.CreateIfNotExists();

            // Get All Players for a sport
            TableQuery<PlayerEntity> query = new TableQuery<PlayerEntity>().Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, "soccer"));

            int iCount = 0;
            foreach (PlayerEntity entity in table.ExecuteQuery(query))
            {
                Console.WriteLine("Club:" + entity.Club_VC + " Player Name: " + entity.First_Name_VC + " " + entity.Last_Name_VC + " Position: " + entity.Position_VC);

                iCount++;
            }

            CreateData("soccer", iCount.ToString(), "Marcus", "Rashford", "Man Utd", "Striker");

            Console.ReadKey();
        }

        static Boolean CreateData(string sSport, string sRow, string sFirstName, string sLastName, string sClub, string sPostition)
        {
            Boolean bSuccess = false;

            // Create our player
           
            // Create the entity with a partition key for sport and a row
            // Row should be unique within that partition
            PlayerEntity _record = new PlayerEntity(sSport, sRow);

            _record.Sport_VC = sSport;
            _record.First_Name_VC = sFirstName;
            _record.Last_Name_VC = sLastName;
            _record.Club_VC = sClub;
            _record.Position_VC = sPostition;
            
            //Create a storage account object.
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(_connectionString);

            // Create the table client.
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            // Retrieve a reference to the table.
            CloudTable table = tableClient.GetTableReference("player");


            // Create the TableOperation object that inserts the customer entity.
            TableOperation insertOperation = TableOperation.Insert(_record);

            try
            {

                // Execute the insert operation.
                table.Execute(insertOperation);

                bSuccess = true;
            }
            catch (Exception ex)
            {
                bSuccess = false;
            }


            return bSuccess;
        }
    }
}
