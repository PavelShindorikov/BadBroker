using System.ComponentModel.DataAnnotations;

namespace Application.Contracts
{
    public class BestRevenueRequest
    {
        [Required]
        public DateTime StartDate { get; init; }
        [Required]
        public DateTime EndDate { get; set; }
        [Required]
        public decimal Money { get; set; }
    }
}