using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Enums
{
    public enum EvidenceType
    {
        LogFile,
        Screenshot,
        MemoryDump,
        DiskImage,
        NetworkCapture,
        MalwareSample,
        Email,
        Document,
        RegistryExport,

        ProcessList,

        NetworkConnections,
        FileSystem,
        Configuration,
        Other

    }
}
