using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieWebApp.Models;
using System.Diagnostics;

namespace MovieWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly MovieDbContext _context;

        public HomeController(MovieDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Movie movie, IFormFile Video, IFormFile Picture)
        {
            //if (ModelState.IsValid)
            //{
                // 영상(mp4)과 사진(jpg)을 바이너리 데이터로 변환하여 모델에 저장
                if (Video != null && Video.Length > 0)
                {
                    using (var videoStream = new MemoryStream())
                    {
                        Video.CopyTo(videoStream);
                        movie.Video = videoStream.ToArray();
                    }
                }

                if (Picture != null && Picture.Length > 0)
                {
                    using (var pictureStream = new MemoryStream())
                    {
                        Picture.CopyTo(pictureStream);
                        movie.Picture = pictureStream.ToArray();
                    }
                }

                // 데이터베이스에 저장
                _context.Movie.Add(movie);
                _context.SaveChanges();

                return RedirectToAction("Index"); // 등록 후 리다이렉트
            //}

            //return View(movie); // 유효성 검사 오류 시 폼 다시 표시
        }
        public IActionResult Detail()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
