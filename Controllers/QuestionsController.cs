using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Net_Interview_Question_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class QuestionsController : ControllerBase
    {

        private readonly ILogger<QuestionsController> _logger;

        public QuestionsController(ILogger<QuestionsController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public List<DBModel> Post([FromBody] DBModelQuestion value)
        {
            using (var db = new DBModelContext())
            {
                DBModel CreateQuestion = new DBModel();
                CreateQuestion.Question = value.Question;
                CreateQuestion.User = value.User;
                CreateQuestion.QuestionTags = value.QuestionTags;
                db.DBModel.Add(CreateQuestion);
                db.SaveChanges();

                var dataset = db.DBModel.Where(x => x.User == value.User).Select(x => new { x.User, x.Question, x.Answer, x.QuestionTags, x.Votes }).ToList();

                List<DBModel> TotalList = new List<DBModel>();
                foreach (var item in dataset)
                {
                    TotalList.Add(new DBModel
                    {
                        User = item.User,
                        Question = item.Question,
                        Answer = item.Answer,
                        QuestionTags = item.QuestionTags,
                        Votes = item.Votes
                    });
                }
                return TotalList;
            }
        }


        [HttpGet]
        public List<DBModel> Get([FromQuery] DBModelTags value)
        {
            using (var db = new DBModelContext())
            {
                var dataset = db.DBModel.Where(x => x.QuestionTags.Contains(value.QuestionTags)).Select(x => new { x.User, x.Question, x.Answer, x.QuestionTags, x.Votes }).ToList();

                List<DBModel> TotalList = new List<DBModel>();
                foreach (var item in dataset)
                {
                    TotalList.Add(new DBModel
                    {
                        User = item.User,
                        Question = item.Question,
                        Answer = item.Answer,
                        QuestionTags = item.QuestionTags,
                        Votes = item.Votes
                    });
                }

                return TotalList;
            }
        }


        private object GetObjectValues(object entity)
        {
            throw new NotImplementedException();
        }
    }
}
