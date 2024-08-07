using System.Linq;
using System.Threading.Tasks;
using Microsoft.Playwright;
using FluentAssertions;
using TechTalk.SpecFlow;
using CsharpPlaywrith.Pages;
using static System.Net.Mime.MediaTypeNames;
using InventoryTests.Pages;

namespace CsharpPlaywrith.Steps
{
    [Binding]
    
    public class ProductsStepDefinitions
    {
        private ProductPage _productPage;
        public  IPage _page;

        public ProductsStepDefinitions(IPage page)
        {

            _page = page;
            _productPage = new ProductPage(_page);

        }
        [Then(@"the user should see exactly (.*) inventory items")]
        public async Task ThenTheUserShouldSeeExactlyInventoryItems(int p0)
        {
            var productElements = await _productPage.GetProductElementsAsync();
            productElements.Count.Should().Be(p0);
        }

        [Then("all product images should be different")]
        public async Task ThenAllProductImagesShouldBeDifferent()
        {
            var imageUrls = await _productPage.GetProductImageUrlsAsync();
            imageUrls.Should().OnlyHaveUniqueItems(because: "each product should have a unique image");
        }
    }
}
