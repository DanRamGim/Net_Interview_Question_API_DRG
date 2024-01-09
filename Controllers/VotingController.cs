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
    public class VotingController : ControllerBase
    {

        private readonly ILogger<VotingController> _logger;

        public VotingController(ILogger<VotingController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public List<DBModel> Post([FromBody] DBModelVoting value)
        {
            using (var db = new DBModelContext())
            {
                var dataset = db.DBModel.Select(x => new { x.User, x.Question, x.Answer, x.QuestionTags, x.Votes }).ToList();

                if (value.Question != null && value.Question != "")
                {
                    dataset = db.DBModel.Where(x => x.Question == value.Question).Select(x => new { x.User, x.Question, x.Answer, x.QuestionTags, x.Votes }).ToList();
                }
                else if(value.Answer != null && value.Answer != "")
                {
                    dataset = db.DBModel.Where(x => x.Answer == value.Answer).Select(x => new { x.User, x.Question, x.Answer, x.QuestionTags, x.Votes }).ToList();
                }

                DBModel ModifyVotes = new DBModel();
                ModifyVotes.User = dataset[0].User;
                ModifyVotes.Question = value.Question;
                ModifyVotes.Answer = value.Answer;
                ModifyVotes.Votes = value.Votes;
                db.DBModel.Attach(ModifyVotes);
                db.Entry(ModifyVotes).Property(x => x.Votes).IsModified = true;
                db.SaveChanges();

                dataset = db.DBModel.Where(x => x.User == ModifyVotes.User).Select(x => new { x.User, x.Question, x.Answer, x.QuestionTags, x.Votes }).ToList();

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
