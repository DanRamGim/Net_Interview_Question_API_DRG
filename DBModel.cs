using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Net_Interview_Question_API
{
    public class DBModel
    {
        [Key]
        public string User { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public string QuestionTags { get; set; }
        public string Votes { get; set; }

    }

    public class DBModelQuestion
    {
        
        public string User { get; set; }
        [Key]
        public string Question { get; set; }
        public string QuestionTags { get; set; }
    }

    public class DBModelAnswer
    {
        public string Question { get; set; }

        public string Answer { get; set; }
    }

    public class DBModelTags
    {
        [FromQuery]
        public string QuestionTags { get; set; }
    }

    public class DBModelVoting
    {
        [Key]
        public string Question { get; set; }

        public string Answer { get; set; }

        public string Votes { get; set; }
    }


    public class DBModelContext : DbContext
    {
        public DbSet<DBModel> DBModel { get; set; }
    }
}
