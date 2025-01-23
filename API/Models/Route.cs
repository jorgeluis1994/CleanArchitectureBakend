namespace API.Models
{
    public class RoutesModels
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string StartLocation { get; set; }
        public string EndLocation { get; set; }
        public double Distance { get; set; }
    }
}