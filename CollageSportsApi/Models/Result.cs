
namespace CollageSportsApi.Models
{
    public class Result
    {
        public int resultId { get; set; }
        public int Id { get; set; }
        public int StudentId { get; set; } //foreignkey to student
        public int sportsName { get; set; } //foreign key to sports

        public string Position { get; set; }
        public string Score { get; set; }

    }
}
