using System;
using System.Collections.Generic;
using System.Text;

namespace Miroku.Data.Entities
{
    public class Message : BaseEntity
    {
        public string Content { get; set; } = string.Empty;

        //public MessageRole MessageRole { get; set; }

        //private enum MessageRole
        //{
        //    User,
        //    Assistant,
        //    System
        //}
    }
}