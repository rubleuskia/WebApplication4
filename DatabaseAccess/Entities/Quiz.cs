using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DatabaseAccess.Entities
{
    public class Quiz
    {
        public Guid Id { get; set; }

        [MaxLength(50)]
        public string Name { get; set; }

        // TODO remmove
        public bool IsCompleted { get; set; }

        public ICollection<QuizQuestion> Questions { get; set; }

        public ICollection<QuizCompletionHistory> QuizCompletions { get; set; }
    }
}