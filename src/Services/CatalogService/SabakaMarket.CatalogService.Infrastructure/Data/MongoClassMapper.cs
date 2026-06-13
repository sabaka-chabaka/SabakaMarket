using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using SabakaMarket.CatalogService.Domain.Entities;

namespace SabakaMarket.CatalogService.Infrastructure.Data;

public static class MongoClassMapper
{
    private static bool _isRegistered;

    public static void RegisterMaps()
    {
        if (_isRegistered) return;

        BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String));

        BsonClassMap.RegisterClassMap<Product>(cm =>
        {
            cm.AutoMap();
            cm.MapIdProperty(c => c.Id);
            
            cm.MapField("SellerId").SetOrder(1);
            cm.MapField("Name").SetOrder(2);
            cm.MapField("Description").SetOrder(3);
            cm.MapField("Price").SetOrder(4);
            cm.MapField("Quantity").SetOrder(5);
            cm.MapField("CreatedAt").SetOrder(6);
            cm.MapField("IsActive").SetOrder(7);
        });

        _isRegistered = true;
    }
}