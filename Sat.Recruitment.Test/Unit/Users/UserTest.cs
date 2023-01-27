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
        
        [Fact]
        public void Gift_Should_Not_Be_Given_To_Normal_User_When_Initial_Money_Not_Greather_Than_10USD()
        {
            var user = UserMother.Normal(10);
            user.Money.Should().Be(10);
        }
        
        [Fact]
        public void Gift_Should_Be_Given_To_Normal_User_When_Initial_Money_Above_100USD()
        {
            var user = UserMother.Normal(101);
            user.Money.Should().Be(113.12m);
        }
        
        [Fact]
        public void Gift_Should_Be_Given_To_Normal_User_When_Initial_Money_Above_10USD_But_Less_Than_101USD()
        {
            var user = UserMother.Normal(11);
            user.Money.Should().Be(19.8m);
        }
        
        [Fact]
        public void Gift_Should_Be_Given_To_Super_User_When_Initial_Money_Above_100USD()
        {
            var user = UserMother.Super(101);
            user.Money.Should().Be(121.2m);
        }
        
        [Fact]
        public void Gift_Should_Not_Be_Given_To_Super_User_When_Initial_Money_Equal_Or_Below_100USD()
        {
            var user = UserMother.Super(100);
            user.Money.Should().Be(100);
            
            user = UserMother.Super(99);
            user.Money.Should().Be(99);
        }
        
        [Fact]
        public void Gift_Should_Be_Given_To_Premium_User_When_Initial_Money_Above_100USD()
        {
            var user = UserMother.Premium(101);
            user.Money.Should().Be(303);
        }
        
        [Fact]
        public void Gift_Should_Not_Be_Given_To_Premium_User_When_Initial_Money_Equal_Or_Below_100USD()
        {
            var user = UserMother.Premium(100);
            user.Money.Should().Be(100);
            
            user = UserMother.Premium(99);
            user.Money.Should().Be(99);
        }
    }
}