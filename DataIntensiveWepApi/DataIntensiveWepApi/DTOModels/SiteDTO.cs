namespace DataIntensiveWepApi.DTOModels
{
    public class SiteDTO
    {
        public string SiteUuid { get; set; }
        public string Name { get; set; }
        public string City { get; set; }

        public string CustomerUuid { get; set; }

        public List<UserDTO> Users { get; set; }
    }
}
