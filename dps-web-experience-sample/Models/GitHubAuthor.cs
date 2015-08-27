using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ACOM.DocumentationSample.Models
{
    [Serializable]
    public class GithubAuthor
    {
        public string Id { get; set; }

        public string Login { get; set; }

        public string Name { get; set; }
    }
}