﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ACOM.DocumentationSample.Models
{
    public class ArticlesConfiguration
    {
        public string ConnectionString { get; set; }

        public string Container { get; set; }

        public string ArticleListFormat { get; set; }

        public string SettingsName { get; set; }

        public TimeSpan CacheExpiration { get; set; }
    }
}