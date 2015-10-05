using System.ComponentModel.DataAnnotations;

namespace BeyondEarthApp.Web.Api.Models.Initial
{
    public class NewGame
    {
        [Required(AllowEmptyStrings = false)]
        public string Description { get; set; }

        public Faction Faction { get; set; }
    }
}
