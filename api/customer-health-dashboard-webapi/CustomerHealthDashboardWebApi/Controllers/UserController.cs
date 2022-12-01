using CustomerHealthDashboardWebApi.Data;
using CustomerHealthDashboardWebApi.Dto.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Xml.Linq;

namespace CustomerHealthDashboardWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {

        private readonly ILogger<UserController> _logger;
        private readonly IConfiguration _configuration;
        private TestimonialTreeContext _dbContext;

        public UserController(IConfiguration configuration, ILogger<UserController> logger, TestimonialTreeContext dbContext)
        {
            _configuration = configuration;
            _logger = logger;

            _dbContext = dbContext;

        }

        
        [HttpGet("/api/v1/parentusers")]
        public List<UserInfoDto> GetParentUsers()
        {
            var userDtos = new List<UserInfoDto>();

            var dbSet = _dbContext.Set<UserInfo>().DefaultIfEmpty().AsNoTracking();
            var dbResults = dbSet.Where(x => x.ParentUser == null || x.ParentUser == "").ToList();

            foreach (var dbResult in dbResults)
            {
                //build the dtos here that you will send to the front end
                var userDto = GetUserInfoDto(dbResult);
                userDtos.Add(userDto);
            }
            return userDtos;
        }

        [HttpGet("/api/v1/parentusers/{parentUserId:INT}/childusers")]
        public List<UserInfoDto> GetChildUsers(int parentUserId)
        {
            var userDtos = new List<UserInfoDto>();

            var dbSet = _dbContext.Set<UserInfo>().DefaultIfEmpty().AsNoTracking();
            var dbResults = dbSet.Where(x => x.ParentId == parentUserId ).ToList();

            foreach (var dbResult in dbResults)
            {
                //build the dtos here that you will send to the front end
                var userDto = GetUserInfoDto(dbResult);

                userDtos.Add(userDto);
            }
            return userDtos;
        }

        private UserInfoDto GetUserInfoDto(Data.UserInfo dataUserInfo)
        {
            //This maps the db model to the dto you will send to the front end
            //this allows you to add properties that may not be stored in the database that the front end needs
            var userDto = new UserInfoDto();
            userDto.Username = dataUserInfo.Username;
            userDto.UserId = dataUserInfo.UserId;
            userDto.CompanyName = dataUserInfo.CompanyName;
            //etc....
            //Finish the mapping here....

            return userDto;
        }
    }
}
