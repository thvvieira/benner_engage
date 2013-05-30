using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyFriends.Model;

namespace MyFriends.Design
{
    public class DesignDataService : IDataService
    {
        public async Task<IList<Friend>> GetFriends()
        {
            var result = new List<Friend>();

            for (var index = 0; index < 42; index++)
            {
                result.Add(
                    new Friend
                    {
                        DateOfBirth = (DateTime.Now - TimeSpan.FromDays(index)),
                        FirstName = "FirstName" + index,
                        LastName = "LastName" + index,
                        ImageUrl = "http://www.galasoft.ch/labs/json/Pictures/1.jpg"
                    });
            }

            return result;
        }
    }
}