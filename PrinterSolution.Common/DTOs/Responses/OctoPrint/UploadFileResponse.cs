using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrinterSolution.Common.DTOs.Responses.OctoPrint
{
    public class UploadFileResponse
    {
        public Files Files { get; set; }
        public bool Done { get; set; }
    }

    public class Files
    {
        public Local Local { get; set; }
        public Sdcard Sdcard { get; set; }
    }

    public class Local
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public string Type { get; set; }
        public List<string> TypePath { get; set; }
        public string Origin { get; set; }
        public Refs Refs { get; set; }
    }

    public class Refs
    {
        public string Resource { get; set; }
        public string Download { get; set; }
    }

    public class Sdcard
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public string Origin { get; set; }
        public Refs Refs { get; set; }
    }
}
