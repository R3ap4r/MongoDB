using MongoDB.Bson.Serialization.Attributes;
using System;

namespace MongoDB
{
    /// <summary>
    /// Model for the Employee class
    /// </summary>
    public class EmployeeModel
    {
        [BsonId]
        public Guid id { get; set; }
        [BsonElement("EFirstName")]
        public string EFirstName { get; set; }
        [BsonElement("ELastName")]
        public string ELastName { get; set; }
        [BsonElement("EmployeeId")]
        public int EmployeeId { get; set; }

    }
}
