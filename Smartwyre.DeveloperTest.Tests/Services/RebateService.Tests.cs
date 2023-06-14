using Moq;
using Smartwyre.DeveloperTest.Data;
using Smartwyre.DeveloperTest.Services;
using Smartwyre.DeveloperTest.Types;
using Xunit;

namespace Smartwyre.DeveloperTest.Tests.Services;

public class RebateServiceTests
{
    [Fact]
    public void Calculate_FixedCashAmountRebate_Success()
    {
        var request = new CalculateRebateRequest
        {
            RebateIdentifier = "RebateTestIdent",
            ProductIdentifier = "ProductTestIdent"
        };
        var rebateDataStore = new Mock<IRebateDataStore>();
        var productDataStore = new Mock< IProductDataStore>();
        var stubRebate = new Rebate
        {
            Incentive = IncentiveType.FixedCashAmount,
            Amount = 1
        };
        var stubProduct = new Product
        {
            SupportedIncentives = SupportedIncentiveType.FixedCashAmount
        };

        rebateDataStore.Setup(x => x.GetRebate(request.RebateIdentifier)).Returns(stubRebate);
        productDataStore.Setup(x => x.GetProduct(request.ProductIdentifier)).Returns(stubProduct);
        var rebateService = new RebateService(rebateDataStore.Object, productDataStore.Object);

        var result = rebateService.Calculate(request);

        Assert.True(result.Success);
        rebateDataStore.Verify(x => x.StoreCalculationResult(stubRebate, stubRebate.Amount), Times.Once);
    }

    [Fact]
    public void Calculate_FixedCashAmountRebate_NotSuccess()
    {
        var request = new CalculateRebateRequest
        {
            RebateIdentifier = "RebateTestIdent",
            ProductIdentifier = "ProductTestIdent"
        };
        var rebateDataStore = new Mock<IRebateDataStore>();
        var productDataStore = new Mock<IProductDataStore>();
        var stubRebate = new Rebate
        {
            Incentive = IncentiveType.FixedCashAmount,
            Amount = 0
        };
        var stubProduct = new Product
        {
            SupportedIncentives = SupportedIncentiveType.FixedCashAmount
        };

        rebateDataStore.Setup(x => x.GetRebate(request.RebateIdentifier)).Returns(stubRebate);
        productDataStore.Setup(x => x.GetProduct(request.ProductIdentifier)).Returns(stubProduct);
        var rebateService = new RebateService(rebateDataStore.Object, productDataStore.Object);

        var result = rebateService.Calculate(request);

        Assert.False(result.Success);
        rebateDataStore.Verify(x => x.StoreCalculationResult(stubRebate, stubRebate.Amount), Times.Never);
    }

    [Fact]
    public void Calculate_AmountPerUomRebate_Success()
    {
        var request = new CalculateRebateRequest
        {
            RebateIdentifier = "RebateTestIdent",
            ProductIdentifier = "ProductTestIdent",
            Volume = 2
        };
        var rebateDataStore = new Mock<IRebateDataStore>();
        var productDataStore = new Mock<IProductDataStore>();
        var stubRebate = new Rebate
        {
            Incentive = IncentiveType.AmountPerUom,
            Amount = 3
        };
        var stubProduct = new Product
        {
            SupportedIncentives = SupportedIncentiveType.AmountPerUom
        };

        rebateDataStore.Setup(x => x.GetRebate(request.RebateIdentifier)).Returns(stubRebate);
        productDataStore.Setup(x => x.GetProduct(request.ProductIdentifier)).Returns(stubProduct);
        var rebateService = new RebateService(rebateDataStore.Object, productDataStore.Object);

        var result = rebateService.Calculate(request);

        Assert.True(result.Success);
        rebateDataStore.Verify(x => x.StoreCalculationResult(stubRebate, stubRebate.Amount * request.Volume), Times.Once);
    }

    [Fact]
    public void Calculate_AmountPerUomRebate_NotSuccess()
    {
        var request = new CalculateRebateRequest
        {
            RebateIdentifier = "RebateTestIdent",
            ProductIdentifier = "ProductTestIdent",
            Volume = 2
        };
        var rebateDataStore = new Mock<IRebateDataStore>();
        var productDataStore = new Mock<IProductDataStore>();
        var stubRebate = new Rebate
        {
            Incentive = IncentiveType.AmountPerUom,
            Amount = 0
        };
        var stubProduct = new Product
        {
            SupportedIncentives = SupportedIncentiveType.AmountPerUom
        };

        rebateDataStore.Setup(x => x.GetRebate(request.RebateIdentifier)).Returns(stubRebate);
        productDataStore.Setup(x => x.GetProduct(request.ProductIdentifier)).Returns(stubProduct);
        var rebateService = new RebateService(rebateDataStore.Object, productDataStore.Object);

        var result = rebateService.Calculate(request);

        Assert.False(result.Success);
        rebateDataStore.Verify(x => x.StoreCalculationResult(stubRebate, stubRebate.Amount * request.Volume), 
            Times.Never);
    }

