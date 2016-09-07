using NinjaDomain.Classes;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NinjaDomain.DataModel
{
  public class DataHelpers
  {
    public static void NewDbWithSeed() {
     
      Database.SetInitializer(new DropCreateDatabaseAlways<NinjaContext>());
      using (var context = new NinjaContext())
      {
          if (context.Posts.Any())
          {
              return;
          }
          var vtClan = context.Topics.Add(new Topic { });
          var turtleClan = context.Topics.Add(new Topic { });
          var amClan = context.Topics.Add(new Topic { });

          var j = new Post
          {
              Name = "JulieSan",
              ServedInOniwaban = false,
              DateOfBirth = new DateTime(1980, 1, 1),


          };
          var s = new Post
          {
              Name = "SampsonSan",
              ServedInOniwaban = false,
              DateOfBirth = new DateTime(2008, 1, 28),


          };
      }
    }
  }
}

