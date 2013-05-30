using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFriends.Helpers
{
    public enum FullNameSchema
    {
        FirstLast = 0,
        LastFirstComma = 1,
    }

    public class ChangeFullNameSchemaMessage
    {
        public ChangeFullNameSchemaMessage(FullNameSchema schema)
        {
            Schema = schema;
        }

        public FullNameSchema Schema
        {
            get;
            private set;
        }
    }
}
