using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class PlaybookStep
    {
        public Guid Id { get; set; }
        public int Order { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }

        public PlaybookPhase Phase { get; set; }

        public bool isRequired { get; set; }

        public List<string> ChecklistItems { get; set; } = [];

        //public string AutomationScript { get; set; } = string.Empty;
    }
}
