namespace PrinterSolution.Service.Services
{
    public class PrinterService : IPrinterService
    {
        private readonly DatabaseContext _ctx;

        public PrinterService(DatabaseContext context)
        {
            _ctx = context;
        }

        public Printer CreatePrinter(string name, string address, PrinterType type, int height = 100, int width = 100, int depth = 100, bool heatBed = false)
        {
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

            var validator = new PrinterValidator();
            validator.ValidateAndHandle(printer);

            _ctx.Printers.Add(printer);
            _ctx.SaveChanges();

            return printer;
        }

        public bool DeletePrinter(long id)
        {
            var printer = _ctx.Printers.FirstOrDefault(p => p.Id == id);
            if (printer == null)
            {
                throw new KeyNotFoundException("Printer not found.");
            }

            _ctx.Printers.Remove(printer);
            _ctx.SaveChanges();

            return true;
        }

        public Printer GetPrinterById(long id)
        {
            var printer = _ctx.Printers.Find(id);

            if (printer == null)
            {
                throw new KeyNotFoundException("Printer not found.");
            }

            return printer;
        }

        public List<Printer> GetPrinters()
        {
            return _ctx.Printers.ToList();
        }

        public async Task<Printer> GetPrinterStatusById(long id)
        {
            var printer = _ctx.Printers.Find(id);

            if (printer == null)
            {
                throw new KeyNotFoundException("Printer not found.");
            }

            var netPrinter = new OctoPrint.API.Printer(printer.Address, "");

            var status = await netPrinter.Self.GetState();

            if (status.Code != 200)
            {
                throw new Exception();
            }

            throw new NotImplementedException();
        }

        public Printer UpdatePrinter(Printer printer)
        {
            var validator = new PrinterValidator();
            validator.ValidateAndHandle(printer);

            if (_ctx.Printers.Any(p => p.Id != printer.Id && p.Name.Equals(printer.Name)))
            {
                throw new ArgumentException("This name is already used.");
            }

            if (_ctx.Printers.Any(p => p.Id != printer.Id && p.Address.Equals(printer.Address)))
            {
                throw new ArgumentException("This name is already used.");
            }

            _ctx.Printers.Update(printer);
            _ctx.SaveChanges();

            return printer;
        }
    }
}
