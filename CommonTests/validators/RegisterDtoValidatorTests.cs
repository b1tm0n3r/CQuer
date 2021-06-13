using Common.DTOs;
using Common.Validators;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CommonTests.validators
{
    [TestClass]
    public class RegisterDtoValidatorTests
    {
        [TestMethod]
        public void RETURN_TRUE_IF_DATA_IS_VALID() 
        {
            RegisterDtoValidator objectUnderTest = new RegisterDtoValidator();

            var testData = new RegisterDto()
            {
                AccountType = Common.DataModels.IdentityManagement.AccountType.Administrator,
                Password = "Test123Test!",
                RepeatedPassword = "Test123Test!",
                Username = "test"
            };

            var result = objectUnderTest.Validate(testData);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.IsValid);
        }
        [TestMethod]
        public void RETURN_FALSE_IF_PASSWORDS_NOT_MATCH()
        {
            RegisterDtoValidator objectUnderTest = new RegisterDtoValidator();

            var testData = new RegisterDto()
            {
                AccountType = Common.DataModels.IdentityManagement.AccountType.Administrator,
                Password = "Test123Test!",
                RepeatedPassword = "InvalidTestPassword",
                Username = "test"
            };

            var result = objectUnderTest.Validate(testData);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsValid);
        }
        [TestMethod]
        public void RETURN_FALSE_IF_USERNAME_CONTAINS_DOUBLE_QUOTES()
        {
            RegisterDtoValidator objectUnderTest = new RegisterDtoValidator();

            var testData = new RegisterDto()
            {
                Password = "Test123Test!",
                RepeatedPassword = "Test123Test!",
                Username = "\"test\""
            };

            var result = objectUnderTest.Validate(testData);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsValid);
        }
        [TestMethod]
        public void RETURN_FALSE_IF_USERNAME_CONTAINS_SPECIAL_CHARACTERS()
        {
            RegisterDtoValidator objectUnderTest = new RegisterDtoValidator();

            var testData = new RegisterDto()
            {
                Password = "Test123Test!",
                RepeatedPassword = "Test123Test!",
                Username = "<test>test;$</test>"
            };

            var result = objectUnderTest.Validate(testData);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsValid);
        }
    }
}
