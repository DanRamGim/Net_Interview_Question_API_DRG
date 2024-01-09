using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Net_Interview_Question_API
{
    public class BulkUpload
    {
        public static void DeleteEverything()
        {
            using (var db = new DBModelContext())
            {
                db.DBModel.RemoveRange(db.DBModel);
                db.SaveChanges();
            }
        }

        public static void LoadFile()
        {

            string dir = System.IO.Directory.GetCurrentDirectory();
            string file = dir + @"\FILE\questionnaire.csv";
            using (var db = new DBModelContext())
            {
                using (var reader = new StreamReader(file))
                {
                    List<string> listA = new List<string>();
                    int headers = 1;
                    while (!reader.EndOfStream)
                    {
                        if (headers != 1)
                        {
                            var line = reader.ReadLine();
                            var values = line.Split(',');
                            var questionnaire = new DBModel { User = values[0], Question = values[1], Answer = values[2], QuestionTags = values[3], Votes = values[4] };
                            db.DBModel.Add(questionnaire);
                            db.SaveChanges();
                        }
                        headers = 0;
                    }
                }
            }
        }
    }
}
