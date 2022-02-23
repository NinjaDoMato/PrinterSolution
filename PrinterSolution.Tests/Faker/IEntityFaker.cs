using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bogus;

namespace PrinterSolution.Tests.Faker
{
    public interface IEntityFaker<T> where T : class
    {
        IList<T> Generate(int count);
        T Generate();
    }
}
