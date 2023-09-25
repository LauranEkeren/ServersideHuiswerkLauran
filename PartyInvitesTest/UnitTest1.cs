using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using NSubstitute;
using PartyInvites.Controllers;
using PartyInvites.Models;

namespace PartyInvitesTest
{
    public class UnitTest1
    {
        [Fact]
        public void ListResponses_Should_Return_Invites_Which_Attend()
        {
            var repositoryMock = Substitute.For<IRepository>();

            var sut = new HomeController(repositoryMock);

            repositoryMock.GetResponses().Returns(new List<GuestResponse>() {
                new GuestResponse {
                    Name = "Rathalos",
                    Email = "Rath@gmail.com",
                    Phone = "1234567890",
                    WillAttend = true
                },
                new GuestResponse
                {
                    Name = "Rathian",
                    Email = "Rath@gmail.com",
                    Phone = "1234567890",
                    WillAttend = false
                }
            }.AsQueryable());

            var result = sut.ListResponses();

            var responsesInModel = result.Model as IEnumerable<GuestResponse>;
            //Assert.Equal(1, responsesInModel.Count);
            Assert.Equal("Rathalos", responsesInModel.First().Name);
            Assert.Equal("ListResponses", result.ViewName);
        }
    }
}