using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace InMemory.Caching.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        readonly IMemoryCache _memoryCache;

        public ValuesController(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        //Without using absolute and sliding time

        [HttpGet("setName/{name}")]
        public void SetName(string name)
        {
            _memoryCache.Set("name", name);
        }

        [HttpGet("getName")]
        public string GetName()
        {
            //return _memoryCache.Get<string>("name");

            if(_memoryCache.TryGetValue<string>("name", out string name))
            {
                return name;
            }
            return "";
        }

        //With using absolute and sliding time

        [HttpGet("setDate")]
        public void SetDate()
        {
            _memoryCache.Set<DateTime>("date", DateTime.Now, options : new()
            {
                AbsoluteExpiration = DateTime.Now.AddSeconds(30),
                SlidingExpiration = TimeSpan.FromSeconds(5)
            });
        }

        [HttpGet("getDate")]
        public DateTime GetDate()
        {
            return _memoryCache.Get<DateTime>("date");
        }
    }
}
