using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace WebUI.Class.Articles_Model.model_associations_many_to_many_csharp_backend
{
    public class PlaylistsPayload
    {
        [JsonProperty(propertyName: "playlists")]
        public List<Playlist> Playlists { get; set; }
        [JsonProperty(propertyName: "count")]
        public int Count { get; set; }
    }
}