    [Fact]
    public void Calculate_FixedRateRebate_Success()
    {
        var request = new CalculateRebateRequest
        {
            RebateIdentifier = "RebateTestIdent",
            ProductIdentifier = "ProductTestIdent",
            Volume = 2
        };
        var rebateDataStore = new Mock<IRebateDataStore>();
        var productDataStore = new Mock<IProductDataStore>();
        var stubRebate = new Rebate
        {
            Incentive = IncentiveType.FixedRateRebate,
            Percentage = 3
        };
        var stubProduct = new Product
        {
            SupportedIncentives = SupportedIncentiveType.FixedRateRebate,
            Price = 4
        };

        rebateDataStore.Setup(x => x.GetRebate(request.RebateIdentifier)).Returns(stubRebate);
        productDataStore.Setup(x => x.GetProduct(request.ProductIdentifier)).Returns(stubProduct);
        var rebateService = new RebateService(rebateDataStore.Object, productDataStore.Object);

        var result = rebateService.Calculate(request);

        Assert.True(result.Success);
        rebateDataStore.Verify(x => x.StoreCalculationResult(stubRebate, stubRebate.Percentage * request.Volume * stubProduct.Price), Times.Once);
    }

    [Fact]
    public void Calculate_FixedRateRebate_NotSuccess()
    {
        var request = new CalculateRebateRequest
        {
            RebateIdentifier = "RebateTestIdent",
            ProductIdentifier = "ProductTestIdent",
            Volume = 2
        };
        var rebateDataStore = new Mock<IRebateDataStore>();
        var productDataStore = new Mock<IProductDataStore>();
        var stubRebate = new Rebate
        {
            Incentive = IncentiveType.FixedRateRebate,
            Percentage = 3
        };
        var stubProduct = new Product
        {
            SupportedIncentives = SupportedIncentiveType.FixedRateRebate,
            Price = 0
        };

        rebateDataStore.Setup(x => x.GetRebate(request.RebateIdentifier)).Returns(stubRebate);
        productDataStore.Setup(x => x.GetProduct(request.ProductIdentifier)).Returns(stubProduct);
        var rebateService = new RebateService(rebateDataStore.Object, productDataStore.Object);

        var result = rebateService.Calculate(request);

        Assert.False(result.Success);
        rebateDataStore.Verify(x => x.StoreCalculationResult(stubRebate, stubRebate.Percentage * request.Volume * stubProduct.Price), Times.Never);
    }

    [Fact]
    public void Calculate_NoRebateReturned_NotSuccess()
    {
        var request = new CalculateRebateRequest
        {
            RebateIdentifier = "RebateTestIdent",
            ProductIdentifier = "ProductTestIdent"
        };
        var rebateDataStore = new Mock<IRebateDataStore>();
        var productDataStore = new Mock<IProductDataStore>();
        
        var stubProduct = new Product
        {
            SupportedIncentives = SupportedIncentiveType.FixedCashAmount
        };

        rebateDataStore.Setup(x => x.GetRebate(request.RebateIdentifier)).Returns((Rebate)null);
        productDataStore.Setup(x => x.GetProduct(request.ProductIdentifier)).Returns(stubProduct);
        var rebateService = new RebateService(rebateDataStore.Object, productDataStore.Object);

        var result = rebateService.Calculate(request);

        Assert.False(result.Success);
    }

    [Fact]
    public void Calculate_NoProductReturned_NotSuccess()
    {
        var request = new CalculateRebateRequest
        {
            RebateIdentifier = "RebateTestIdent",
            ProductIdentifier = "ProductTestIdent"
        };
        var rebateDataStore = new Mock<IRebateDataStore>();
        var productDataStore = new Mock<IProductDataStore>();

        var stubRebate = new Rebate
        {
            Incentive = IncentiveType.FixedRateRebate,
            Percentage = 3
        };

        rebateDataStore.Setup(x => x.GetRebate(request.RebateIdentifier)).Returns(stubRebate);
        productDataStore.Setup(x => x.GetProduct(request.ProductIdentifier)).Returns((Product)null);
        var rebateService = new RebateService(rebateDataStore.Object, productDataStore.Object);

        var result = rebateService.Calculate(request);

        Assert.False(result.Success);
    }
}
