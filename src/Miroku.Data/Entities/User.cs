using System;
using System.Collections.Generic;
using System.Text;

namespace Miroku.Data.Entities
{
    public class User : BaseEntity
    {
        public List<Conversation> Conversations { get; set; } = [];
    }
}