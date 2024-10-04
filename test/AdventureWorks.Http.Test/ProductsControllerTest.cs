using AdventureWorks.Common.Domain.Products;
using AdventureWorks.Common.Exceptions;
using AdventureWorks.Http.Controllers;
using AdventureWorks.Http.Responses.Products.v1;
using AdventureWorks.Service.Production;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Moq;
using System.Net;
using Tynamix.ObjectFiller;

namespace AdventureWorks.Http.Test
{
    [TestClass]
    public class ProductsControllerTest
    {
        private ProductsController _target;
        private Mock<IProductsService> _productsServiceMock;
        private Filler<ProductDto> _productResultFiller;

        [TestInitialize]
        public void Init() 
        {
            _productsServiceMock = new Mock<IProductsService>();
            _target = new ProductsController(_productsServiceMock.Object);
            _productResultFiller = new Filler<ProductDto>();
        }

        [TestCleanup]
        public void Cleanup() 
        {
            _productsServiceMock.VerifyNoOtherCalls();

            _productsServiceMock = null;
            _target = null;
            _productResultFiller = null;
        }

        [TestMethod]
        public async Task GetProduct_ExistingProduct_MatchingId_OkResponseAndProductReturned()
        {
            var productResult = _productResultFiller.Create();
            productResult.ProductId = 99;

            var expectedProductResponseModel = MapProductResponseModel(productResult);

            _productsServiceMock.Setup(x => x.GetProductByIdAsync(99))
                .ReturnsAsync(productResult);

            var actual = await _target.GetProduct(99) as OkObjectResult;
            Assert.IsNotNull(actual);
            Assert.AreEqual(200, actual.StatusCode);

            var responseContent = actual.Value as ProductResponseModel;
            Assert.IsNotNull(responseContent);

            VerifyProductResponseModel(expectedProductResponseModel, responseContent);

            _productsServiceMock.Verify(x => x.GetProductByIdAsync(99), Times.Once);
        }

        [TestMethod]
        public async Task GetProducts_NoExistingProducts_OkResponseAndEmptyListReturned()
        {
            _productsServiceMock.Setup(x => x.GetAllProductsAsync())
                .ReturnsAsync(new List<ProductDto>());

            var actual = await _target.GetProducts() as OkObjectResult;
            Assert.IsNotNull(actual);
            Assert.AreEqual(200, actual.StatusCode);
            
            var responseContent = actual.Value as List<ProductResponseModel>;
            Assert.IsNotNull(responseContent);
            Assert.AreEqual(0, responseContent.Count);

            _productsServiceMock.Verify(x => x.GetAllProductsAsync(), Times.Once);
        }

        [TestMethod]
        public async Task GetProducts_ThreeExistingProducts_OkResponseAndListWithThreeProductsReturnedOkResponse()
        {
            var productResults = new List<ProductDto> 
            {
                _productResultFiller.Create(),
                _productResultFiller.Create(),
                _productResultFiller.Create()
            };

            var expectedProductResponseModels = new List<ProductResponseModel> 
            {
                MapProductResponseModel(productResults[0]),
                MapProductResponseModel(productResults[1]),
                MapProductResponseModel(productResults[2]),
            };

            _productsServiceMock.Setup(x => x.GetAllProductsAsync())
                .ReturnsAsync(productResults);

            var actual = await _target.GetProducts() as OkObjectResult;
            Assert.IsNotNull(actual);
            Assert.AreEqual(200, actual.StatusCode);

            var responseContent = actual.Value as List<ProductResponseModel>;
            Assert.IsNotNull(responseContent);
            Assert.AreEqual(3, responseContent.Count);

            VerifyProductResponseModels(expectedProductResponseModels, responseContent);
            _productsServiceMock.Verify(X => X.GetAllProductsAsync(), Times.Once);
        }

