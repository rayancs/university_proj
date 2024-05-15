namespace university_proj.DataModels;

public class ShoeModel
{
    public Guid Id{get;set;}
    public string SerialNumber { get;set;}
    public string Type{get;set;}
    public string Status{get;set;}
    public int usage { get ;set;} = 0;
}
