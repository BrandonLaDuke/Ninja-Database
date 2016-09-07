using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using NinjaDomain.Classes;

namespace NinjaDomain.DataModel
{
  public class ConnectedRepository
  {
    private readonly NinjaContext _context = new NinjaContext();

    public Post GetNinjaWithEquipment(int id)
    {
      return _context.Posts.Include(n => n.EquipmentOwned)
        .FirstOrDefault(n => n.Id == id);
    }

    public Post GetNinjaById(int id)
    {
      return _context.Posts.Find(id);
    }

    public List<Post> GetNinjas()
    {
      return _context.Posts.ToList();
    }

    public IEnumerable GetClanList()
    {
        return null;
      //return _context.Topics.OrderBy(c => c.ClanName).Select(c => new {c.Id, c.ClanName}).ToList();
    }

    public ObservableCollection<Post> NinjasInMemory()
    {
      if (_context.Posts.Local.Count == 0)
      {
        GetNinjas();
      }
      return _context.Posts.Local;
    }

    public void Save()
    {
      RemoveEmptyNewNinjas();
      _context.SaveChanges();
    }

    public Post NewNinja()
    {
      var post = new Post();
      _context.Posts.Add(post);
      return post;
    }

    private void RemoveEmptyNewNinjas()
    {
      //you can't remove from or add to a collection in a foreach loop
      for (var i = _context.Posts.Local.Count; i > 0; i--)
      {
        var post = _context.Posts.Local[i - 1];
        if (_context.Entry(post).State == EntityState.Added
            && !post.IsDirty)
        {
          _context.Posts.Remove(post);
        }
      }
    }

    public void DeleteCurrentNinja(Post post)
    {
      _context.Posts.Remove(post);
      Save();
    }

    public void DeleteEquipment(ICollection equipmentList)
    {
      foreach (NinjaEquipment equip in equipmentList)
      {
        _context.Equipment.Remove(equip);
      }
    }

#if false
  /// <summary>
  /// Quick way to initialize and seed the database on first use.
  /// </summary>
    public ConnectedRepository() {
      DataHelpers.NewDbWithSeed();
    }
#endif
  }
}