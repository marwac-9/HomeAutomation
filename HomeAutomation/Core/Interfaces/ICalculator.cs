using System;

using Xamarin.Forms;

namespace Interfaces
{
    public interface IOperation
    {
        int Operate(int left, int right);
    }
    public class IOperationData
    {
        public Char Symbol { get; set; }
        public int test { get; set; }
    }
    public interface ICalculator
    {
        String Calculate(String input);
    }
}
