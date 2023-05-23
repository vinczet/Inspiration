using Moq;
using Inspiration.Infrastructure.Authentication;
using Inspiration.Repository.Interfaces;
using Inspiration.Repository.DataAccessObjects;
using Inspiration.Domain.Services;
using Shouldly;
using Inspiration.Domain;
using Inspiration.Contract.Authentication;

namespace Insipration.Domain.Tests
{
    [TestClass]
    public class AuthenticationServiceTests
    {
        [TestMethod]
        public void Register_DuplicateEmail_Error()
        {
            var name = "AnyName";
            var email = "AnyEmail";
            var password = "AnyPassword";

            Mock<IJwtTokenGenerator>  mockJwtTokenGenerator = new Mock<IJwtTokenGenerator>();
            Mock<IUserRepository>  mockUserRepository = new Mock<IUserRepository>();
            mockUserRepository.Setup(x => x.GetUserByEmail(It.IsAny<string>())).Returns(new UserDao(Guid.NewGuid(), "","","",true));
            var service = new AuthenticationService(mockJwtTokenGenerator.Object, mockUserRepository.Object);

            var result = service.Register(name, email, password);            

            result.IsError.ShouldBeTrue();
            result.Errors.ShouldNotBeNull();
            result.Errors.Count.ShouldBe(1);
            result.FirstError.ShouldBe(Errors.User.DuplicateEmail);
        }

        [TestMethod]
        public void Register_Name_Empty_Error()
        {
            var name = ""; //This is the error
            var email = "AnyEmail";
            var password = "AnyPassword";

            Mock<IJwtTokenGenerator> mockJwtTokenGenerator = new Mock<IJwtTokenGenerator>();
            Mock<IUserRepository> mockUserRepository = new Mock<IUserRepository>();
            mockUserRepository.Setup(x => x.GetUserByEmail(It.IsAny<string>())).Returns((UserDao?)null);
            var service = new AuthenticationService(mockJwtTokenGenerator.Object, mockUserRepository.Object);

            var result = service.Register(name, email, password);

            result.IsError.ShouldBeTrue();
            result.Errors.ShouldNotBeNull();
            result.Errors.Count.ShouldBe(1);
            result.FirstError.ShouldBe(Errors.User.EmptyName);
        }

        [TestMethod]
        public void Register_Email_Empty_Error()
        {
            var name = "AnyName"; 
            var email = ""; //This is the error
            var password = "AnyPassword";

            Mock<IJwtTokenGenerator> mockJwtTokenGenerator = new Mock<IJwtTokenGenerator>();
            Mock<IUserRepository> mockUserRepository = new Mock<IUserRepository>();
            mockUserRepository.Setup(x => x.GetUserByEmail(It.IsAny<string>())).Returns((UserDao?)null);
            var service = new AuthenticationService(mockJwtTokenGenerator.Object, mockUserRepository.Object);

            var result = service.Register(name, email, password);

            result.IsError.ShouldBeTrue();
            result.Errors.ShouldNotBeNull();
            result.Errors.Count.ShouldBe(1);
            result.FirstError.ShouldBe(Errors.User.EmptyEmail);
        }

        [TestMethod]
        public void Register_Password_Empty_Error()
        {
            var name = "AnyName";
            var email = "AnyEmail"; 
            var password = ""; //This is the error

            Mock<IJwtTokenGenerator> mockJwtTokenGenerator = new Mock<IJwtTokenGenerator>();
            Mock<IUserRepository> mockUserRepository = new Mock<IUserRepository>();
            mockUserRepository.Setup(x => x.GetUserByEmail(It.IsAny<string>())).Returns((UserDao?)null);
            var service = new AuthenticationService(mockJwtTokenGenerator.Object, mockUserRepository.Object);

            var result = service.Register(name, email, password);

            result.IsError.ShouldBeTrue();
            result.Errors.ShouldNotBeNull();
            result.Errors.Count.ShouldBe(1);
            result.FirstError.ShouldBe(Errors.User.EmptyPassword);
        }

