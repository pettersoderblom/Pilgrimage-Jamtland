namespace pilgrimsvandringen_grupp_5_e.ViewModels
{
    public class TrailViewModel
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
        public string StartPointCoords { get; set; }
        public string EndPointCoords { get; set; }
        public ICollection<StageViewModel> StageViewModels { get; set; }
        public ICollection<PointOfInterestViewModel> PointOfInterestViewModels { get; set;}
    }
}
