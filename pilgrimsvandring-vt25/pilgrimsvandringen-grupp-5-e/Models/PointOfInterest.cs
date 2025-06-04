namespace pilgrimsvandringen_grupp_5_e.Models
{
    public class PointOfInterest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public int TrailId { get; set; }
        public Trail Trail {  get; set; } 
    }
}
