using FakeItEasy;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using toll_calculator;

namespace UnitTests
{
    [TestClass]
    public class SchemasTests
    {
        private IYearSchema fakeSchema;
        private Schemas _sut;

        [TestInitialize]
        public void Initialize()
        {
            fakeSchema = new Fake<IYearSchema>().FakedObject;
            _sut = new Schemas();
        }

        [TestMethod]

        public void GivenAYear_YearNotFoundReturnNull()
        {
            _sut.RegisterSchemaForYear(fakeSchema);
            var result = _sut.GetSchemaForYear(0 != fakeSchema.GetYear() ? 0:1 );
            result.Should().Be(null);
        }


        [TestMethod]
        public void RegisterYear_AddsYearToList()
        {
            _sut.RegisterSchemaForYear(fakeSchema);
            var result = _sut.GetSchemaForYear(fakeSchema.GetYear());
            result.Should().NotBeNull();
        }
    }

}
