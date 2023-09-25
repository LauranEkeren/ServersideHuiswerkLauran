using Domain.Models;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using PartyInvitesExtEF.Controllers;
using UI.ViewModels;

namespace PartyInvitesExtEF.Tests.Controllers;
public class HomeControllerTests
{
    [Fact]
    public void Can_Use_Repository()
    {
        // Arrange
        Mock<IGuestResponseRepository> mock = new();
        mock.Setup(m => m.Responses).Returns(GetGuestResponses());

        var sut = new HomeController(mock.Object);

        // Act
        IEnumerable<GuestResponse>? result = ((sut.ListResponses() as ViewResult)?
            .ViewData.Model as ListResponsesViewModel)?
            .GuestResponses;

        // Assert
        Assert.NotNull(result);
        Assert.NotEmpty(result);
    }

    [Fact]
    public void Can_Paginate()
    {
        // Arrange
        Mock<IGuestResponseRepository> mock = new();
        mock.Setup(m => m.Responses).Returns(GetGuestResponses());

        var sut = new HomeController(mock.Object);
        sut.PageSize = 3;

        // Act
        var actualResponses = ((sut.ListResponses(2) as ViewResult)?
            .ViewData.Model as ListResponsesViewModel)?
            .GuestResponses;

        // Assert
        Assert.True(actualResponses?.Count() == 1);
        Assert.Equal("e", actualResponses?.First().Name);
    }

    
    public GuestResponse[] GetGuestResponses()
    {
        return new GuestResponse[]
            {
                new GuestResponse { Id = 1, Name = "a", Email = "a@a.nl", Phone = "1234", WillAttent = true},
                new GuestResponse { Id = 2, Name = "b", Email = "b@b.nl", Phone = "1234", WillAttent = true},
                new GuestResponse { Id = 3, Name = "c", Email = "c@c.nl", Phone = "1234", WillAttent = true },
                new GuestResponse { Id = 4, Name = "d", Email = "d@d.nl", Phone = "1234", WillAttent = false },
                new GuestResponse { Id = 5, Name = "e", Email = "e@e.nl", Phone = "1234", WillAttent = true },
            };
    }
}
