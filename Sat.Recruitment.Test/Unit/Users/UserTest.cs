using FluentAssertions;
using Xunit;

namespace Sat.Recruitment.Test.Unit.Users
{
    public class UserTest : UnitTest
    {
        [Fact]
        public void Gift_Should_Not_Be_Given_To_Normal_User_With_100USD_Initial_Money()
        {
            var user = UserMother.Normal(100);
            user.Money.Should().Be(100);
        }
    }
}