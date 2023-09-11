using System.ComponentModel.DataAnnotations.Schema;

namespace Petty.Entities;

[Table("Photos")]
public class Photo
{
    public int Id { get; set; }
    public string Url { get; set; }
    public bool IsMain { get; set; }
    public string PublicId { get; set; }
    public int PetId { get; set; }
    public Pet Pet { get; set; }
}