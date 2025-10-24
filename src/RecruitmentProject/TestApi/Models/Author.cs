namespace RecruitmentProject.Test.TestApi.Models;

public class Author
{
    public int Id { get; set; }
    public int IdBook { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
}
