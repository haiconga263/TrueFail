using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Models
{
    public class MongDBBaseModel
    {
        [BsonId]
        public ObjectId Id { get; set; }
    }
}
