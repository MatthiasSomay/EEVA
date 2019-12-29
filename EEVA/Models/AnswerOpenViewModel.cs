using EEVA.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EEVA.Web.Models
{
    public class AnswerOpenViewModel
    {
      
        public Question Question { get; set; }
        public IEnumerable<string> Keywords { get; set; }
    }
}
