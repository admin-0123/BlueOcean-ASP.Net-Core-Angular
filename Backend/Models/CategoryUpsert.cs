namespace Virta.Models
{
    public class CategoryUpsert
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public bool Visible { get; set; }
        public int Priority { get; set; }
    }
}
