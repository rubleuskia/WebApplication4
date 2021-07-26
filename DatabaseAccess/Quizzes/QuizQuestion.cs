using System;
using System.ComponentModel.DataAnnotations;

namespace DatabaseAccess.Quizzes
{
    public class QuizQuestion
    {
        public Guid Id { get; set; }

        [MaxLength(3000)]
        public string Question { get; set; }

        [MaxLength(3000)]
        public string Answer { get; set; }

        public Guid QuizId { get; set; }

        public Quiz Quiz { get; set; }
    }
}