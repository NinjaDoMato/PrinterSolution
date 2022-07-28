using AutoMapper;
using PrinterSolution.Common.DTOs.Requests;
using PrinterSolution.Repository.Interfaces;

namespace PrinterSolution.Service.Services
{
    public class PrinterService : IPrinterService
    {
        private readonly IRepository<Printer> repository;
        private readonly IMapper mapper;
        public PrinterService(IRepository<Printer> repository, IMapper mapper)
        {
            this.mapper = mapper;
            this.repository = repository;
        }

        public Printer CreatePrinter(CreatePrinterModel model)
        {
            var printer = mapper.Map<Printer>(model);

            repository.Insert(printer);

            return printer;
        }

        public bool DeletePrinter(long id)
        {
            var printer = repository.FirstOrDefault(p => p.Id == id);
            if (printer == null)
            {
                throw new KeyNotFoundException("Printer not found.");
            }

            repository.Delete(printer);

            return true;
        }

        public Printer GetPrinterById(long id)
        {
            var printer = repository.Single(p => p.Id == id);

            if (printer == null)
            {
                throw new KeyNotFoundException("Printer not found.");
            }

            return printer;
        }

        public List<Printer> GetPrinters()
        {
            return repository.Where(p => true).ToList();
        }

        public async Task<Printer> GetPrinterStatusById(long id)
        {
            var printer = repository.Single(p => p.Id == id);

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
            //var validator = new PrinterValidator();
            //validator.ValidateAndHandle(printer);

            if (repository.Where(p => p.Id != printer.Id && p.Name.Equals(printer.Name)).Any())
            {
                throw new ArgumentException("This name is already used.");
            }

            if (repository.Where(p => p.Id != printer.Id && p.Address.Equals(printer.Address)).Any())
            {
                throw new ArgumentException("This name is already used.");
            }

            repository.Update(printer);

            return printer;
        }
    }
}
