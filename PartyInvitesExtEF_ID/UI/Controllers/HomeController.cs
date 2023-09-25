using System.Diagnostics;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Domain.Models;
using UI.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace PartyInvitesExtEF.Controllers;

[AutoValidateAntiforgeryToken]
[Authorize(Policy = "LoggedInUser")]
public class HomeController : Controller
{
    private readonly IGuestResponseRepository _repository;


    public HomeController(IGuestResponseRepository repository)
    {
        _repository = repository;
    }

    public int PageSize { get; set; } = 2;

    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public ViewResult RsvpForm()
    {
        var choices = new[] {
            new { Answer = "", Desc = "Choose an option"},
            new { Answer = "true", Desc = "Yes, I'll be there" },
            new { Answer = "false", Desc = "No, I can't come" }

        };
        ViewBag.AttendanceChoices = new SelectList(choices, "Answer", "Desc");
        return View();
    }

    [HttpPost]
    public IActionResult RsvpForm(GuestResponse guestResponse)
    {
        if (ModelState.GetValidationState(nameof(GuestResponse.Email)) == ModelValidationState.Valid && !guestResponse.Email!.EndsWith(".nl"))
        {
            ModelState.AddModelError(nameof(guestResponse.Email), "Only .nl domains allowed");
        }

        if (ModelState.IsValid)
        {
            _repository.CreateResponse(guestResponse);
            return RedirectToAction(nameof(Thanks), guestResponse);
        }
        else
        {
            var choices = new[] {
                new { Answer = "", Desc = "Choose an option"},
                new { Answer = "true", Desc = "Yes, I'll be there" },
                new { Answer = "false", Desc = "No, I can't come" }
            };
            ViewBag.AttendanceChoices = new SelectList(choices, "Answer", "Desc");
            return View();
        }
    }

    public IActionResult Thanks(GuestResponse guestResponse)
    {
        return View(guestResponse);
    }

    public IActionResult Responses(int? idx)
    {
        if (idx == null)
        {
            var viewModel = new ListResponsesViewModel
            {
                GuestResponses = _repository.Responses,
                PagingInfo = new PagingInfo
                {
                    CurrentPage = 1,
                    ItemsPerPage = PageSize,
                    TotalItems = _repository.Responses.Count(),
                },
            };
            return View("ListResponses", viewModel);
            // of redirect (misschien netter maar kan gebruiker verwarren) -->
            //return RedirectToAction("ListResponses");
        }
        
        var response = _repository.Responses.ElementAtOrDefault(idx.Value);
        if (response == null)
        {
            return NotFound();
        }
        

        return View("ResponseDetail", response);
    }

    public IActionResult ListResponses(int page = 1)
    {
        var guestResponses = _repository.Responses
            .Where(r => r.WillAttent == true)
            .OrderBy(r => r.Name)
            .Skip((page - 1) * PageSize)
            .Take(PageSize);

        var viewModel = new ListResponsesViewModel
        {
            GuestResponses = guestResponses,
            PagingInfo = new PagingInfo
            {
                CurrentPage = page,
                ItemsPerPage = PageSize,
                TotalItems = _repository.Responses.Count(),
            },
        };

        return View(viewModel);
    }

    public IActionResult ResponseDetail(int index)
    {
        var guestResponse = _repository.Responses.ElementAtOrDefault(index);
        if (guestResponse == null)
        {
            return NotFound();
        }

        return View(guestResponse);
    }

    public IActionResult Privacy()
    {
        return View();
    }


}
