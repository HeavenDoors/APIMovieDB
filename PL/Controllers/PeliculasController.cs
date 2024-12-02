
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;
using System.Text.Json.Nodes;

namespace PL.Controllers
{
    public class PeliculasController : Controller
    {
        string apiToken = "Bearer eyJhbGciOiJIUzI1NiJ9.eyJhdWQiOiIyN2Y1ZmFhOGVkZGQwNzIwMWI5NTU3M2RjYTNmODgxYSIsIm5iZiI6MTczMzAyMTcxOS41MTIsInN1YiI6IjY3NGJkMDE3YTc0ZjhmNGI0ZDlhOTI4NCIsInNjb3BlcyI6WyJhcGlfcmVhZCJdLCJ2ZXJzaW9uIjoxfQ.hPTHEKjYYUvs2mI3-3jEJsvwe1DrYfNQYj9SZFRLuk0";

        public IActionResult GetAll()
        {
            return View();
        }

        public JsonResult Get()
        {
            string url = "https://api.themoviedb.org/3/movie/popular?language=es&page=1";

            ML.Result result = new ML.Result();
            result.Objects = new List<object>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);

                client.DefaultRequestHeaders.Add("Authorization", apiToken);
               var responseTask = client.GetAsync("");

                responseTask.Wait();

                var resultTask = responseTask.Result;

                if (resultTask.IsSuccessStatusCode)
                {
                    var readTask = resultTask.Content.ReadAsAsync<ML.Result>();

                    readTask.Wait();

                    foreach (var item in readTask.Result.results)
                    {
                        ML.Peliculas peliculas = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Peliculas>(item.ToString());

                        result.Objects.Add(peliculas);
                    }

                    result.Correct = true;
                }
            }

            return Json(result);
        }

        public JsonResult AddFavorite(int id)
        {
            ML.Result result = new ML.Result();
            ML.AddModel agregar = new ML.AddModel();

            agregar.favorite = true;
            agregar.media_type = "movie";
            agregar.media_id = id;

            try
            {
                string url = "https://api.themoviedb.org/3/account/21662636/favorite";

                string json = Newtonsoft.Json.JsonConvert.SerializeObject(agregar);

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url);
                    client.DefaultRequestHeaders.Add("Authorization", apiToken);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    var responseTask = client.PostAsync("", content);

                    responseTask.Wait();

                    var resultTask = responseTask.Result;

                    if (resultTask.IsSuccessStatusCode) 
                    {
                        result.Correct = true;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }

            return Json(result);
        }

        public IActionResult GetFavoritos()
        {
            return View();
        }

        public JsonResult GetFavoritosJson()
        {
            string url = "https://api.themoviedb.org/3/account/21662636/favorite/movies?language=es&page=1&sort_by=created_at.asc";

            ML.Result result = new ML.Result();
            result.Objects = new List<object>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);

                client.DefaultRequestHeaders.Add("Authorization", apiToken);
                var responseTask = client.GetAsync("");

                responseTask.Wait();

                var resultTask = responseTask.Result;

                if (resultTask.IsSuccessStatusCode)
                {
                    var readTask = resultTask.Content.ReadAsAsync<ML.Result>();

                    readTask.Wait();

                    foreach (var item in readTask.Result.results)
                    {
                        ML.Peliculas peliculas = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Peliculas>(item.ToString());

                        result.Objects.Add(peliculas);
                    }

                    result.Correct = true;
                }
            }

            return Json(result);
        }


        public JsonResult DeleteFavorite(int id)
        {
            ML.Result result = new ML.Result();
            ML.AddModel agregar = new ML.AddModel();

            agregar.favorite = false;
            agregar.media_type = "movie";
            agregar.media_id = id;

            try
            {
                string url = "https://api.themoviedb.org/3/account/21662636/favorite";

                string json = Newtonsoft.Json.JsonConvert.SerializeObject(agregar);

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url);
                    client.DefaultRequestHeaders.Add("Authorization", apiToken);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    var responseTask = client.PostAsync("", content);

                    responseTask.Wait();

                    var resultTask = responseTask.Result;

                    if (resultTask.IsSuccessStatusCode)
                    {
                        result.Correct = true;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }

            return Json(result);
        }
    }
}
