using System.Composition;
using Interfaces;
using System;

namespace Operations
{
    [Export(typeof(IOperation))]
    [ExportMetadata("Symbol", '+')]
    [ExportMetadata("test", 3)]
    public class Add : IOperation
    {
        public int Operate(int left, int right)
        {
            return left + right;
        }
    }

    [Export(typeof(IOperation))]
    [ExportMetadata("Symbol", '-')]
    [ExportMetadata("test", 10)]
    public class Subtract : IOperation
    {
        public int Operate(int left, int right)
        {
            return left - right;
        }
    }
}
