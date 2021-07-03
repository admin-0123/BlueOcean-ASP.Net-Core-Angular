namespace Virta.Entities
{
    public class Cart : MongoBaseDocument
    {
        public string UserId { get; set; }
        public string[] ProductIds { get; set; }
    }
}
