# DPS Web Experience Sample Website

Sample web site that renders articles published by Azure DPS (Documentation Publishing System). This sample should be used by teams onboarding to DPS as a reference for implementing their rendering experience.

## Documentation Controller class
The __DocumentationController__ class provides the necessary methods to list and give the details of the _documentation articles_.

### Public methods

#### Index
Lists the documentation articles for the specified __culture__.

##### Parameters
- _culture_
	- Type: System.String
	- Culture from where the articles list will be loaded.

#### Remarks
The documentation articles will be loaded from the storage container specified in the _container_ app settings. In case the container does not contain articles for the specified culture, the controller will try to load the content from the default culture (__en-us__) folder.

#### Article
Loads a documentation article __culture__.

##### Parameters
- _culture_
	- Type: System.String
	- Culture from where the articles list will be loaded.
- id
	- Type: System.String
	- The identifier of the _documentation article_.

#### Remarks
The article will be loaded from the storage container specified in the _container_ app settings. In case the container does not contain the requested article for the specified culture, the controller will try to load the content from the default culture (__en-us__) folder.

### Private methods

#### GetPublishedVersion
Loads the version info from the settings file in the storage container.

##### Parameters
- _blobContainer_
	- Type: Microsoft.WindowsAzure.Storage.Blob.CloudBlobContainer
	- The blob storage container.
- _culture_
	- Type: System.String
	- Culture identifier.

#### Remarks
The __PublishVersionInfo__ will be deserialized from the _settings.json_ file located in the culture folder of the given storage container.

#### GetContent
Loads the blob text content.

##### Parameters
- _blob_
	- Type: Microsoft.WindowsAzure.Storage.Blob.CloudBlockBlob
	- Documentation article blob..

#### Remarks
This method returns an HTML-encoded string with the blob content.

#### GetArticleMetada
Parse the documentation article metadata from the given blob.

##### Parameters
- _blockBlob_
	- Type: Microsoft.WindowsAzure.Storage.Blob.CloudBlockBlob
	- Documentation article blob.

#### Remarks
This method extract the metadata attributes of the given blob and returns a new documentation __Article__ object with this parsed data.

#### GetContributorsAndAuthors
Get a list containing the authors of the article and the contributors.

##### Parameters
- _article_
	- Type: ACOM.DocumentationSample.Models.Article
	- A documentation article object.

#### Remarks
Get a unified list of __GithubAuthor__ containing the article Authors and Contributors.

#### ParseTags
Parse the article tags.

##### Parameters
- _tagsString_
	- Type: System.String
	- A JSON array containing the article tags.

#### Remarks
Returns the deserialized dictionary with the documentation article tags.

#### GetSlugFromBlobName
Resolve the slug from a blob name.

##### Parameters
- _blobName_
	- Type: System.String
	- Blob name.

#### Remarks
Returns the slugified blob name.

#### ParseTags
Parse the article tags

##### Parameters
- _tagsString_
	- Type: System.String
	- A JSON array containing the article tags.

#### Remarks
Returns the deserialized dictionary with the documentation article tags.

#### ParseAuthors
Parse the authors names from a comma separated string.

##### Parameters
- _articleAuthorString_
	- Type: System.String
	- Comma separated array of author names.

#### Remarks
Returns an array of strings containing the parsed authors.

#### ParseGitHubContributors
Deserialize the GithubContributors list.

##### Parameters
- _githubContributorsString_
	- Type: System.String
	- JSON string containing the array of Github Authors.

#### Remarks
Returns a collection with the parsed __GithubAuthors__.

## Documentation Article metadata
|__Property__|__Description__|__Example__|
|:-------|:----|:--------------------:|
|x-ms-meta-pageTitle|Page title| ```Page title```|
|x-ms-meta-articleTitle|Article title| ```Article title```|
|x-ms-meta-metaDescription|Article meta description| ```This is an article```|
|x-ms-meta-articleAuthor|CSV list of authors|```author1, author2```|
|x-ms-meta-githubContributors|Contributors JSON array|```[{"Id":"123456", "Login":"jdoe", "Name":"John Doe"},{"Id":"654321", "Login":"user1", "Name":"Another User"}]```
| | |


