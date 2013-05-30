using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using GalaSoft.SimpleSerializer;

namespace MyFriends.Model
{
    public class DataService : IDataService
    {
        private const string ServiceUrl = "http://www.galasoft.ch/labs/json/JsonDemo.ashx";
        private readonly HttpClient _client;

        public DataService()
        {
            _client = new HttpClient();
        }

        public async Task<IList<Friend>> GetFriends()
        {
            var request = new HttpRequestMessage(
                HttpMethod.Post, 
                new Uri(ServiceUrl));

            var response = await _client.SendAsync(request);
            var result = await response.Content.ReadAsStringAsync();

            var serializer = new JsonSerializer();
            var deserialized = serializer.Deserialize<FacebookResult>(result);

            return deserialized.Friends;
        }
    }

    [SimpleSerialize]
    public class FacebookResult
    {
        [SimpleSerialize(FieldName = "data")]
        public List<Friend> Friends
        {
            get;
            set;
        }
    }
}