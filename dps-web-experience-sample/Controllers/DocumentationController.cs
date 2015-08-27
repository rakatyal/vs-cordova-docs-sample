using ACOM.DocumentationSample.Models;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.RetryPolicies;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace ACOM.DocumentationSample.Controllers
{

  public class DocumentationController : Controller
  {
    ArticlesConfiguration _config;
    CloudStorageAccount _storageAccount;
    CloudBlobClient _blobClient;
    CloudBlobContainer _blobContainer;
    string _partnerName;

    public DocumentationController()
    {
      _config = new ArticlesConfiguration
      {
        ConnectionString = WebConfigurationManager.ConnectionStrings["articles"].ConnectionString,
        Container = WebConfigurationManager.AppSettings["container"],
        ArticleListFormat = "{0}/{1}/documentation/articles/{2}.html",
        SettingsName = "settings.json"
      };

      _storageAccount = CloudStorageAccount.Parse(_config.ConnectionString);
      _blobClient = _storageAccount.CreateCloudBlobClient();
      _blobContainer = _blobClient.GetContainerReference(_config.Container);

      _partnerName = WebConfigurationManager.AppSettings["partner"];
    }

    [Route("{culture=en-us}")]
    public ActionResult Index(string culture)
    {
      var version = GetPublishedVersion(_blobContainer, culture);
      var blobPrefixFormat = _config.ArticleListFormat.Replace("{2}.html", "");
      var prefix = string.Format(CultureInfo.InvariantCulture, blobPrefixFormat, culture, version.PublishedVersion);

      var model = new DocumentationArticlesListModel();
      model.PageTitle = _partnerName + " Documentation Articles";
      foreach (CloudBlockBlob articleBlob in _blobContainer.ListBlobs(prefix, false).OfType<CloudBlockBlob>())
      {
        // Populate the blob's attributes.
        articleBlob.FetchAttributes();

        var articleMetadata = GetArticleMetada(articleBlob);
        model.Articles.Add(articleMetadata);
      }
      model.Count = model.Articles.Count;

      return View(model);
    }

    [Route("{culture=en-us}/docs/{id}")]
    public ActionResult Article(string culture, string id)
    {
      var version = GetPublishedVersion(_blobContainer, culture);

      var blobUrl = string.Format(CultureInfo.InvariantCulture, _config.ArticleListFormat, culture, version.PublishedVersion, id);
      CloudBlockBlob blob = _blobContainer.GetBlockBlobReference(blobUrl);
      if (!blob.Exists())
      {
        throw new HttpException(404, "Page not found");
      }

      var articleMetadata = GetArticleMetada(blob);
      var articleContent = GetContent(blob);

      var viewModel = new DocumentationArticleMetadataModel
      {
        Slug = articleMetadata.Slug,
        Tags = articleMetadata.Tags,
        Content = articleContent,
        PageTitle = articleMetadata.PageTitle,
        ArticleTitle = articleMetadata.ArticleTitle,
        MetaDescription = articleMetadata.MetaDescription,
        Contributors = this.GetContributorsAndAuthors(articleMetadata)
      };


      return View(viewModel);
    }

    #region Private
    /// <summary>
    /// Every culture can have a separate version
    /// </summary>
    internal PublishVersionInfo GetPublishedVersion(CloudBlobContainer blobContainer, string culture)
    {
      PublishVersionInfo version = null;
      var blob = blobContainer.GetBlockBlobReference(culture + "/" + _config.SettingsName);
      if (blob.Exists())
      {
        var contents = blob.DownloadTextAsync().Result;
        version = JsonConvert.DeserializeObject<PublishVersionInfo>(contents);

      }

      return version;
    }
    #endregion

    private IHtmlString GetContent(CloudBlockBlob blob)
    {
      var contents = blob.DownloadTextAsync().Result;

      return new HtmlString(contents);
    }

    private Article GetArticleMetada(CloudBlockBlob blockBlob)
    {
      var lastModifiedProp = blockBlob.Properties.LastModified;
      var pageTitle = string.Empty;
      var articleTitle = string.Empty;
      var metaDescription = string.Empty;
      var serviceString = string.Empty;
      var docCenterString = string.Empty;
      var tagsString = string.Empty;
      var createdDate = string.Empty;
      var githubContributorsString = string.Empty;
      string articleAuthorString = string.Empty;


      

      blockBlob.Metadata.TryGetValue("articleTitle", out articleTitle);
      blockBlob.Metadata.TryGetValue("pageTitle", out pageTitle);
      blockBlob.Metadata.TryGetValue("metaDescription", out metaDescription);
      blockBlob.Metadata.TryGetValue("articleServices", out serviceString);
      blockBlob.Metadata.TryGetValue("articleDocumentationCenter", out docCenterString);
      blockBlob.Metadata.TryGetValue("tags", out tagsString);
      blockBlob.Metadata.TryGetValue("createdDate", out createdDate);
      blockBlob.Metadata.TryGetValue("articleAuthor", out articleAuthorString);
      blockBlob.Metadata.TryGetValue("gitHubContributors", out githubContributorsString);

      if (string.IsNullOrEmpty(pageTitle))
      {
        pageTitle = articleTitle;
      }

      return new Article
      {
        LastModified = lastModifiedProp != null ? lastModifiedProp.Value.UtcDateTime : new DateTime(),
        Slug = this.GetSlugFromBlobName(blockBlob.Name),
        BlobName = blockBlob.Name,
        PageTitle = WebUtility.HtmlDecode(pageTitle),
        ArticleTitle = WebUtility.HtmlDecode(articleTitle),
        MetaDescription = WebUtility.HtmlDecode(metaDescription),
        Tags = this.ParseTags(tagsString),
        Authors = this.ParseAuthors(articleAuthorString),
        GitHubContributors = this.ParseGitHubContributors(WebUtility.HtmlDecode(githubContributorsString)).ToArray(),
      };
    }

    internal IEnumerable<GithubAuthor> GetContributorsAndAuthors(Article article)
    {
        var authors = article.Authors.SelectMany(a => article.GitHubContributors.Where(c => c.Login.Equals(a, StringComparison.InvariantCultureIgnoreCase))).ToList();
        var otherContributors = article.GitHubContributors.Except(authors).Reverse().ToList();

        return authors.Union(otherContributors);
    }

    internal Dictionary<string, string> ParseTags(string tagsString)
    {
      var tagsDictionary = new Dictionary<string, string>();

      if (!string.IsNullOrEmpty(tagsString))
      {
        tagsDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(tagsString);
      }

      return tagsDictionary;
    }

    internal string GetSlugFromBlobName(string blobName)
    {
      return blobName.Split(new[] { "/" }, StringSplitOptions.RemoveEmptyEntries).Last().Replace(".html", string.Empty);
    }

    internal string[] ParseAuthors(string articleAuthorString)
    {
        if (string.IsNullOrWhiteSpace(articleAuthorString))
        {
            return new string[0];
        }

        return articleAuthorString.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(a => a.Trim()).ToArray();
    }

    internal IEnumerable<GithubAuthor> ParseGitHubContributors(string githubContributorsString)
    {
        if (!string.IsNullOrEmpty(githubContributorsString))
        {
            var obj = JToken.Parse(githubContributorsString);
            return obj.ToObject<List<GithubAuthor>>();
        }

        return Enumerable.Empty<GithubAuthor>();
    }
  }
}