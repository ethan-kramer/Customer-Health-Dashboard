using System.Data.Common;
using CustomerHealthDashboardWebApi.Data;
using CustomerHealthDashboardWebApi.Dto.User;
using CustomerHealthDashboardWebApi.Dto.Testimonials;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using System.Xml.Linq;
using Microsoft.AspNetCore.DataProtection;
using CustomerHealthDashboardWebApi.Util;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CustomerHealthDashboardWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IConfiguration _configuration;
        private TestimonialTreeContext _dbContext;

        public UserController(IConfiguration configuration, ILogger<UserController> logger,
            TestimonialTreeContext dbContext)
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
            var dbResults = dbSet.Where(x => x.ParentId == parentUserId).ToList();

            foreach (var dbResult in dbResults)
            {
                //build the dtos here that you will send to the front end
                var userDto = GetUserInfoDto(dbResult);

                userDtos.Add(userDto);
            }

            return userDtos;
        }


        [HttpGet("/api/v1/data/{ActualUserID}/testimonialcount")] // not defining ActualUserIDs as INT like John bc they are null a lot
        public int GetUserTestimonialCount(int ActualUserID)
        {
            var testimonialCount = 0;

            var dbSet = _dbContext.Set<Testimonials>().DefaultIfEmpty().AsNoTracking();


            string query =
                " SELECT COUNT(Testimonial) as totalReviews" +
                " FROM Testimonials WHERE ActualUserID = " + ActualUserID.ToString();
            //" GROUP BY ActualUserID;";

            var dbResults = _dbContext.ExecuteQueryAsDictionary(query);

            foreach (Dictionary<string, object> dbResult in dbResults)
            {
                object value;
                dbResult.TryGetValue("totalReviews", out value);
                if (value != null)
                {
                    testimonialCount = (int)value;
                }
            }

            return testimonialCount;
        }

        // num testimonials by week for parent users
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

        // weekly average in progress
        // once we have weekly average and in the last week, we should be able to compare the two in javascript
        [HttpGet("/api/v1/data/{ActualUserID}/weeklyaverage")]
        public int GetWeeklyAverage(int ActualUserID)
        {
            /*
             * TODO - read and then remove comment
             * - when debugging the code, I inspected the value of `query` and noticed that it was malformed
             *   for instances it out put something like `WHERE ActualuserId = 3GROUP BY`. If you notice `3GROUP BY` is
             *   not valid sql.
             * - notice now that I placed a space at the beginning of each line of the query string parts so that it would
             *   ensure proper spacing and formatting.
             */ 
            string query =
                " WITH UserTestimonials AS (" +
                " SELECT ActualUserID, DATEPART(week, DATETIMESTAMP) AS SpecificWeek, COUNT(*) AS NumReviews" +
                " FROM TESTIMONIALS" +
                " WHERE ActualUserID = " + ActualUserID.ToString() + 
                " GROUP BY ActualUserID, DATEPART(week, DATETIMESTAMP)" +
                " ) " +
                " SELECT AVG(NumReviews) AS WeeklyAverage" +
                " FROM UserTestimonials;";

            // when the type returned does not match one of our defined data models such as User or Tesitimonial, then
            // we need to use this helper method that Jon White wrote to return a dynamic dictionary, which we can use 
            // to extract the calculated value
            var dbResults = _dbContext.ExecuteQueryAsDictionary(query); // return as a dictionary

            int weeklyAverage = -1;

            // there should only be one record here, so it should get the first record and then grab it's WeeklyAverage
            // output its value and then we return it later
            foreach (Dictionary<string, object> dbResult in dbResults)
            {
                object value;
                dbResult.TryGetValue("WeeklyAverage", out value);
                if (value != null)
                {
                    weeklyAverage = (int)value;
                }
            }
            
            return weeklyAverage;
        }

        /*
         * Calculating avg outside of query
         *
         */
        // public int GetWeeklyAverage(int ActualUserID)
        // {
        //     int weeklyAverage = 0;
        //
        //     string query =
        //         "SELECT ActualUserID, DATEPART(week, DATETIMESTAMP) AS SpecificWeek, COUNT(*) AS NumReviews " +
        //         "FROM TESTIMONIALS " +
        //         "WHERE ActualUserID = " + ActualUserID.ToString() +
        //         "GROUP BY DATEPART(week, DATETIMESTAMP), ActualUserID;";
        //
        //     var dbResults = _dbContext.Testimonials.FromSqlRaw(query);
        //
        //
        //     int weekCount = weeklyTestimonials.Select(record => record.Week).Distinct().Count();
        //
        //     int totalCount = weeklyTestimonials.Sum(record => record.TotalCount);
        //
        //     weeklyAverage = (int)Math.Round((double)totalCount / weekCount);
        //
        //     return weeklyAverage;
        // }


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
            //testimonialsDto.ActualUserID = dataTestimonials.ActualUserID;
            testimonialsDto.DateTimeStamp = dataTestimonials.DateTimeStamp;

            return testimonialsDto;
        }
    }
}