﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Authorization.Model.Product;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Authorization.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]

    public class CartController : ControllerBase
    {
        Uri baseAddress = new Uri("https://localhost:44385");
        HttpClient client;

        public CartController()
        {
            client = new HttpClient();
            client.BaseAddress = baseAddress;
        }

        [HttpGet("{Var}")]
        public IActionResult Get(string Var)
        {
            HttpResponseMessage response = null;
            try
            {
                response = client.GetAsync(client.BaseAddress + "api/Cart/" + Var).Result;
            }
            catch
            {
                return BadRequest("Micro Not working");
            }
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                return Ok(data);
            }
            return BadRequest();
        }

        [HttpPost("{username}")]
        public IActionResult Post(string username, [FromBody] ProductItem obj)
        {
            string data = JsonConvert.SerializeObject(obj);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

            HttpResponseMessage response = null;
            try
            {
                response = client.PostAsync(client.BaseAddress + "api/Cart/" + username, content).Result;
            }
            catch
            {
                return BadRequest("Micro Not working");
            }

            if (response.IsSuccessStatusCode)
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            HttpResponseMessage response = null;
            try
            {
                response = client.DeleteAsync(client.BaseAddress + "api/Cart/" + id).Result;
            }
            catch
            {
                return BadRequest("Micro Not working");
            }
            if (response.IsSuccessStatusCode)
                return Ok();
            return BadRequest();
        }

    }
}