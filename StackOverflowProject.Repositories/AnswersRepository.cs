using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackOverflowProject.DomainModels;

namespace StackOverflowProject.Repositories
{
    public interface IAnswersRepository
    {
        void InsertAnswer(Answer answer);
        void UpdateAnswer(Answer answer);
        void UpdateAnswerVotesCount(int answerID, int userID, int value);
        void DeleteAnswer(int answerID);
        List<Answer> GetAnswersByQuestionID(int questionID);
        List<Answer> GetAnswersByAnswerID(int answerID);
    }

    public class AnswersRepository : IAnswersRepository
    {
        StackOverflowDatabaseDbContext db;
        IQuestionRepository qr;
        IVotesRepository vr;

        public AnswersRepository()
        {
            db = new StackOverflowDatabaseDbContext();
            qr = new QuestionsRepository();
            vr = new VotesRepository();
        }

        public void DeleteAnswer(int answerID)
        {
            var ans = db.Answers.Where(temp => temp.AnswerID == answerID).FirstOrDefault();
            if(ans != null)
            {
                db.Answers.Remove(ans);
                db.SaveChanges();
                qr.UpdateQuestionAnswersCount(ans.QuestionID, -1);
            }
        }

        public List<Answer> GetAnswersByAnswerID(int answerID)
        {
            return db.Answers.Where(temp => temp.AnswerID == answerID).ToList();
        }

        public List<Answer> GetAnswersByQuestionID(int questionID)
        {
            return db.Answers.Where(temp => temp.QuestionID == questionID).ToList();
        }

        public void InsertAnswer(Answer answer)
        {
            db.Answers.Add(answer);
            db.SaveChanges();
            qr.UpdateQuestionAnswersCount(answer.QuestionID, 1);
        }

        public void UpdateAnswer(Answer answer)
        {
            var ans = db.Answers.Where(temp => temp.AnswerID == answer.AnswerID).FirstOrDefault();
            if(ans != null)
            {
                ans.AnswerText = answer.AnswerText;
                db.SaveChanges();
            }
        }

        public void UpdateAnswerVotesCount(int answerID, int userID, int value)
        {
            var ans = db.Answers.Where(temp => temp.AnswerID == answerID).FirstOrDefault();
            if (ans != null)
            {
                ans.VotesCount += value;
                db.SaveChanges();
                qr.UpdateQuestionVotesCount(ans.QuestionID, value);
                vr.UpdateVote(answerID, userID, value);
            }
        }
    }
}
