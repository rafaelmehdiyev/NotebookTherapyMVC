using Entities.DTOs.Color;

namespace NotebookTherapyMVC.Areas.Manage.Controllers
{

    [Area("Manage")]

    public class ColorController : Controller
    {
		private readonly IColorService _colorService;
		private readonly IMapper _mapper;

        public ColorController(IColorService colorService, IMapper mapper)
        {
            _colorService = colorService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
		{
			var result = await _colorService.GetAllAsync();
			return View(result);
		}
		[HttpPost]
		public async Task<IActionResult> Create(ColorPostDto dto)
		{
			if (!ModelState.IsValid)
			{
				return View();
			}
			var result = await _colorService.CreateAsync(dto);
			return RedirectToAction(nameof(Index));
		}

		[HttpGet]
		public async Task<IActionResult> Update(int id)
		{
			var result = await _colorService.GetByIdAsync(id);
			ColorUpdateDto dto = _mapper.Map<ColorUpdateDto>(result.Data);
			return View(dto);
		}
		[HttpPost]
		public async Task<IActionResult> Update(ColorUpdateDto dto)
		{
			if (!ModelState.IsValid)
			{
				return View(dto);
			}
			await _colorService.UpdateAsync(dto);
			return RedirectToAction(nameof(Index));
		}
		public async Task<IActionResult> Delete(int id)
		{
			var result = (await _colorService.GetByIdAsync(id)).Data;
			if (result == null) { return RedirectToAction(nameof(Index)); }
			await _colorService.SoftDeleteByIdAsync(id);
			return RedirectToAction(nameof(Index));
		}
		public async Task<IActionResult> Recover(int id)
		{
			var result = (await _colorService.GetByIdAsync(id)).Data;
			if (result == null) { return RedirectToAction(nameof(Index)); }
			await _colorService.RecoverByIdAsync(id);
			return RedirectToAction(nameof(Index));
		}
	}
}
