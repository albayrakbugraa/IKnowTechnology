using Microsoft.AspNetCore.Http;

namespace IKnowTechnology.BLL.Models.DTOs
{
    public class UpdateUserDTO
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public IFormFile newPhoto { get; set; }
        public string ImagePath { get; set; }
    }
}