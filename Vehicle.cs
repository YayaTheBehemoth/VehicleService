namespace VehicleService;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
public class Vehicle
{
[BsonId]
[BsonRepresentation(BsonType.ObjectId)]
public string? Id {get; set;}

[BsonElement("marke")]

public string? marke{get; set;}

public int? afstand{get; set;}

}