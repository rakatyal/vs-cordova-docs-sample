using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ACOM.DocumentationSample.Models
{
  public class DocumentationArticleMetadataModel
  {
    public string PageTitle { get; set; }

    public string ArticleTitle { get; set; }

    public string MetaDescription { get; set; }

    public string Slug { get; set; }

    public Dictionary<string, string> Tags { get; set; }

    public IHtmlString Content { get; set; }

    public IEnumerable<GithubAuthor> Contributors { get; set; }

  }

}