using DtMoneyAPI.Enums;
using System.ComponentModel.DataAnnotations;

namespace DtMoneyAPI.Models
{
    public class Transaction
    {
        public Transaction()
        {

        }

        public Transaction(int id, string title, double value, ETransactionType type, string category)
        {
            Id = id;
            Title = title;
            Value = value;
            Type = type;
            Category = category;
        }

        public int Id { get; set; }
        [Required(), MaxLength(100)]
        public string Title { get; set; }

        public double Value { get; set; }
        [Required]
        public ETransactionType Type { get; set; } = ETransactionType.In;
        [Required, MaxLength(100)]
        public string Category { get; set; }

    }
}