        private ProductResponseModel MapProductResponseModel(ProductDto productResult)
        {
            var productResponseModel = new ProductResponseModel
            {
                SafetyStockLevel = productResult.SafetyStockLevel,
                ListPrice = productResult.ListPrice,
                MakeFlag = productResult.MakeFlag,
                Class = productResult.Class,
                Color = productResult.Color,
                ProductLine = productResult.ProductLine,
                ModifiedDate = productResult.ModifiedDate,
                SellEndDate = productResult.SellEndDate,
                DaysToManufacture = productResult.DaysToManufacture,
                Name = productResult.Name,
                DiscontinuedDate = productResult.DiscontinuedDate,
                FinishedGoodsFlag = productResult.FinishedGoodsFlag,
                ProductModelId = productResult.ProductModelId,
                SizeUnitMeasureCode = productResult.SizeUnitMeasureCode,
                WeightUnitMeasureCode = productResult.WeightUnitMeasureCode,
                ProductId = productResult.ProductId,
                ProductNumber = productResult.ProductNumber,
                ProductSubcategoryId = productResult.ProductSubcategoryId,
                SellStartDate = productResult.SellStartDate,
                Size = productResult.Size,
                ReorderPoint = productResult.ReorderPoint,
                Rowguid = productResult.Rowguid,
                StandardCost = productResult.StandardCost,
                Style = productResult.Style,
                Weight = productResult.Weight
            };

            return productResponseModel;
        }

        private void VerifyProductResponseModels(List<ProductResponseModel> expected, List<ProductResponseModel> actual)
        {
            Assert.IsNotNull(expected);
            Assert.IsNotNull(actual);
            Assert.AreEqual(expected.Count, actual.Count);

            for (int i = 0; i < expected.Count; i++) 
            {
                VerifyProductResponseModel(expected[i], actual[i]);
            }
        }

        private void VerifyProductResponseModel(ProductResponseModel expected, ProductResponseModel actual)
        {
            Assert.IsNotNull(expected);
            Assert.IsNotNull(actual);
            Assert.AreEqual(expected.Style, actual.Style);
            Assert.AreEqual(expected.Size, actual.Size);
            Assert.AreEqual(expected.Name, actual.Name);
            Assert.AreEqual(expected.ListPrice, actual.ListPrice);
            Assert.AreEqual(expected.SizeUnitMeasureCode, actual.SizeUnitMeasureCode);
            Assert.AreEqual(expected.Class, actual.Class);
            Assert.AreEqual(expected.SellStartDate, actual.SellStartDate);
            Assert.AreEqual(expected.Weight, actual.Weight);
            Assert.AreEqual(expected.Color, actual.Color);
            Assert.AreEqual(expected.DaysToManufacture, actual.DaysToManufacture);
            Assert.AreEqual(expected.FinishedGoodsFlag, actual.FinishedGoodsFlag);
            Assert.AreEqual(expected.MakeFlag, actual.MakeFlag);
            Assert.AreEqual(expected.WeightUnitMeasureCode, actual.WeightUnitMeasureCode);
            Assert.AreEqual(expected.StandardCost, actual.StandardCost);
            Assert.AreEqual(expected.SellEndDate, actual.SellEndDate);
            Assert.AreEqual(expected.SellStartDate, actual.SellStartDate);
            Assert.AreEqual(expected.SafetyStockLevel, actual.SafetyStockLevel);
            Assert.AreEqual(expected.DiscontinuedDate, actual.DiscontinuedDate);
            Assert.AreEqual(expected.ModifiedDate, actual.ModifiedDate);
            Assert.AreEqual(expected.ProductLine, actual.ProductLine);
            Assert.AreEqual(expected.ProductId, actual.ProductId);
            Assert.AreEqual(expected.ProductModelId, actual.ProductModelId);
            Assert.AreEqual(expected.ProductNumber, actual.ProductNumber);
            Assert.AreEqual(expected.ProductSubcategoryId, actual.ProductSubcategoryId);
            Assert.AreEqual(expected.ReorderPoint, actual.ReorderPoint);
            Assert.AreEqual(expected.Rowguid, actual.Rowguid);            
        }
    }
}