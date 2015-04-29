using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebUI.Class.Articles_Model.model_associations_many_to_many_csharp_backend
{
    public class Filter
    {
        public string Property { get; set; }
        public string Value { get; set; }
        public bool ExactMatch { get; set; }
    }
}