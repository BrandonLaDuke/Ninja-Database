using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using NinjaDomain.Classes;

namespace NinjaDomain.DataModel
{
  public class DisconnectedRepository
  {
    //public List<Posts> GetQueryableNinjasWithClan(string query, int page, int pageSize) {
    //  using (var context = new NinjaContext()) {
    //    context.Database.Log = message => Debug.WriteLine(message);
    //    var linqQuery = context.Posts.Include(n => n.Topic);
    //    if (!string.IsNullOrEmpty(query)) {
    //      linqQuery = linqQuery.Where(n => n.Name.Contains(query));
    //    }
    //    if (page > 0 && pageSize > 0) {
    //      linqQuery = linqQuery.OrderBy(n => n.Name).Skip(page - 1).Take(pageSize);
    //    }

    //    return linqQuery.ToList();
    //  }
    //}

    //public List<Posts> GetNinjasWithClan() {
    //  using (var context = new NinjaContext()) {
    //    //return context.Posts.Include(n => n.Topic).ToList();
    //    return context.Posts.AsNoTracking().Include(n => n.Topic).ToList();
    //  }
    //}

    public Post GetNinjaWithEquipment(int id) {
      using (var context = new NinjaContext()) {
        return context.Posts.AsNoTracking().Include(n => n.EquipmentOwned)
          .FirstOrDefault(n => n.Id == id);
      }
    }

    //public Posts GetNinjaWithEquipmentAndClan(int id) {
    //  using (var context = new NinjaContext()) {
    //    return context.Topics.AsNoTracking().Include(n => n.EquipmentOwned)
    //      .Include(n => n.Topic)
    //      .FirstOrDefault(n => n.Id == id);
    //  }
    //}

    //public IEnumerable GetClanList() {
    //  using (var context = new NinjaContext()) {
    //    return context.Topics.AsNoTracking().OrderBy(c => c.ClanName)
    //      .Select(c => new { c.Id, c.ClanName }).ToList();
    //  }
    //}

    public Post GetNinjaById(int id) {
      using (var context = new NinjaContext()) {
        return context.Posts.Find(id);
        // return context.Posts.AsNoTracking().SingleOrDefault(n => n.Id == id);
      }
    }

    public void SaveUpdatedNinja(Post posts) {
      using (var context = new NinjaContext()) {
        context.Entry(posts).State = EntityState.Modified;
        context.SaveChanges();
      }
    }

    public void SaveNewNinja(Post post) {
      using (var context = new NinjaContext()) {
        context.Posts.Add(post);
        context.SaveChanges();
      }
    }

    public void DeleteNinja(int postId) {
      using (var context = new NinjaContext()) {
        var post = context.Posts.Find(postId);
        context.Entry(post).State = EntityState.Deleted;
        context.SaveChanges();
      }
    }

    public void SaveNewEquipment(NinjaEquipment equipment, int postId) {
      //paying the price of not having a foreign key here. 
      //reason #857 why I prefer foreign keys!
      using (var context = new NinjaContext()) {
        var post = context.Posts.Find(postId);
        post.EquipmentOwned.Add(equipment);

        context.SaveChanges();
      }
    }

    public void SaveUpdatedEquipment(NinjaEquipment equipment, int postId) {
      //paying the price of not having a foreign key here. 
      //reason #858 why I prefer foreign keys!
      using (var context = new NinjaContext()) {
        var equipmentWithNinjaFromDatabase = 
          context.Equipment.Include(n => n.Post).FirstOrDefault(e => e.Id == equipment.Id);

        context.Entry(equipmentWithNinjaFromDatabase).CurrentValues.SetValues(equipment);

        context.SaveChanges();
      }
    }
   

    public NinjaEquipment GetEquipmentById(int id) {
      using (var context = new NinjaContext()) {
        return context.Equipment.Find(id);
      }
    }
  }
}