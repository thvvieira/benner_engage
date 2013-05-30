using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFriends.Model
{
    public interface IDataService
    {
        Task<IList<Friend>> GetFriends();
    }
}
