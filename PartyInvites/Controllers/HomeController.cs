using Microsoft.AspNetCore.Mvc;
using PartyInvites.Models;

namespace PartyInvites.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRepository _repository;

        public HomeController(IRepository repository)
        {
            _repository = repository;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ViewResult RsvpForm()
        {
            return View();
        }

        [HttpPost]
        public ViewResult RsvpForm(GuestResponse guestResponse)
        {
            if (ModelState.IsValid)
            {
                _repository.AddResponse(guestResponse);
                return View("Thanks", guestResponse);
            }
            else
            {
                return View();
            }
        }

        public ViewResult ListResponses()
        {
            return View("ListResponses", _repository.GetResponses().Where(r => r.WillAttend == true));
        }

        [Route("~/response/{id}")]
        [Route("Home/response/{id}")]
        public IActionResult GuestResponse(int id)
        {
            if (_repository.GetResponses().Count() <= id)
            {
                return RedirectToAction("Index");
            }
            GuestResponse response = _repository.GetResponses().ElementAt(id);  
            return View(response);
        }

        public IActionResult GuestResponseName(string name)
        {
            int id = _repository.GetResponses().TakeWhile(t => t.Name != name).Count();
            return RedirectToAction("response", new { id = id });
        }

        public IActionResult RenderPartial()
        {
            int amount = _repository.GetResponses().Count();
            return PartialView("Partial", amount);
        }
    }
}
