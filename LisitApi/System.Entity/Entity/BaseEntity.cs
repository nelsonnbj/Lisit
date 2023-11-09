using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemTheLastBugSpa.Data.Entity
{
    public class BaseEntity
    {
        public BaseEntity() 
        {
            Id = Guid.NewGuid();
            CreateAt = DateTime.Now;
            Active = true;
        }
        [Key]
        public Guid Id { get; set; }
        public bool Active { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
