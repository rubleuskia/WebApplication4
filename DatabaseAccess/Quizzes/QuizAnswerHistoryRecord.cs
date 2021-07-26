using System;
using DatabaseAccess.Entities;

namespace DatabaseAccess.Quizzes
{
    public class QuizAnswerHistoryRecord
    {
        public Guid Id { get; set; }

        public Guid QuestionId { get; set; }

        public QuizQuestion Question { get; set; }

        public string UserId { get; set; }

        public User User { get; set; }

        public string Answer { get; set; }

        public bool IsCorrect { get; set; }

        public Guid CompletionAttemptId { get; set; }
    }
}