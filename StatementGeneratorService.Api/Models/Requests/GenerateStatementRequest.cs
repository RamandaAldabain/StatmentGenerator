namespace StatementGeneratorService.Api.Models.Requests
{
    public class GenerateStatementRequest
    {
        public string CustomerId { get; set; }

        public int Month { get; set; }

        public int Year { get; set; }
    }
}
