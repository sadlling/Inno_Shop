using ProductManagement.Application.Common.Paging;

namespace ProductManagement.Application.Common.Filtering
{
    public class ProductParameters:QueryStringParameters
    {
        public string? Name { get; set; }
        public float? MinCost { get; set; } = 0;
        public float? MaxCost { get; set; } = float.MaxValue;
        public bool? IsEnabled { get; set; }

        public bool IsValidCostRange() => MinCost <= MaxCost;  

    }
}
