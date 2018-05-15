using BLL.FaqDataWork.Interface;
using DataAccess.Dao;
using DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.FaqDataWork
{
    /// <summary>
    /// Anything related to grabbing FAQ data
    /// </summary>
    public class FaqRetriever : IFaqRetriever
    {
        public FaqRetriever()
        {
            FaqDao = new FaqDao();
        }

        IFaqDao FaqDao;

        /// <summary>
        /// Get ALL FAQ questions, will return null if the DAO returned no records. Simply added this layer to keep with standard considering its a review!
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, string> GetFaqQuestions()
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            try
            {
                result = FaqDao.GetFaqQuestions();
            }
            catch(Exception ex)
            {
                result = null; // Just want to confirm we always return null if there is an issue here
                // TODO: Get some logging in here, and for now we gotta catchem all
            }

            return result;
        }
    }
}
