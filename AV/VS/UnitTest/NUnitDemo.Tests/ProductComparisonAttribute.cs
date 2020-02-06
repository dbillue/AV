using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace NUnitDemo.Tests
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class,  AllowMultiple = false)]
    public class ProductComparisonAttribute : CategoryAttribute
    {
        // *** Not Implemented *** \\
    }
}
