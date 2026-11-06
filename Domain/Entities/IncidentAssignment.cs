using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class IncidentAssignment
    {
        public Guid Id { get; set; }
        public Guid IncidentId { get; set; }
        public Guid EmployeeId { get; set; }

        public DateTime AssignedAt { get; set; }
        public Guid AssignedBy { get; set; }

        public DateTime? RemovedAt { get; set; }
        public Guid? RemovedBy { get; set; }
    }
}
