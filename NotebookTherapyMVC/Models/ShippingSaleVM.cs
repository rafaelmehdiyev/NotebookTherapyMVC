namespace NotebookTherapyMVC.Models
{
    public class ShippingSaleVM
    {
        public ShippingPostDto? shippingDto { get; set; }
        public ShippingGetDto? shipping { get; set; }
        public int? SaleId { get; set; }
        public SaleGetDto? Sale { get; set; }
    }
}
