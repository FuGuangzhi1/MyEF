using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MyEF;
using MyEF.Entities;
using MyEF.Test;

namespace MyEFTestApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TsetController : ControllerBase
    {
        private MyDBContext _DBContext;
        public TsetController()
        {
            _DBContext = myDBContext();
        }

        [HttpGet]
        public IEnumerable<Test_Table> Get()
        {
            return _DBContext.Where<Test_Table>(x => x.TsetInt == 5);
        }
        [HttpPost]
        public int Post()
        {
            _DBContext.Add<Test_Table>(new Test_Table
            {
                CreateTime = DateTime.Now,
                Id = Guid.NewGuid(),
                IsDeleted = false,
                TsetBool = true,
                TsetDateTime = DateTime.Now,
                TsetInt = 1,
                TsetString = "",
                UpdateTime = DateTime.Now,
            });
            return _DBContext.SaveChange();
        }
        [HttpPut]
        public void Put()
        {
            _DBContext.Update<Test_Table>(new Test_Table
            {
                CreateTime = DateTime.Now,
                Id = Guid.Parse("00000000-0000-0000-7BBA-08DA28D5AAE7"),
                IsDeleted = false,
                TsetBool = true,
                TsetDateTime = DateTime.Now,
                TsetInt = 1,
                TsetString = "测试修改数据",
                UpdateTime = DateTime.Now,
            });
            _DBContext.SaveChange();
        }
        [HttpDelete]
        public void Delete()
        {
            _DBContext.Delete<Test_Table>("00000000-0000-0000-7BBA-08DA28D5AAE7");
            _DBContext.SaveChange();
        }
        private MyDBContext myDBContext()
        {
            MyEF.Config.DbContextOptions options = new MyEF.Config.DbContextOptions();
            options = options.UseSqlSever("Data Source=127.0.0.1;Initial Catalog=StudyWebApi;User ID=sa;password=123456;");
            return new MyDBContext(options);
        }
    }
}
