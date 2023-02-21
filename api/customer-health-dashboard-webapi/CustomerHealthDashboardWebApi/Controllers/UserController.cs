using CustomerHealthDashboardWebApi.Data;
using CustomerHealthDashboardWebApi.Dto.User;
using CustomerHealthDashboardWebApi.Dto.Testimonials;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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


        
        [HttpGet("/api/v1/data/{ActualUserID}/testimonialcount")]                     // not defining ActualUserIDs as INT like John bc they are null a lot
        public List<TestimonialsDto> GetUserTestimonialCount(int ActualUserID)
        {
            var testimonialsDtos = new List<TestimonialsDto>();

            var dbSet = _dbContext.Set<Testimonials>().DefaultIfEmpty().AsNoTracking();


            string query = 
                "SELECT COUNT(Testimonial) as totalReviews" +
                "FROM Testimonials" +
                "GROUP BY ActualUserID;";

            var dbResults = _dbContext.Testimonials.FromSqlRaw(query, ActualUserID).ToList();

            foreach (var dbResult in dbResults)
            {
                //build the dtos here that you will send to the front end
                var testimonialsDto = GetTestimonialsDto(dbResult);
                testimonialsDtos.Add(testimonialsDto);
            }
            return testimonialsDtos;
        }

        // for potential weekly average: num testimonials by week for parent users
        [HttpGet("/api/v1/data/{ActualUserID}/weeklytestimonials")]
        public List<TestimonialsDto> GetWeeklyTestimonials(int ActualUserID)
        {
            var testimonialsDtos = new List<TestimonialsDto>();

            var dbSet = _dbContext.Set<Testimonials>().DefaultIfEmpty().AsNoTracking();


            string query = 
                "SELECT ActualUserID, count(*) as num_reviews" +
                "FROM TESTIMONIALS" +
                "GROUP BY DATEPART(week, DATETIMESTAMP), ActualUserID;";

            var dbResults = _dbContext.Testimonials.FromSqlRaw(query, ActualUserID).ToList();

            foreach (var dbResult in dbResults)
            {
                //build the dtos here that you will send to the front end
                var testimonialsDto = GetTestimonialsDto(dbResult);
                testimonialsDtos.Add(testimonialsDto);
            }
            return testimonialsDtos;
        }


        // getting all user information for any that had < 20 reviews in a given week
        [HttpGet("/api/v1/data/{ActualUserID}/badweeklytestimonials")]
        public List<TestimonialsDto> GetBadWeeklyCounts(int ActualUserID)
        {
            var testimonialsDtos = new List<TestimonialsDto>();

            var dbSet = _dbContext.Set<Testimonials>().DefaultIfEmpty().AsNoTracking();


            string query =
                "WITH CTE AS (" +
                    "SELECT" +
                        "ActualUserId," +
                        "DATEPART(week, DATETIMESTAMP) as [WeekNumber]," +
                        "COUNT(*) AS [NumberOfTestimonials]" +
                    "FROM Testimonials T" +
                    "GROUP BY ActualUserId, DATEPART(week, DateTimestamp)" +
                    ")" +
                    "SELECT UI.*" +
                "FROM CTE" +
                "LEFT JOIN UserInfo UI ON UI.UserID = CTE.ActualUserID" +
                "WHERE CTE.NumberOfTestimonials < 20;";

            var dbResults = _dbContext.Testimonials.FromSqlRaw(query, ActualUserID).ToList();

            foreach (var dbResult in dbResults)
            {
                //build the dtos here that you will send to the front end
                var testimonialsDto = GetTestimonialsDto(dbResult);
                testimonialsDtos.Add(testimonialsDto);
            }
            return testimonialsDtos;
        }



        // NEED TO INSERT MORE DUMMY DATA TO BE ABLE TO TRY AND TEST NUMTESTIMONIALS THIS WEEK



        // james' query
        [HttpGet("/api/v1/data/{ActualUserID}/jamesquery")]
        public List<TestimonialsDto> GetUserTestimonials(int ActualUserID)
        {
            var testimonialsDtos = new List<TestimonialsDto>();

            var dbSet = _dbContext.Set<Testimonials>().DefaultIfEmpty().AsNoTracking();

            
            string query = 
                "WITH CTE AS (" +
                "SELECT" +
                "ActualUserId," +
                "DATEPART(YEAR, DateTimeStamp) AS [Year]," +
                "DATEPART(WEEK, DateTimestamp) AS [Week]," +
                "COUNT(*) AS [NumberOfTestimonials]" +
                "FROM Testimonials T " +
                "WHERE DateTimeStamp > DATEADD(MONTH, -1, GETDATE())" +
                "GROUP BY ActualUserId, DATEPART(YEAR, DateTimeStamp), DATEPART(WEEK, DateTimestamp)" +
                ")" +
                "SELECT UI.*" +
                "FROM CTE" +
                "LEFT JOIN UserInfo UI ON UI.UserID = CTE.ActualUserID" +
                "WHERE CTE.NumberOfTestimonials > 5;";

            var dbResults = _dbContext.Testimonials.FromSqlRaw(query, ActualUserID).ToList();

            foreach (var dbResult in dbResults)
            {
                //build the dtos here that you will send to the front end
                var testimonialsDto = GetTestimonialsDto(dbResult);
                testimonialsDtos.Add(testimonialsDto);
            }
            return testimonialsDtos;
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


        // "not all code paths return a value"?
        private TestimonialsDto GetTestimonialsDto(Data.Testimonials dataTestimonials)
        {
            var testimonialsDto = new TestimonialsDto();
            testimonialsDto.ActualUserID = dataTestimonials.ActualUserID;
            testimonialsDto.DateTimeStamp = dataTestimonials.DateTimeStamp;
        }
    }
}
