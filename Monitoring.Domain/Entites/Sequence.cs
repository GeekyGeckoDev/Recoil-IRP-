using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Monitoring.Domain.Entites
{
    public class Sequence
    {
        [Key]
        public Guid Id { get; set; }

        public int Value { get; set; }
    
    }
}
