using Petty.Entities;

namespace Petty.DTO;

public class PetDto
{
    public string Animal { get; set; }
    public string Breed { get; set; }
    public int Age { get; set; }
    public bool IsPermanentCare { get; set; }
    public string PhotoUrl { get; set; }
    public List<Photo> Photos { get; set; } = new List<Photo>();
}