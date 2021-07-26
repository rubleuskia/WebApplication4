using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DatabaseAccess.Entities;

namespace DatabaseAccess.Quizzes
{
    public class Quiz
    {
        public Guid Id { get; set; }

        [MaxLength(1000)]
        public string Name { get; set; }

        public Guid CreatedById { get; set; }

        public User CreatedBy { get; set; }

        public int CorrectAnswersCount { get; set; }

        public ICollection<QuizQuestion> Questions { get; set; }
    }
}