namespace Petty.Entities;

public class Pet
{
    public int Id { get; set; }
    public string Animal { get; set; }
    public string Breed { get; set; }
    public int Age { get; set; }
    public bool IsPermanentCare { get; set; }
    public List<Photo> Photos { get; set; } = new List<Photo>();
}