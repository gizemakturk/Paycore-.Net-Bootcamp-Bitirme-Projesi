using Base.Response;
using Data.Model;
using NUnit.Framework;

namespace UnitTest
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            BaseResponse<Offer> response = new BaseResponse<Offer>("Hata");
            Assert.That(response.Message, Is.EqualTo("Hata"));
        }

        [Test]
        public void Test2()
        {
            BaseResponse<Offer> response = new BaseResponse<Offer>("Hata");
            Assert.That(response.Success, Is.EqualTo(false));
        }
    }

   
}