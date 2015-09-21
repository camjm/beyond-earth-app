using System.Collections.Generic;

namespace BeyondEarthApp.Web.Api.Models
{
    /// <summary>
    /// Data transfer object (DTO) that can return type-safe paged data and relevant hypermedia links
    /// </summary>
    public class PagedDataInquiryResponse<T> : IPageLinkContaining
    {
        private List<T> _items;

        public List<T> Items
        {
            get { return _items ?? (_items = new List<T>()); }
            set { _items = value; }
        }

        public int PageSize { get; set; }

        public int PageNumber { get; set; }

        public int PageCount { get; set; }

        #region Links

        private List<Link> _links;

        public List<Link> Links
        {
            get { return _links ?? (_links = new List<Link>()); }
            set { _links = value; }
        }

        public void AddLink(Link link)
        {
            Links.Add(link);
        }

        #endregion
    }
}
