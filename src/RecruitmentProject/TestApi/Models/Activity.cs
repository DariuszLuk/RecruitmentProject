namespace RecruitmentProject.Test.TestApi.Models;

public class Activity
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public DateTime DueDate { get; set; }
    public bool Completed { get; set; }
}
