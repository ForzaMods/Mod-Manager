﻿using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json.Linq;

namespace Mod_Manager_V2.Resources
{
    public class ModPage
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Version { get; set; }
        public string Creator { get; set; }
        public string Description { get; set; }
        public string ImageLink { get; set; }
        public string FileLink { get; set; }
        public string UploadDate { get; set; }
        public List<string> FilePaths { get; set; }
    }

    public class ModPageParser
    {
        private const string rawUrl = "https://cdn.discordapp.com/attachments/555877092731125820/1116324036788629584/test.json";

        public async Task<List<ModPage>> ParseModPagesFromGitHub()
        {
            string json = await DownloadJsonFromUrl(rawUrl);
            JObject jsonObject = JObject.Parse(json);
            JArray modsArray = (JArray)jsonObject["mods"];
            List<ModPage> modPages = modsArray.ToObject<List<ModPage>>();
            return modPages;
        }

        private async Task<string> DownloadJsonFromUrl(string url)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(url);
                    return await response.Content.ReadAsStringAsync();
                }
                catch
                {
                    return string.Empty;
                }
            }
        }
    }
}