        [TestMethod]
        public void Register_Success()
        {
            var token = "token";
            var name = "AnyName";
            var email = "AnyEmail";
            var password = "AnyPassword";

            Mock<IJwtTokenGenerator> mockJwtTokenGenerator = new Mock<IJwtTokenGenerator>();
            Mock<IUserRepository> mockUserRepository = new Mock<IUserRepository>();
            mockUserRepository.Setup(x => x.GetUserByEmail(It.IsAny<string>())).Returns((UserDao?)null);
            mockJwtTokenGenerator.Setup(x=>x.GenerateToken(It.IsAny<Guid>(), name)).Returns(token);                       

            var service = new AuthenticationService(mockJwtTokenGenerator.Object, mockUserRepository.Object);

            var result = service.Register(name, email, password);            

            result.IsError.ShouldBeFalse();            
            result.Value.ShouldNotBeNull();
            result.Value.ShouldBeOfType<AuthenticationResult>();
            result.Value.Token.ShouldBe(token);
            result.Value.User.Name.ShouldBe(name);
            result.Value.User.Email.ShouldBe(email);
            result.Value.User.Password.ShouldBe(password);
            result.Value.User.IsAdmin.ShouldBeFalse();
            result.Value.User.Id.ShouldNotBe(Guid.Empty);
        }

        [TestMethod]
        public void Login_NoUserWithSuchEmail_Error()
        {            
            var email = "AnyEmail";
            var password = "AnyPassword";

            Mock<IJwtTokenGenerator> mockJwtTokenGenerator = new Mock<IJwtTokenGenerator>();
            Mock<IUserRepository> mockUserRepository = new Mock<IUserRepository>();
            mockUserRepository.Setup(x => x.GetUserByEmail(It.IsAny<string>())).Returns((UserDao?)null);
            var service = new AuthenticationService(mockJwtTokenGenerator.Object, mockUserRepository.Object);

            var result = service.Login(email, password);

            result.IsError.ShouldBeTrue();
            result.Errors.ShouldNotBeNull();
            result.Errors.Count.ShouldBe(1);
            result.FirstError.ShouldBe(Errors.Authentication.InvalidCredentials);
        }

        [TestMethod]
        public void Login_PasswordsNotMatch_Error()
        {
            var email = "AnyEmail";
            var password = "AnyPassword";
            var passwordInDb = "OtherPassword";

            Mock<IJwtTokenGenerator> mockJwtTokenGenerator = new Mock<IJwtTokenGenerator>();
            Mock<IUserRepository> mockUserRepository = new Mock<IUserRepository>();
            mockUserRepository.Setup(x => x.GetUserByEmail(It.IsAny<string>())).Returns(new UserDao(Guid.NewGuid(), "", "", passwordInDb, true));
            var service = new AuthenticationService(mockJwtTokenGenerator.Object, mockUserRepository.Object);

            var result = service.Login(email, password);

            result.IsError.ShouldBeTrue();
            result.Errors.ShouldNotBeNull();
            result.Errors.Count.ShouldBe(1);
            result.FirstError.ShouldBe(Errors.Authentication.InvalidCredentials);
        }

        [TestMethod]
        public void Login_Success()
        {
            var token = "token";
            var name = "AnyName";
            var email = "AnyEmail";
            var password = "AnyPassword";

            Mock<IJwtTokenGenerator> mockJwtTokenGenerator = new Mock<IJwtTokenGenerator>();
            Mock<IUserRepository> mockUserRepository = new Mock<IUserRepository>();
            mockUserRepository.Setup(x => x.GetUserByEmail(It.IsAny<string>())).Returns(new UserDao(Guid.NewGuid(), name, email, password, false));
            mockJwtTokenGenerator.Setup(x => x.GenerateToken(It.IsAny<Guid>(), name)).Returns(token);

            var service = new AuthenticationService(mockJwtTokenGenerator.Object, mockUserRepository.Object);

            var result = service.Login(email, password);

            result.IsError.ShouldBeFalse();
            result.Value.ShouldNotBeNull();
            result.Value.ShouldBeOfType<AuthenticationResult>();
            result.Value.Token.ShouldBe(token);
            result.Value.User.Name.ShouldBe(name);
            result.Value.User.Email.ShouldBe(email);
            result.Value.User.Password.ShouldBe(password);
            result.Value.User.IsAdmin.ShouldBeFalse();
            result.Value.User.Id.ShouldNotBe(Guid.Empty);
        }
    }
}