namespace NotebookTherapyMVC.Models
{
    public class FilterProductVM
    {
        public FilterProductVM() : this(false,false) { }
        public FilterProductVM(int categoryId)
        {
            categoryIds = new[] { categoryId};
        }
        public FilterProductVM(bool onSale = false, bool isTrending = false)
        {
            this.onSale = onSale;
            orderFilter = isTrending ? 1 : 0;
        }
        public int[] categoryIds { get; set; }
        public int[] sizeIds { get; set; }
        public int[] colorIds { get; set; }
        public int[] bundleIds { get; set; }
        public int priceRange { get; set; } = 1000;
        public bool onSale { get; set; } = false;
        public int orderFilter { get; set; } = 0;
    }
}
