namespace Petty.Helpers;

public class UserParams : PaginationParams
{
    public string CurrentUsername { get; set; }
    public string LookingFor { get; set; }

    public string OrderBy { get; set; } = "lastActive";
}