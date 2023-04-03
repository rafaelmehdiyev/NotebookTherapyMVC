namespace NotebookTherapyMVC.Areas.Manage.Controllers
{
    [Area("Manage")]
    //[Authorize(Roles = "Admin")]
    public class BundleController : Controller
    {
        private readonly IBundleService _bundleService;
        private readonly IMapper _mapper;

        public BundleController(IBundleService bundleService, IMapper mapper)
        {
            _bundleService = bundleService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            IDataResult<List<BundleGetDto>> result = await _bundleService.GetAllAsync(true);
            return View(result);
        }
        [HttpPost]
        public async Task<IActionResult> Create(BundlePostDto dto)
        {
            if (!ModelState.IsValid)
            {
                return PartialView("_bundleCreatePartial",dto);
            }
            IResult result = await _bundleService.CreateAsync(dto);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            IDataResult<BundleGetDto> result = await _bundleService.GetByIdAsync(id);
            BundleUpdateDto dto = _mapper.Map<BundleUpdateDto>(result.Data);
            return View(dto);
        }
        [HttpPost]
        public async Task<IActionResult> Update(BundleUpdateDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }
            await _bundleService.UpdateAsync(dto);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int id)
        {
            BundleGetDto result = (await _bundleService.GetByIdAsync(id)).Data;
            if (result == null) { return RedirectToAction(nameof(Index)); }
            await _bundleService.SoftDeleteByIdAsync(id);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Recover(int id)
        {
            BundleGetDto result = (await _bundleService.GetByIdAsync(id)).Data;
            if (result == null) { return RedirectToAction(nameof(Index)); }
            await _bundleService.RecoverByIdAsync(id);
            return RedirectToAction(nameof(Index));
        }
		public async Task<IActionResult> HardDelete(int id)
		{
            BundleGetDto result = (await _bundleService.GetByIdAsync(id)).Data;
			if (result == null) { return RedirectToAction(nameof(Index)); }
			await _bundleService.HardDeleteByIdAsync(id);
			return RedirectToAction(nameof(Index));
		}
	}
}

