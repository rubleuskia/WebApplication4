using System;
using System.ComponentModel.DataAnnotations;

namespace DatabaseAccess.Entities
{
    public class QuizQuestion
    {
        public Guid Id { get; set; }

        public Guid QuizId { get; set; }

        public Quiz Quiz { get; set; }

        [MaxLength(4000)]
        [Required]
        public string Text { get; set; }

        [MaxLength(4000)]
        [Required]
        public string Answer { get; set; }
    }
}