using System;
using Firefly.CodeTests.AuthManager.Authentication;
using Firefly.CodeTests.AuthManager.Database;
using Firefly.CodeTests.AuthManager.Factory;
using Firefly.CodeTests.AuthManager.Hashing;
using Firefly.CodeTests.AuthManager.Model;
using Firefly.CodeTests.AuthManager.Validators;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Firefly.CodeTests.AuthManager.UnitTests
{
    [TestClass]
    public class AuthManagerIntegrationTest
    {
        private DatabaseService _databaseService;
        private IUserFactory _userFactory;
        private IUsernameValidator _usernameValidator;
        private IPasswordValidator _passwordValidator;
        private Mock<IActiveDirectoryAccountValidator> _activeDirectoryAccountValidator;
        private IPasswordHashService _passwordHashService;
        private IAuthService<bool> _authService;
        private IUserRepository _userRepository;
        private IUserCredentialRepository _userCredentialRepository;
        

        private const  string ValidUsername = "newUsername@testmail.com";
        private const string ValidPassword = "password";

        [TestInitialize]
        public void TestInitialize()
        {
            var connectionString = @"Data Source=(localdb)\ProjectsV13;Initial Catalog=Firefly.CodeTests.AuthManager.Database;Integrated Security=True;Pooling=False;Connect Timeout=30";
            _databaseService = new DatabaseService(connectionString);
            //implement DB cleanup
            _databaseService.ClearTestData();

            var sqlConnectionWrapper = new SqlConnectionWrapper(connectionString);
            _passwordHashService = new FakePasswordHashService();
            _passwordValidator = new PasswordValidator();
            _activeDirectoryAccountValidator = new Mock<IActiveDirectoryAccountValidator>();
            _userRepository = new UserRepository(sqlConnectionWrapper);
            _usernameValidator = new UsernameValidator(_activeDirectoryAccountValidator.Object, _userRepository);
            _userCredentialRepository = new UserCredentialRepository(sqlConnectionWrapper, _passwordHashService);
            _userFactory = new UserFactory(_passwordValidator, _usernameValidator, _userRepository, _userCredentialRepository);
            _authService = new AuthService(_userCredentialRepository,_passwordHashService);

        }

        [TestMethod]
        public void CreateNewUser_UserNameValidationFails_1()
        {
            //implement a test where, during a new User creation, the UserName custom validation fails
            //	custom validation checks for UserName: should be a valid email address
            string invalidUsername = "username";
            Exception thrownException = null;

            try
            {
                _userFactory.CreateAuthenticatedUser(invalidUsername, "password");
            }
            catch (ArgumentException exception)
            {
                thrownException = exception;
            }

            Assert.IsNotNull(thrownException);
            Assert.IsInstanceOfType(thrownException, typeof(ArgumentException));
            Assert.IsTrue(thrownException.Message.Contains("username is not a valid email address"));

        }

        [TestMethod]
        public void CreateNewUser_UserNameValidationFails_2()
        {
            //implement a test where, during a new User creation, the UserName custom validation fails
            //	custom validation checks for UserName: should be a valid active directory account (domain\username)
            string username = "user@email.com";
            Exception thrownException = null;

            _activeDirectoryAccountValidator.Setup(x => x.ValidateUser(username)).Returns(false);

            try
            {
                _userFactory.CreateAuthenticatedUser(username, "password");
            }
            catch (ArgumentException exception)
            {
                thrownException = exception;
            }

            Assert.IsNotNull(thrownException);
            Assert.IsInstanceOfType(thrownException, typeof(ArgumentException));
            Assert.IsTrue(thrownException.Message.Contains("username is not a valid active directory account"));
        }

        [TestMethod]
        public void CreateNewUser_Succeeds()
        {
            //implement a test where the creation succeed
            User user = CreateValidUser();

            Assert.IsNotNull(user);
            Assert.AreEqual(ValidUsername, user.Username);
            Assert.AreEqual(ValidPassword, user.Password);
        }

        private User CreateValidUser()
        {
            _activeDirectoryAccountValidator.Setup(x => x.ValidateUser(ValidUsername)).Returns(true);
            var user = _userFactory.CreateAuthenticatedUser(ValidUsername, ValidPassword);
            return user;
        }

        [TestMethod]
        public void CreateNewUser_UserNameAlreadyExists()
        {
            //implement a test where the creation fails because a User with the same UserName already exists
            
            Exception thrownException = null;
            CreateValidUser();

            try
            {
                CreateValidUser();
            }
            catch (ArgumentException exception)
            {
                thrownException = exception;
            }

            Assert.IsNotNull(thrownException);
            Assert.IsInstanceOfType(thrownException, typeof(ArgumentException));
            Assert.IsTrue(thrownException.Message.Contains($"user with username {ValidUsername} already exists"));

        }

        [TestMethod]
        public void GetUser_Succeeds()
        {
            //implement a test that retrieve, using the UserName, the user created with the test CreateNewUser_Succeededs
            var createdUser = CreateValidUser();

            User user = _userRepository.FindByUserName(ValidUsername);

            Assert.AreEqual(createdUser, user);
        }

        [TestMethod]
        public void GetUser_Fails()
        {
            // implement a test that doesn't retrieve a User with a speficied UserName
            
            User user = _userRepository.FindByUserName("username");

            Assert.IsNull(user);
        }

        [TestMethod]
        public void AuthenticaUserCredentialsOk_Succeeds()
        {
            //implement a test in which an account is successfully authenticated against an exsisting user previously created in CreateNewUser_Succeededs
            var user = CreateValidUser();

            var authResponse = _authService.AuthenticateUser(user.Username, user.Password);

            Assert.IsTrue(authResponse);
        }

        [TestMethod]
        public void AuthenticateUser_WrongPassword()
        {
            //implement a test in which user authentication fails bacause the password is wrong
            var user = CreateValidUser();

            var authResponse = _authService.AuthenticateUser(user.Username, "wrong password");

            Assert.IsFalse(authResponse);
        }
    }
}
