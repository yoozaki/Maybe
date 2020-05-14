using FluentAssertions;
using Xunit;

namespace Maybe.Tests
{
    public class Basic
    {
        [Fact]
        public void BindTest()
        {
            var valueMaybe = Maybe<string>.Return("test");
            valueMaybe.Bind(x => x.Should().Be("test"));
        }

        [Fact]
        public void BindNothingTest()
        {
            var valueMaybe = Maybe<string>.Return(null);
            valueMaybe.Bind(x => Assert.True(false));
        }
    }
}
