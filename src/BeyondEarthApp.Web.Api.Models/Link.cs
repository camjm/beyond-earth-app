namespace BeyondEarthApp.Web.Api.Models
{
    /// <summary>
    /// Patterned from the standard HTML link element
    /// </summary>
    public class Link
    {
        /// <summary>
        /// The relationship between the resource and the resource identified by the link
        /// </summary>
        public string Rel { get; set; }

        /// <summary>
        /// The linked resource's address
        /// </summary>
        public string Href { get; set; }

        /// <summary>
        /// The HTTP method used to access the resource
        /// </summary>
        public string Method { get; set; }
    }
}
