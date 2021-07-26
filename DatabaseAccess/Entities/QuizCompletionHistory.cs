using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DatabaseAccess.Entities
{
    public class QuizCompletionHistory
    {
        public Guid Id { get; set; }

        public Guid QuizId { get; set; }

        public Quiz Quiz { get; set; }

        [Required]
        public string UserId { get; set; }

        public User User { get; set; }

        public ICollection<QuizQuestionUserAnswer> UserAnswers { get; set; }

    }
}