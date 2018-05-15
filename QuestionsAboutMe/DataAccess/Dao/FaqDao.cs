using Amazon;
using Amazon.DynamoDb;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
using DataAccess.Enums;
using DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Dao
{
    /// <summary>
    /// Any calls made to the FAQ table need to come through here
    /// </summary>
    public class FaqDao : IFaqDao
    {
        /// <summary>
        /// Will return null if no records were found, gets everything from the FAQ table
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, string> GetFaqQuestions()
        {
            Dictionary<string, string> result = null;

            try
            {
                // Make sure this DB client gets cleaned up, fastest route is the using statement
                // TODO: Lets get these keys encrypted and in the web config or the database(Maybe BCrypt?)
                using (AmazonDynamoDBClient amazonDynamoDBClient = new AmazonDynamoDBClient("AKIAINS26T6JVAGVNZ2A", "qolTppubmJ5XWBl1UtHE42HPs7lULGgF2t8okG0J", RegionEndpoint.USEast1))
                {
                    Amazon.DynamoDBv2.Model.BatchGetItemRequest itemRequest = new Amazon.DynamoDBv2.Model.BatchGetItemRequest();

                    // Apparently you can't rename tables in Dynamo so I have to re-write the table and copy the data. Not top priority for getting inital APP done
                    Table FaqTable = Table.LoadTable(amazonDynamoDBClient, Tables.MyFaqQuetsions.ToString());

                    ScanFilter scanFilter = new ScanFilter();
                    scanFilter.AddCondition("MyFaqQuestions_pk", ScanOperator.GreaterThan, 0); // Lets grab all the rows since we know there will never be too many to do so

                    Search search = FaqTable.Scan(scanFilter);

                    List<Document> documents = search.GetRemaining();

                    if (documents != null)
                    {
                        result = new Dictionary<string, string>();
                        foreach (Document document in documents)
                        {
                            string answer = document.Values.ToList()[0].AsString();

                            string question = document.Values.ToList()[2].AsString();

                            List<string> names = document.GetAttributeNames();

                            result.Add(question, answer);

                            //if(names != null)
                            //{
                            //    foreach(string name in names)
                            //    {
                            //        DynamoDBEntry storedValue;

                            //        if (document.TryGetValue(name, out storedValue))
                            //        {
                            //            result.Add(name, storedValue);
                            //        }
                            //    }
                            //}
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // TODO: Pass this exception back up to the BLL to be handled, maybe implement a retry count first?
                throw ex;
            }

            return result;
        }

        /// <summary>
        /// Just here to save my test code for adding records in case I end up needing something for it
        /// </summary>
        public void InsertRecords()
        {
            Dictionary<string, string> result = null;

            AmazonDynamoDBClient amazonDynamoDBClient = new AmazonDynamoDBClient("AKIAINS26T6JVAGVNZ2A", "qolTppubmJ5XWBl1UtHE42HPs7lULGgF2t8okG0J", RegionEndpoint.USEast1);

            Dictionary<string, AttributeValue> attributes = new Dictionary<string, AttributeValue>();
            AttributeCollection collection = new AttributeCollection();
            attributes.Add("1", new AttributeValue() { N = "1" }); // Primary key, need to look into getting this updated automatically
            attributes.Add("firstrecord", new AttributeValue("First"));
            attributes.Add("secondrecord", new AttributeValue("secondrecord"));

            // Create PutItem request
            Amazon.DynamoDBv2.Model.PutItemRequest request = new Amazon.DynamoDBv2.Model.PutItemRequest("Test", attributes);
            try
            {
                // Issue PutItem request
                amazonDynamoDBClient.PutItem(request);
            }
            catch (Exception ex)
            {

            }
        }
    }
}
