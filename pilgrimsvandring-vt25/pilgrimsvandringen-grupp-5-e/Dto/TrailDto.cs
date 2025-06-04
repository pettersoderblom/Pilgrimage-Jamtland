namespace pilgrimsvandringen_grupp_5_e.Dto
{
    public class TrailDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string StartPoint { get; set; }
        public string EndPoint { get; set; }
        public string Terrain { get; set; }
        public string Difficulty { get; set; }
        public string Color { get; set; }
        public double Length { get; set; }
        public double Height { get; set; }
        public string Surface { get; set; }
    }
}
