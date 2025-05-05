using MongoDB.Bson;

namespace mamisum_api.Helpers
{
    public static class ObjectIdExtensions
    {
        public static DateTime CreationTimeFromObjectId(this string objectId)
        {
            return ObjectId.Parse(objectId).CreationTime;
        }
    }
}
