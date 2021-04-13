using System;
using System.Collections.Generic;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Virta.Api.Controllers;
using Virta.Api.DTO;
using Xunit;

namespace Virta.Tests
{
    public class ProductsControllerTest : IntegrationTest
    {
        [Fact]
        public async void GetProducts_ValidRequest()
        {
            var response = await _client.GetAsync("api/products");

            response.Should();
        }
    }
}
