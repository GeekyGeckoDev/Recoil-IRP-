using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Evidence
    {
        public Guid Id { get; set; }

        public string DisplayName { get; set; }

        public string FileName { get; set; }

        public string Description { get; set; }

        public EvidenceType Type { get; set; }

        public string FilePath { get; set; }

        public string Hash { get; set; }

        public string HashAlgorithm { get; set; } = "SHA256";

        public long FileSize { get; set; }

        public DateTime CollectedAt { get; set; }

        public Guid CollectedBy { get; set; }
        public string CollectionMethod { get; set; }

        public string Source { get; set; }

        public string ChainOfCustody { get; set; }

        public bool IsPreserved { get; set; }

        public bool IsVerified { get; set; }

        public Dictionary<string, string> Metadata { get; set; } = [];
    }
}
