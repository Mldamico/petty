namespace Petty.DTO;

public class MemberDto
{
    public int Id { get; set; }
    public string UserName { get; set; }

    public int Age { get; set; }
    public DateTime Created { get; set; } = DateTime.UtcNow;
    public DateTime LastActive { get; set; } = DateTime.UtcNow;
    public string LookingFor { get; set; }
    public string City { get; set; }
    public string Country { get; set; }
}