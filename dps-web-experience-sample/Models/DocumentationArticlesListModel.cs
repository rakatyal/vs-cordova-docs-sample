using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ACOM.DocumentationSample.Models
{
    public class DocumentationArticlesListModel
    {
        public DocumentationArticlesListModel()
        {
            this.Articles = new List<Article>();
        }

        public string PageTitle { get; set; }

        public int Count { get; set; }

        public List<Article> Articles { get; set; }
    }
}