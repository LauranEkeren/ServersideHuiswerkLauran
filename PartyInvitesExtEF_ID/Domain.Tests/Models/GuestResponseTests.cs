using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Tests.Models;
public class GuestResponseTests
{
    [Fact]
    public void Properties_WhenSetThenPropertiesCanBeGet()
    {
        //Arrange
        const int expectedId = 12;
        const string expectedName = "EenNaam Met Spatie";
        const string expectedEmail = "bla@da.nl";
        const string expectedEmailValidation = "bla@da.nl";
        const string expectedPhone = "0123456";
        const bool expectedWillAttend = true;

        //Act 
        GuestResponse subject = new()
        {
            Id = expectedId,
            Name = expectedName,
            Email = expectedEmail,
            EmailValidation = expectedEmailValidation,
            Phone = expectedPhone,
            WillAttent = expectedWillAttend,
        };

        //Assert
        
        Assert.Equal(expectedId, subject.Id);
        Assert.Equal(expectedName, subject.Name);
        Assert.Equal(expectedEmail, subject.Email);
        Assert.Equal(expectedEmailValidation, subject.EmailValidation);
        Assert.Equal(expectedPhone, subject.Phone);
        Assert.Equal(expectedWillAttend, subject.WillAttent);

    }


}
