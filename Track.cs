using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebUI.Class.Articles_Model.model_associations_many_to_many_csharp_backend
{
    public class Track
    {
        public int TrackId { get; set; }
        public string Name { get; set; }
        public int AlbumId { get; set; }
    }
}