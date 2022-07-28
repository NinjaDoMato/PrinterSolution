using System.Collections.Generic;

namespace PrinterSolution.Tests.Faker
{
    public interface IEntityFaker<T> where T : class
    {
        IList<T> Generate(int count);
        T Generate();
    }
}
