namespace Petty.Helpers;

public class PetParams
{
    private const int MaxPageSize = 50;
    public int PageNumber { get; set; } = 1;
    private int _pageSize = 10;

    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
    }
    public string Animal { get; set; }
    public string Breed { get; set; }
    public bool IsPermanentCare { get; set; }

    public int MinAge { get; set; } = 0;

    public int MaxAge { get; set; } = 20;
    
    
    


}