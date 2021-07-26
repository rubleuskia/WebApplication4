using System;
using DatabaseAccess.Entities;

namespace DatabaseAccess.Quizzes
{
    public class QuizCompletionRecord
    {
        public Guid QuizId { get; set; }

        public Quiz Quiz { get; set; }

        public string UserId { get; set; }

        public User User { get; set; }

        public int CompletionCount { get; set; }
    }
}