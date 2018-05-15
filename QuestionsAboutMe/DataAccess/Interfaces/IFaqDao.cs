using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    /// <summary>
    /// Interface to allow mocking for unit tests, don't have Ninject yet though..
    /// </summary>
    public interface IFaqDao
    {
        Dictionary<string, string> GetFaqQuestions();
    }
}
