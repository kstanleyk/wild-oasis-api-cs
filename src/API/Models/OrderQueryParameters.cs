using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace WildOasis.API.Models
{
    public class OrderQueryParameters
    {
        [BindRequired]
        public string Branch { get; set; }
        [BindRequired]
        public string OrderType { get; set; }
        [BindRequired]
        public string ServiceUnit { get; set; }
    }
}
