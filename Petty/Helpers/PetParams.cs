namespace Petty.Helpers;

public class PetParams: PaginationParams
{
    public string Animal { get; set; }
    public string Breed { get; set; }
    public bool IsPermanentCare { get; set; }

    public int MinAge { get; set; } = 0;

    public int MaxAge { get; set; } = 20;
    
    
    


}