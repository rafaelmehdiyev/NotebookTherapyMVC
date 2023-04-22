using Microsoft.AspNetCore.Mvc;

namespace NotebookTherapyMVC.Areas.Manage.Controllers
{
	[Area("Manage")]
	[Authorize(Roles = "Admin")]
	public class ReviewController : Controller
    {
		private readonly IReviewService _reviewService;

        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        public async Task<IActionResult> Index()
		{
			IDataResult<List<ReviewGetDto>> result = await _reviewService.GetAllAsync(false,Includes.ReviewIncludes);
			return View(result);
		}
		[HttpGet]
		public async Task<IActionResult> Get(int id)
		{
			IDataResult<ReviewGetDto> result = await _reviewService.GetByIdAsync(id,Includes.ReviewIncludes);
			return PartialView("_getReviewPartial", result);
		}

	
		public async Task<IActionResult> Delete(int id)
		{
			ReviewGetDto result = (await _reviewService.GetByIdAsync(id)).Data;
			if (result == null) { return RedirectToAction(nameof(Index)); }
			await _reviewService.SoftDeleteByIdAsync(id);
			return RedirectToAction(nameof(Index));
		}

		public async Task<IActionResult> Recover(int id)
		{
			ReviewGetDto result = (await _reviewService.GetByIdAsync(id)).Data;
			if (result == null) { return RedirectToAction(nameof(Index)); }
			await _reviewService.RecoverByIdAsync(id);
			return RedirectToAction(nameof(Index));
		}
		public async Task<IActionResult> HardDelete(int id)
		{
			ReviewGetDto result = (await _reviewService.GetByIdAsync(id)).Data;
			if (result == null) { return RedirectToAction(nameof(Index)); }
			await _reviewService.HardDeleteByIdAsync(id);
			return RedirectToAction(nameof(Index));
		}
	}
}
