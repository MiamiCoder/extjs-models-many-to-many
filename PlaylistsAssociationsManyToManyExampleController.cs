using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;

namespace WebUI.Class.Articles_Model.model_associations_many_to_many_csharp_backend
{
    public class PlaylistsAssociationsManyToManyExampleController : ApiController
    {
        public PlaylistsPayload Get(int page, int start, int limit)
        {
            string countSql = string.Format("SELECT COUNT(p.PlaylistId) FROM `chinook`.`playlist` p join `chinook`.`playlisttrack` pt on (p.PlaylistId = pt.PlaylistId) LIMIT {0}, {1}",
                    start, limit);
            string resultsetSql = string.Format("SELECT p.PlaylistId, p.Name FROM `chinook`.`playlist` p join `chinook`.`playlisttrack` pt on (p.PlaylistId = pt.PlaylistId)  LIMIT {0}, {1}",
                        start, limit);

            return GetPlaylistsPayload(countSql, resultsetSql);
        }
        
        public PlaylistsPayload Get(string filter)
        {

            Filter[] filters = JsonConvert.DeserializeObject<Filter[]>(filter);

            string countSql = string.Format("SELECT COUNT(p.PlaylistId) FROM `chinook`.`playlist` p join `chinook`.`playlisttrack` pt on (p.PlaylistId = pt.PlaylistId) where pt.{0} = {1}",
                    filters[0].Property, filters[0].Value);
            string resultsetSql = string.Format("SELECT p.PlaylistId, p.Name FROM `chinook`.`playlist` p join `chinook`.`playlisttrack` pt on (p.PlaylistId = pt.PlaylistId) where pt.{0} = {1}",
                        filters[0].Property, filters[0].Value);

            return GetPlaylistsPayload(countSql, resultsetSql);
        }

        private PlaylistsPayload GetPlaylistsPayload(string countSql, string resultsetSql)
        {
            PlaylistsPayload payload = new PlaylistsPayload();
            List<Playlist> playlists = new List<Playlist>();
            payload.Playlists = playlists;
            int count = 0;           

            using (MySqlConnection cn = new MySqlConnection("server=127.0.0.1;uid=root;pwd=root;database=chinook;"))
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.CommandText = countSql;
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Connection = cn;
                    cn.Open();

                    object countObject = cmd.ExecuteScalar();
                    count = int.Parse(countObject.ToString());
                    payload.Count = count;

                    cmd.CommandText = resultsetSql;
                    cmd.CommandType = System.Data.CommandType.Text;

                    MySqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Playlist order = new Playlist()
                        {
                            PlaylistId = reader.GetInt32(0),
                            Name = reader.GetString(1)
                        };
                        playlists.Add(order);
                    }

                    cn.Close();

                }

                return payload;
            }
        }
    }
}
