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
    public class AnswersController : ControllerBase
    {

        private readonly ILogger<AnswersController> _logger;

        public AnswersController(ILogger<AnswersController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public List<DBModel> Post([FromBody] DBModelAnswer value)
        {
            using (var db = new DBModelContext())
            {
                var dataset = db.DBModel.Where(x => x.Question == value.Question).Select(x => new { x.User, x.Question, x.Answer, x.QuestionTags, x.Votes }).ToList();
                DBModel CreateAnswer = new DBModel();
                CreateAnswer.User = dataset[0].User;
                CreateAnswer.Question = value.Question;
                CreateAnswer.Answer = value.Answer;
                db.DBModel.Attach(CreateAnswer);
                db.Entry(CreateAnswer).Property(x => x.Answer).IsModified = true;
                db.SaveChanges();

                dataset = db.DBModel.Where(x => x.Question == value.Question).Select(x => new { x.User, x.Question, x.Answer, x.QuestionTags, x.Votes }).ToList();

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
