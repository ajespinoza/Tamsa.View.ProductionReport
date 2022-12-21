using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tamsa.View.ProductionReport.Model.Types
{
    public class BundleLocal
    {
        public ProductKey ProductKey;
        public string LotID;
        public string Location;
        public ElabStatus ElabStatus;
        public Parameters Parameters;
        public Machine Machine;
        public Items Items;
        public int PiecesCount;
    }

    public class ProductKey
    {
        public string Order;
        public string Heat;
    }
    public class ElabStatus
    {
        public string Status;
        public string SubStatus;
    }
    public class Parameters
    {
        public string HandlingType;
        public string ReportType;
        public string Comments;
        public string GenerateDetail;
        public string ReNumberPipes;
        public string Printer;
    }
    public class Machine
    {
        public string MachineId;
    }

    public class Items
    {
        public List<Pipe> Pipes;
    }

    public class Pipe
    {
        public string Name;
        public string LotID;
        public string ManufacturingNumber;
        public List<ProcessRuns> ProcessRuns;
    }

    public class ProcessRuns
    {
        public string Name;
        public Process Process;
    }

    public class Process
    {
        public string Name;
        public DateTime TimeStamp;
        public List<ProcessParameters> ProcessParameters;
    }

    public class ProcessParameters
    {
        public string Name;
        public string Value;
    }
}
