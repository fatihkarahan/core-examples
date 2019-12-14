using Elasticsearch.Net;
using Nest;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace ES
{
    public class ESManager 
    {
        private static ElasticClient _client = null;
        public static ElasticClient Client
        {
            get
            {
                if (_client == null)
                {
                    lock (new Object())
                    {
                        if (_client == null)
                        {
                            List<Uri> nodes = new List<Uri>();
                            
                                nodes.Add(new Uri("http://localhost:9200"));
                            var pool = new SniffingConnectionPool(nodes);
                            ConnectionSettings settings = null;
                            _client = new ElasticClient(settings);
                        }
                    }
                }
                return _client;
            }
        }
    }
}
