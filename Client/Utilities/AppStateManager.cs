using Microsoft.AspNetCore.Components;

namespace Blazor.Client.Utilities
{
	public class AppStateManager
	{
		public event Action<ComponentBase> OnQuantityChanged;
		public event Action<ComponentBase> OnCartUpdated;
		public event Action<ComponentBase> OnCategoriesChanged;
		public event Action<ComponentBase> OnProductsChanged;
		public event Action<ComponentBase> OnPageChanged;
		public void UpdateCartQuantity(ComponentBase componentBase)
		{
			OnQuantityChanged?.Invoke(componentBase);
		}

		public void UpdateCart(ComponentBase componentBase)
		{
			OnCartUpdated?.Invoke(componentBase);
		}

		public void UpdateCategories(ComponentBase componentBase)
		{
			OnCategoriesChanged?.Invoke(componentBase);
		}
		public void UpdateProducts(ComponentBase componentBase)
		{
			OnProductsChanged?.Invoke(componentBase);
		}
        public void UpdatePage(ComponentBase componentBase)
        {
            OnPageChanged?.Invoke(componentBase);
        }
    }
}
