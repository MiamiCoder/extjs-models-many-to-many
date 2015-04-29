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
    public class TracksAssociationsManyToManyExampleController : ApiController
    {
        public TracksPayload Get(int page, int start, int limit)
        {
            string countSql = string.Format("SELECT COUNT(t.TrackId) FROM `chinook`.`track` t join `chinook`.`playlisttrack` pt on (t.TrackId = pt.TrackId) LIMIT {0}, {1}",
                    start, limit);
            string resultsetSql = string.Format("SELECT t.TrackId, t.Name, t.AlbumId, pt.PlaylistId FROM `chinook`.`track` t join `chinook`.`playlisttrack` pt on (t.TrackId = pt.TrackId)  LIMIT {0}, {1}",
                        start, limit);

            return GetTracksPayload(countSql, resultsetSql);
        }

        public TracksPayload Get(string filter)
        {
            Filter[] filters = JsonConvert.DeserializeObject<Filter[]>(filter);

            string countSql = string.Format("SELECT COUNT(t.TrackId) FROM `chinook`.`track` t join `chinook`.`playlisttrack` pt on (t.TrackId = pt.TrackId) where pt.{0} = {1}",
                    filters[0].Property, filters[0].Value);
            string resultsetSql = string.Format("SELECT t.TrackId, t.Name, t.AlbumId, pt.PlaylistId FROM `chinook`.`track` t join `chinook`.`playlisttrack` pt on (t.TrackId = pt.TrackId) where pt.{0} = {1}",
                        filters[0].Property, filters[0].Value);

            return GetTracksPayload(countSql, resultsetSql);
        }

        private TracksPayload GetTracksPayload(string countSql, string resultsetSql)
        {

            TracksPayload payload = new TracksPayload();
            List<Track> tracks = new List<Track>();
            payload.Tracks = tracks;
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
                        Track track = new Track()
                        {
                            TrackId = reader.GetInt32(0),
                            Name = reader.GetString(1)
                        };
                        tracks.Add(track);
                    }

                    cn.Close();

                }
            }

            return payload;
        }
    }
}
