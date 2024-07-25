using System.Threading.Tasks;
using BiiSoft.Models.TokenAuth;
using BiiSoft.Web.Controllers;
using Shouldly;
using Xunit;

namespace BiiSoft.Web.Tests.Controllers
{
    public class HomeController_Tests: BiiSoftWebTestBase
    {
        [Fact]
        public async Task Index_Test()
        {
            await AuthenticateAsync(null, new AuthenticateModel
            {
                UserNameOrEmailAddress = "admin",
                Password = "123qwe"
            });

            //Act
            var response = await GetResponseAsStringAsync(
                GetUrl<HomeController>(nameof(HomeController.Index))
            );

            //Assert
            response.ShouldNotBeNullOrEmpty();
        }
    }
}