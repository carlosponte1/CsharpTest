using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Playwright;

namespace InventoryTests.Pages
{
    public class ProductPage
    {
        string itemInventory = ".inventory_item";
        string imageSource = ".inventory_list .inventory_item_img";
        private readonly IPage _page;

        public ProductPage(IPage page)
        {
            _page = page;
        }

        public async Task<IReadOnlyList<IElementHandle>> GetProductElementsAsync()
        {
            await Task.Delay(2000);
            return await _page.QuerySelectorAllAsync(itemInventory);
           
        }
        public async Task<IReadOnlyList<string>> GetProductImageUrlsAsync()
        {
            var imageElements = await _page.QuerySelectorAllAsync(imageSource);
            var imageUrls = new List<string>();

            foreach (var imageElement in imageElements)
            {
                var imageUrl = await imageElement.GetAttributeAsync("src");
                if (!string.IsNullOrEmpty(imageUrl))
                {
                    imageUrls.Add(imageUrl);
                }
                
            }

            return imageUrls;
        }
    }
}
