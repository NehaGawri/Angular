using System;

namespace DemoApp.API.Dtos
{
    public class photosForDetailedDto
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public DateTime DtaeAdded { get; set; }
        public bool IsMain { get; set; }
        // public int UserId { get; set; }
    }
}