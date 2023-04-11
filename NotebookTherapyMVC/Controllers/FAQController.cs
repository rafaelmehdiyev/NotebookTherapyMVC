namespace NotebookTherapyMVC.Controllers
{
    public class FAQController : Controller
    {
        private readonly IFAQService _faqService;
        private readonly IFAQCategoryService _faqCategoryService;

        public FAQController(IFAQService faqService, IFAQCategoryService faqCategoryService)
        {
            _faqService = faqService;
            _faqCategoryService = faqCategoryService;
        }

        public async Task<IActionResult> Index()
        {
            IDataResult<List<FAQCategoryGetDto>> categoryResults = await _faqCategoryService.GetAllAsync(false,Includes.FAQCategoryIncludes);
            return View(categoryResults);
        }
    }
}
