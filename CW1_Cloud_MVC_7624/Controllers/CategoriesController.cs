using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CW1_Cloud_MVC_7624.Data;
using CW1_Cloud_MVC_7624.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace CW1_Cloud_MVC_7624.Controllers
{
    public class CategoriesController : Controller
    {
        private const string BaseUrl = "http://ec2-18-224-8-169.us-east-2.compute.amazonaws.com/";
        private readonly HttpClient _client;

        public CategoriesController()
        {
            _client = new HttpClient { BaseAddress = new Uri(BaseUrl) };
        }
        // GET: Category
        public async Task<IActionResult> Index()
        {
            _client.DefaultRequestHeaders.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new
                MediaTypeWithQualityHeaderValue("application/json"));
            var res = await _client.GetAsync("api/Category");
            if (!res.IsSuccessStatusCode) return View();
            var prResponse = res.Content.ReadAsStringAsync().Result;
            return View(JsonConvert.DeserializeObject<List<Category>>(prResponse));
        }

        // GET: Category/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var category = await GetCategoryById(id);
            if (category == null) return NotFound();
            return View(category);
        }

        // GET: Category/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Category/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description")] Category category)
        {
            if (!ModelState.IsValid) return View();
            try
            {
                var rndId = new Random();
                category.Id = rndId.Next(100);
                //HTTP POST
                var postTask = await _client.PostAsJsonAsync<Category>("api/Category",
                    category);
                // postTask.Wait();
                // var result = postTask.Result;
                if (postTask.IsSuccessStatusCode) return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            return View();
        }

        // GET: Category/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var category = await GetCategoryById(id);
            if (category == null) return NotFound();
            return View(category);
        }

        // POST: Category/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description")] Category cat)
        {
            if (id != cat.Id) return NotFound();
            if (!ModelState.IsValid) return View();
            try
            {
                //HTTP POST
                var postTask = await _client.PutAsJsonAsync<Category>("api/Category/" + cat.Id,
                    cat);
                if (postTask.IsSuccessStatusCode) return RedirectToAction("Index");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryExists(cat.Id))
                {
                    return NotFound();
                }
                throw;
            }
            return View(cat);
        }

        // GET: Category/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var category = await GetCategoryById(id);
            if (category == null) return NotFound();
            return View(category);
        }

        // POST: Category/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var res = await _client.DeleteAsync("api/Category/" + id);
            ModelState.AddModelError("Err", "Server Error.");
            if (!res.IsSuccessStatusCode) return View();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoryExists(int id)
        {
            var cat = GetCategoryById(id);
            return id != cat.Id;
        }

        public async Task<Category> GetCategoryById(int id)
        {
            var res = await _client.GetAsync("api/Category/" + id);
            if (!res.IsSuccessStatusCode) return null;
            var prResponse = res.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<Category>(prResponse);
        }
    }
}