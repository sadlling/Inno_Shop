using ProductManagement.Application.Common.Paging;

namespace ProductManagement.Application.Common.Filtering
{
    public class ProductParameters:QueryStringParameters
    {
        public float MinCost { get; set; }
        public float MaxCost { get; set; }
        public bool IsEnabled { get; set; } = true;
        public bool ValidCostRange => MinCost <= MaxCost;  

    }
}
