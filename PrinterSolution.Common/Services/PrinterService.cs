using PrinterSolution.Common.Database;
using PrinterSolution.Common.Entities;
using PrinterSolution.Common.Utils.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrinterSolution.Common.Services
{
    public interface IPrinterService
    {
        public Printer GetPrinterById(int id);
        public List<Printer> GetPrinters();
        public Printer CreatePrinter(string name, string address, PrinterType type, int height = 100, int width = 100, int depth = 100, bool heatBed = false);
        public Printer UpdatePrinter(Printer printer);
        public bool DeletePrinter(int id);
    }

    public class PrinterService : IPrinterService
    {
        private readonly DatabaseContext _ctx;

        public PrinterService(DatabaseContext context)
        {
            _ctx = context;
        }

        public Printer CreatePrinter(string name, string address, PrinterType type, int height = 100, int width = 100, int depth = 100, bool heatBed = false)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException("Name cannot be empty.");

            if (string.IsNullOrEmpty(address))
                throw new ArgumentNullException("Address cannot be empty.");

            var printer = new Printer
            {
                Name = name,
                Address = address,
                Type = type,
                Height = height,
                Width = width,
                Depth = depth,
                HasHeatedBed = heatBed,
                DateCreated = DateTime.Now,
                Status = PrinterStatus.Offline
            };

            _ctx.Printers.Add(printer);
            _ctx.SaveChanges();

            return printer;
        }

        public bool DeletePrinter(int id)
        {
            throw new NotImplementedException();
        }

        public Printer GetPrinterById(int id)
        {
            throw new NotImplementedException();
        }

        public List<Printer> GetPrinters()
        {
            throw new NotImplementedException();
        }

        public Printer UpdatePrinter(Printer printer)
        {
            throw new NotImplementedException();
        }
    }
}
