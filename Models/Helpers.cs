﻿using Models.BackEnd;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Models
{
    public partial class Artists
    {
        [JsonProperty("Id")]
        public long Id { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("Genres")]
        public object Genres { get; set; }

        [JsonProperty("Images")]
        public object Images { get; set; }

        [JsonProperty("Songs")]
        public object Songs { get; set; }

        [JsonProperty("Albums")]
        public object Albums { get; set; }

        [JsonProperty("Popularity")]
        public long Popularity { get; set; }
    }

    public static class Helpers
    {
        public static Tracks GetTrack(Models.Spotify.Tracks old)
        {
            var model = new Tracks
            {
                Id = 0,
                Total = old.Total,
                Items = old.Items != null ? Helpers.GetItems(old.Items) : null
            };
            return model;
        }

        public static List<Item> GetItems(List<Models.Spotify.Item> old)
        {
            List<Item> list = new List<Item>();
            foreach (var x in old)
            {
                var model = new Item
                {
                    Id = 0,
                    //Artists = x.Artists != null ? GetArtist(x.Artists) : null,
                    DiscNumber = x.DiscNumber,
                    DurationMs = x.DurationMs,
                    Explicit = x.Explicit,
                    IsLocal = x.IsLocal,
                    IsPlayable = true,
                    Name = x.Name,
                    TrackNumber = x.TrackNumber
                };
                list.Add(model);
            }
            return list;
        }

        public static List<Copyright> GetCopyrights(List<Models.Spotify.Copyright> cop)
        {
            List<Copyright> list = new List<Copyright>();
            foreach (var a in cop)
            {
                var model = new Copyright
                {
                    Id = 0,
                    Text = a.Text,
                    Type = a.Type
                };
                list.Add(model);
            }
            return list;
        }

        public static List<Artist> GetArtist(List<Models.Spotify.Artist> Art)
        {
            List<Artist> artists = new List<Artist>();
            foreach (var a in Art)
            {
                var model = new Artist
                {
                    Id = 0,
                    Genres = a.Genres != null ? JsonConvert.SerializeObject(a.Genres.ToList()) : null,
                    Images = a.Images != null ? Helpers.GetImages(a.Images) : null,
                    Name = a.Name,
                    Popularity = 0
                };
                artists.Add(model);
            }
            return artists;
        }

        public static List<Image> GetImages(List<Models.Spotify.Image> images)
        {
            List<Image> Imgs = new List<Image>();
            foreach (var i in images)
            {
                var model = new Image
                {
                    Id = 0,
                    Height = i.Height,
                    Width = i.Width,
                    Url = i.Url.ToString()
                };

                Imgs.Add(model);
            }

            return Imgs;
        }
    }
}
