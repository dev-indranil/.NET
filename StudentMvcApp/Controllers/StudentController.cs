using Microsoft.AspNetCore.Mvc;
using StudentMvcApp.Models;
using System.Text;
using System.Text.Json;

namespace StudentMvcApp.Controllers
{
    public class StudentController : Controller
    {
        private readonly HttpClient _client;
        private readonly string _apiBaseUrl = "https://localhost:7259/api/students";

        public StudentController()
        {
            _client = new HttpClient();
        }

        public async Task<IActionResult> Index()
        {
            var response = await _client.GetAsync(_apiBaseUrl);
            var json = await response.Content.ReadAsStringAsync();
            var students = JsonSerializer.Deserialize<List<Student>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return View(students);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Student student)
        {
            var json = JsonSerializer.Serialize(student);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync(_apiBaseUrl, content);

            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            return View(student);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var response = await _client.GetAsync($"{_apiBaseUrl}/{id}");
            var json = await response.Content.ReadAsStringAsync();
            var student = JsonSerializer.Deserialize<Student>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return View(student);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Student student)
        {
            var json = JsonSerializer.Serialize(student);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _client.PutAsync($"{_apiBaseUrl}/{student.Id}", content);

            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            return View(student);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var response = await _client.GetAsync($"{_apiBaseUrl}/{id}");
            var json = await response.Content.ReadAsStringAsync();
            var student = JsonSerializer.Deserialize<Student>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return View(student);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var response = await _client.DeleteAsync($"{_apiBaseUrl}/{id}");

            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            return RedirectToAction("Delete", new { id });
        }
    }
}
