using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTest_API.Controllers;
using UnitTest_API.Models;
using UnitTest_API.Services.Abstract;

namespace web_api_tests
{
    public class ShoppingCartControllerTest
    {
        private readonly ShoppingCartController _controller;
        private readonly IShoppingCartService _service;
        public ShoppingCartControllerTest()
        {
            _service = new ShoppingCartServiceFake();
            _controller = new ShoppingCartController(_service);
        }

        #region Get_WhenCalled_ReturnsOkResult
        [Fact]
        public void Get_WhenCalled_ReturnsOkResult()
        {
            // Act
            var okResult = _controller.Get();
            // Assert
            Assert.IsType<OkObjectResult>(okResult as OkObjectResult);
        }
        #endregion
        #region Get_WhenCalled_ReturnsAllItems
        [Fact]
        public void Get_WhenCalled_ReturnsAllItems()
        {
            // Act
            var okResult = _controller.Get() as OkObjectResult;
            // Assert
            var items = Assert.IsType<List<ShoppingItem>>(okResult.Value);
            Assert.Equal(3, items.Count);
        }
        #endregion
        #region GetById_UnknownGuidPassed_ReturnsNotFoundResult
        [Fact]
        public void GetById_UnknownGuidPassed_ReturnsNotFoundResult()
        {
            // Act
            var notFoundResult = _controller.Get(Guid.NewGuid());
            // Assert
            Assert.IsType<NotFoundResult>(notFoundResult);
        }
        #endregion
        #region GetById_ExistingGuidPassed_ReturnsOkResult
        [Fact]
        public void GetById_ExistingGuidPassed_ReturnsOkResult()
        {
            // Arrange
            var testGuid = new Guid("ab2bd817-98cd-4cf3-a80a-53ea0cd9c200");
            // Act
            var okResult = _controller.Get(testGuid);
            // Assert
            Assert.IsType<OkObjectResult>(okResult as OkObjectResult);
        }
        #endregion
        #region GetById_ExistingGuidPassed_ReturnsRightItem
        [Fact]
        public void GetById_ExistingGuidPassed_ReturnsRightItem()
        {
            // Arrange
            var testGuid = new Guid("ab2bd817-98cd-4cf3-a80a-53ea0cd9c200");
            // Act
            var okResult = _controller.Get(testGuid) as OkObjectResult;
            // Assert
            Assert.IsType<ShoppingItem>(okResult?.Value);
            Assert.Equal(testGuid, (okResult?.Value as ShoppingItem).Id);
        }
        #endregion
        #region Add_InvalidObjectPassed_ReturnsBadRequest
        [Fact]
        public void Add_InvalidObjectPassed_ReturnsBadRequest()
        {
            // Arrange
            var nameMissingItem = new ShoppingItem()
            {
                Manufacturer = "Guinness",
                Price = 12.00M
            };
            _controller.ModelState.AddModelError("Name", "Required");
            // Act
            var badResponse = _controller.Post(nameMissingItem);
            // Assert
            Assert.IsType<BadRequestObjectResult>(badResponse);
        }
        #endregion
        #region Add_ValidObjectPassed_ReturnsCreatedResponse
        [Fact]
        public void Add_ValidObjectPassed_ReturnsCreatedResponse()
        {
            // Arrange
            ShoppingItem testItem = new ShoppingItem()
            {
                Name = "Guinness Original 6 Pack",
                Manufacturer = "Guinness",
                Price = 12.00M
            };
            // Act
            var createdResponse = _controller.Post(testItem);
            // Assert
            Assert.IsType<CreatedAtActionResult>(createdResponse);
        }
        #endregion
        #region Add_ValidObjectPassed_ReturnedResponseHasCreatedItem
        [Fact]
        public void Add_ValidObjectPassed_ReturnedResponseHasCreatedItem()
        {
            // Arrange
            var testItem = new ShoppingItem()
            {
                Name = "Guinness Original 6 Pack",
                Manufacturer = "Guinness",
                Price = 12.00M
            };
            // Act
            var createdResponse = _controller.Post(testItem) as CreatedAtActionResult;
            var item = createdResponse.Value as ShoppingItem;
            // Assert
            Assert.IsType<ShoppingItem>(item);
            Assert.Equal("Guinness Original 6 Pack", item.Name);
        }
        #endregion
        #region Remove_NotExistingGuidPassed_ReturnsNotFoundResponse
        [Fact]
        public void Remove_NotExistingGuidPassed_ReturnsNotFoundResponse()
        {
            // Arrange
            var notExistingGuid = Guid.NewGuid();
            // Act
            var badResponse = _controller.Remove(notExistingGuid);
            // Assert
            Assert.IsType<NotFoundResult>(badResponse);
        }
        #endregion
        #region Remove_ExistingGuidPassed_ReturnsNoContentResult
        [Fact]
        public void Remove_ExistingGuidPassed_ReturnsNoContentResult()
        {
            // Arrange
            var existingGuid = new Guid("ab2bd817-98cd-4cf3-a80a-53ea0cd9c200");
            // Act
            var noContentResponse = _controller.Remove(existingGuid);
            // Assert
            Assert.IsType<NoContentResult>(noContentResponse);
        }
        #endregion
        #region Remove_ExistingGuidPassed_RemovesOneItem
        [Fact]
        public void Remove_ExistingGuidPassed_RemovesOneItem()
        {
            // Arrange
            var existingGuid = new Guid("ab2bd817-98cd-4cf3-a80a-53ea0cd9c200");
            // Act
            var okResponse = _controller.Remove(existingGuid);
            // Assert
            Assert.Equal(2, _service.GetAllItems().Count());
        }
        #endregion

    }
}
