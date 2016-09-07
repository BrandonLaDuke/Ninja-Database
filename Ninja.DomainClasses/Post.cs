using System;
using System.Collections.Generic;
using System.ComponentModel;
using NinjaDomain.Classes.Interfaces;

namespace NinjaDomain.Classes
{
    public class Post:IModificationHistory
    {
        public Post()
        {
            EquipmentOwned = new List<NinjaEquipment>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public bool ServedInOniwaban { get; set; }
        public Topic Topic { get; set; }
        public int TopicId { get; set; }
        public List<NinjaEquipment> EquipmentOwned { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PostText { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public bool IsDirty { get; set; }
       
    }
}