using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.FaqDataWork.Interface
{
    /// <summary>
    /// Just need this in case I get around to mocking with ninject 
    /// </summary>
    public interface IFaqRetriever
    {
        Dictionary<string, string> GetFaqQuestions();
    }
}
