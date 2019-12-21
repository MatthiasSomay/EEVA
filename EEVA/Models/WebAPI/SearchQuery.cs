using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EEVA.Web.Models.WebAPI
{
    public class SearchQuery
    {
        [Required]
        public string Keyword { get; set; }
        [Range(1, 500)]
        public int MaxResults { get; set; } = 100;
    }
}
