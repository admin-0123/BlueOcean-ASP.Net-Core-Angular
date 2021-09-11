namespace Virta.Api.DTO
{
    public class CategoryDTO
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public bool Visible { get; set; }
        public int Priority { get; set; }
    }
}
