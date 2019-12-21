using EEVA.Web.Models.WebAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EEVA.Web.Models
{
    public class ContactSearchViewModel
    {
        public string Subtitle { get { return "Zoeken naar contacten"; } }

        public SearchQuery Query { get; set; } = new SearchQuery();
    }
}
