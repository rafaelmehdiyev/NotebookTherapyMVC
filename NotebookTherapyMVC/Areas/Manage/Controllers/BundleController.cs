﻿namespace NotebookTherapyMVC.Areas.Manage.Controllers
{
    [Area("Manage")]
    [Authorize(Roles = "Admin")]
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
            var result = await _bundleService.GetAllAsync();
            return View(result);
        }
        [HttpPost]
        public async Task<IActionResult> Create(BundlePostDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var result = await _bundleService.CreateAsync(dto);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var result = await _bundleService.GetByIdAsync(id);
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
            var result = (await _bundleService.GetByIdAsync(id)).Data;
            if (result == null) { return RedirectToAction(nameof(Index)); }
            await _bundleService.SoftDeleteByIdAsync(id);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Recover(int id)
        {
            var result = (await _bundleService.GetByIdAsync(id)).Data;
            if (result == null) { return RedirectToAction(nameof(Index)); }
            await _bundleService.RecoverByIdAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
