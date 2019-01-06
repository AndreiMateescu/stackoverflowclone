using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackOverflowProject.DomainModels;

namespace StackOverflowProject.Repositories
{
    public interface IQuestionRepository
    {
        void InsertQuestion(Question question);
        void UpdateQuestionDetails(Question q);
        void UpdateQuestionVotesCount(int questionID, int value);
        void UpdateQuestionAnswersCount(int questionID, int value);
        void UpdateQuestionViewsCount(int questionID, int value);
        void DeleteQuestion(int questionID);
        List<Question> GetQuestions();
        List<Question> GetQuestionByQuestionID(int questionID);
    }

    public class QuestionsRepository : IQuestionRepository
    {
        StackOverflowDatabaseDbContext db;

        public QuestionsRepository()
        {
            db = new StackOverflowDatabaseDbContext();
        }

        public void DeleteQuestion(int questionID)
        {
            var qt = db.Questions.Where(temp => temp.QuestionID == questionID).FirstOrDefault();
            if (qt != null)
            {
                db.Questions.Remove(qt);
                db.SaveChanges();
            }
        }

        public List<Question> GetQuestions()
        {
            return db.Questions.ToList();
        }

        public List<Question> GetQuestionByQuestionID(int questionID)
        {
            return db.Questions.Where(temp => temp.QuestionID == questionID).ToList();
        }

        public void InsertQuestion(Question question)
        {
            db.Questions.Add(question);
            db.SaveChanges();
        }

        public void UpdateQuestionAnswersCount(int questionID, int value)
        {
            var qt = db.Questions.Where(temp => temp.QuestionID == questionID).FirstOrDefault();
            if (qt != null)
            {
                qt.AnswersCount += value;
                db.SaveChanges();
            }
        }

        public void UpdateQuestionDetails(Question q)
        {
            Question qt = db.Questions.Where(temp => temp.QuestionID == q.QuestionID).FirstOrDefault();
            if(qt != null)
            {
                qt.QuestionName = q.QuestionName;
                qt.QuestionDateAndTime = q.QuestionDateAndTime;
                qt.CategoryID = q.CategoryID;
                db.SaveChanges();
            }
        }

        public void UpdateQuestionViewsCount(int questionID, int value)
        {
            var qt = db.Questions.Where(temp => temp.QuestionID == questionID).FirstOrDefault();
            if (qt != null)
            {
                qt.ViewsCount += value;
                db.SaveChanges();
            }
        }

        public void UpdateQuestionVotesCount(int questionID, int value)
        {
            var qt = db.Questions.Where(temp => temp.QuestionID == questionID).FirstOrDefault();
            if (qt != null)
            {
                qt.VotesCount += value;
                db.SaveChanges();
            }
        }
    }
}
