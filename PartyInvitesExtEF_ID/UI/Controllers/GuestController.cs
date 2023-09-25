using Domain.Models;
using Domain.Services;
using Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using UI.ViewModels;

namespace PartyInvitesExtEF.Controllers;
[Authorize(Policy = "PrivilegedUser")]
public class GuestController : Controller
{
    private readonly IGuestResponseRepository _repository;
    private readonly ILogger<GuestController> _logger;


    public GuestController(IGuestResponseRepository repository, ILogger<GuestController> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public int PageSize { get; set; } = 3;

    [AllowAnonymous]
    public IActionResult Index()
    {
        return View();
    }

    [Authorize(Policy = "LoggedInUser")]
    public IActionResult List(int page = 1)
    {
        var viewModel = new GuestListViewModel
        {
            GuestResponses = _repository.Responses,
            PagingInfo = new PagingInfo
            {
                CurrentPage = page,
                ItemsPerPage = PageSize,
                TotalItems = _repository.Responses.Count(),
            },
        };
        return View(viewModel);
    }

    public IActionResult Details(int id)
    {
        var model = _repository.ReadResponse(id);

        if (model == null)
        {
            return NotFound();
        }


        return View(model);
    }

    public IActionResult Create()
    {
        var model = new GuestResponse();

        ViewBag.AttendanceChoices = CreateAttendenceSelectList();
        return View(model);
    }

    [HttpPost]
    public IActionResult Create(GuestResponse modelToCreate)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.AttendanceChoices = CreateAttendenceSelectList();
            return View();
        }

        var createdModel = _repository.CreateResponse(modelToCreate);

        if (createdModel == null)
        {
            ModelState.AddModelError("error", "Error while Creating");
            return View();
        }

        return RedirectToAction(nameof(Details), new { Id = createdModel.Id });
    }

    

    public IActionResult Edit(int id)
    {
        var model = _repository.ReadResponse(id);

        if (model == null)
        {
            return NotFound();
        }

        // Fill in the validation email so that the user doesn't need to bother
        // with it if it does not change -->
        model.EmailValidation = model.Email;

        ViewBag.AttendanceChoices = CreateAttendenceSelectList();
        return View(model);
    }

    [HttpPost]
    public IActionResult Edit(GuestResponse modelToUpdate)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.AttendanceChoices = CreateAttendenceSelectList();
            return View();
        }

        var updatedModel = _repository.UpdateResponse(modelToUpdate);

        if (updatedModel == null)
        {
            ModelState.AddModelError("error", "Error while updating");
            return View();
        }

        return RedirectToAction(nameof(Details), new { Id = updatedModel.Id });

    }

    [Authorize(Policy = "EventOwner")]
    public IActionResult DeleteConfirm(int id)
    {
        var entityToDelete = _repository.ReadResponse(id);

        if (entityToDelete == null)
        {
            return NotFound();
        }

        return View("DeleteConfirm", entityToDelete);
    }

    [Authorize(Policy = "EventOwner")]
    public IActionResult Delete(int id)
    {
        var entityToDelete = _repository.ReadResponse(id);

        if (entityToDelete == null)
        {
            return NotFound();
        }

        _repository.DeleteResponse(entityToDelete);

        return View("Deleted", entityToDelete);
    }

    private SelectList CreateAttendenceSelectList()
    {
        var choices = new[] {
                new { Answer = "", Desc = "Choose an option"},
                new { Answer = "true", Desc = "Yes, I'll be there" },
                new { Answer = "false", Desc = "No, I can't come" }
            };
        return new SelectList(choices, "Answer", "Desc");
    }
}
