﻿using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace CBSetoLib.Services
{
    public class PictureService
    {
        private readonly HttpClient _httpClient;
        public PictureService(HttpClient httpClient) => _httpClient = httpClient;

        public async Task<Stream> GetPictureAsync(Uri uri) => 
            await (await _httpClient.GetAsync(uri)).Content.ReadAsStreamAsync();
    }
}
