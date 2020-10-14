using Moq;
using NUnit.Framework;
using RNDR.DAL;
using RNDR.Services.UserManagement;

namespace RNDR.Service.Tests.UserManagement
{
    
    public class UserServiceTest
    {
        private IUserService _userService;
        private Mock<DataContext> _dataContext;

        [SetUp]
        public void Setup()
        {
            _dataContext = new Mock<DataContext>();
            _userService = new UserService(_dataContext.Object);
        }
    }
}
