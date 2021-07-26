using System;

namespace DatabaseAccess.Entities
{
    public class QuizRating
    {
        public Guid Id { get; set; }

        public Guid QuizId { get; set; }

        public Quiz Quiz { get; set; }

        public string UserId { get; set; }

        public User User { get; set; }

        public int Rating { get; set; }
    }
}