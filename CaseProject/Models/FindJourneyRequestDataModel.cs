namespace CaseProject.Models
{
    public class FindJourneyRequestDataModel
    {
        public string FromWhere { get; set; }
        public int FromWhereId { get; set; }
        public string ToWhere { get; set; }
        public int ToWhereId { get; set; }
        public string Date { get; set; }
    }
}
