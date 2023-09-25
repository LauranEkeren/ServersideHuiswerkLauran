using Microsoft.AspNetCore.Mvc;

namespace PartyInvites.Models
{
    public class AmountComingViewComponent : ViewComponent
    {

        private readonly IRepository _repository;

        public AmountComingViewComponent(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            int count = _repository.GetResponses().Count();
            return await Task.FromResult((IViewComponentResult)View("AmountComing", count));
        }
    }
}
