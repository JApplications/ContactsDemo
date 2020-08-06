using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ContactsDemo.Services.Model
{
    public class BaseModel
    {
        [Column(Order = 0)]
        public Guid Id { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public Status Status { get; set; }
    }

    public enum Status
    {
        Undefined = 0,
        Active = 1,
        Deleted = 2
    }
}
