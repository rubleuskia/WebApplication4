using System;
using System.ComponentModel.DataAnnotations;

namespace DatabaseAccess.Entities
{
    public class QuizQuestionUserAnswer
    {
        public Guid Id { get; set; }

        public Guid QuizQuestionId { get; set; }

        public QuizQuestion QuizQuestion { get; set; }

        public string UserId { get; set; }

        public User User { get; set; }

        [MaxLength(4000)]
        public string ActualAnswer { get; set; }

        public bool IsCorrect { get; set; }

        public Guid QuizCompletionHistoryId { get; set; }

        public QuizCompletionHistory QuizCompletionHistory { get; set; }
    }
}