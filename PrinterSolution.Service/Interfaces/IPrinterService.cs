using PrinterSolution.Common.DTOs.Requests;

namespace PrinterSolution.Service.Interfaces
{
    public interface IPrinterService
    {
        Printer GetPrinterById(long id);
        Task<Printer> GetPrinterStatusById(long id);
        List<Printer> GetPrinters();
        Printer CreatePrinter(CreatePrinterModel model);
        Printer UpdatePrinter(Printer printer);
        bool DeletePrinter(long id);
    }
}
