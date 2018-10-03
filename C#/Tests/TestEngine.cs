using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TollFeeCalculator.Tests
{
    //TODO: log errors and build better error handling
    public class TestEngine
    {
        private static readonly object _gate = new object();
        private static TestEngine _instance = null;

        private List<Type> Tests { get; set; }

        private TestEngine()
        {
            this.Tests = new List<Type>();
        }

        public static TestEngine Instance
        {
            get
            {
                //Note: "dubble check"-lock for performance.
                if (_instance == null)
                {
                    lock(_gate)
                    {
                        if (_instance == null)
                            _instance = new TestEngine();
                    }
                }

                return _instance;
            }
        }

        public void InitializeAllTests()
        {
            Console.WriteLine("Initializing test run...");

            var typeOfITest = typeof(ITest);

            var typesImplementingITest = 
                AppDomain.CurrentDomain
                    .GetAssemblies()
                    .SelectMany(ass => ass.GetTypes())
                    .Where(t => t.IsClass)
                    .Where(t => typeOfITest.IsAssignableFrom(t));

            this.Tests.AddRange(typesImplementingITest);
        }

        public void ExecuteTests()
        {
            foreach (var type in this.Tests)
            {
                var classInstance = Activator.CreateInstance(type) as ITest;

                if (classInstance == null)
                    throw new ApplicationException($"Could not create and ITest instance for {type.ToString()}");

                var methodInfos = 
                    type.GetMethods()
                        .Where(m => m.GetParameters().Length <= 0)
                        .Where(m => m.GetCustomAttributes(typeof(TollUnitTest), false).Length > 0)
                        .ToList();

                foreach (var methodInfo in methodInfos)
                {
                    try 
                    {
                        methodInfo.Invoke(classInstance, new object[0]);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Could not invoke test {methodInfo.Name} "
                            + $"Exception ocurred, ex:\n {ex.ToString()}");
                    }
                }
            }

            Console.WriteLine(Assert.Instance.ToString());
        }
    }
}