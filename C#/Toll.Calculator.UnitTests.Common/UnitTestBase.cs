using AutoFixture;
using AutoFixture.AutoNSubstitute;

namespace Toll.Calculator.UnitTests.Common
{
    public abstract class UnitTestBase<T>
    {
        private Fixture _fixture;

        protected T SUT => Fixture.Create<T>();

        protected Fixture Fixture => _fixture ?? (_fixture = ConfigureFixture());

        private Fixture ConfigureFixture()
        {
            var fixture = new Fixture();

            AddCustomizations(fixture);

            fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            return fixture;
        }

        protected virtual void AddCustomizations(Fixture fixture)
        {
            fixture.Customize(new AutoNSubstituteCustomization());
        }
    }
}