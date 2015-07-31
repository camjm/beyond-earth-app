using System.Collections.Generic;

namespace BeyondEarthApp.Web.Api.Models
{
    public class User
    {
        public long UserId { get; set; }

        public string Username { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public List<Game> Games { get; set; }

        #region Serialization

        private bool _serializeGames;

        public void SetSerializeGames(bool serialize)
        {
            _serializeGames = serialize;
        }

        public bool SerializeGames()
        {
            return _serializeGames;
        }

        #endregion

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
