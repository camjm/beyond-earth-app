namespace BeyondEarthApp.Web.Api.Models.Paging
{
    public interface IPageLinkContaining : ILinkContaining
    {
        int PageNumber { get; set; }

        int PageCount { get; set; }

        int PageSize { get; set; }
    }
}
