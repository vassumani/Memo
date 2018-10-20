using System;
using Xunit;
using memo.Models;
namespace MemoxUnitTests
{
    public class MemoModelTest
    {
        [Fact]
        public void IsMemoTitleValid()
        {
            var sut = new MemoValidator();
            sut.Title = "Unit Testing";
            Assert.True(sut.isValidTitle());
        }

        [Fact]
        public void IsMemoTitleInInValid()
        {
            var sut = new MemoValidator();
            sut.Title = "";
            Assert.False(sut.isValidTitle());
        }


        [Fact]
        public void IsMemoDateValid()
        {
            var sut = new MemoValidator();
            sut.Date = DateTime.Now;
            Assert.True(sut.isValidDateTime());

        }

    }
}
