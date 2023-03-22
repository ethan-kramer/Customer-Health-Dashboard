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
using Microsoft.AspNetCore.Components;
using Microsoft.Data.SqlClient;

namespace CustomerHealthDashboardWebApi.Controllers
{
    [ApiController]
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
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


        [HttpGet("/api/v1/data/{ActualUserID}/testimonialcount")]
        public int GetUserTestimonialCount(int ActualUserID)
        {
            var testimonialCount = 0;


            string query =
                " SELECT COUNT(*) as totalReviews" +
                " FROM Testimonials WHERE ActualUserID = " + ActualUserID.ToString();

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


        // IN PROGRESS
        // num surveys sent/received by week for specific user
        [HttpGet("/api/v1/data/{Username}/surveygraph")]
        public dynamic GetSurveysStats(string Username, [FromQuery(Name = "excludeZeros")] bool excludeZeros)
        {
            string baseQuery =
                " SELECT" +
                " surveyRequests.Username," +
                " DATEPART(YEAR, surveyRequests.DateTimeStamp) as [Year]," +
                " DATEPART(WEEK, surveyRequests.DateTimeStamp) AS [Week]," +
                " COUNT(surveyRequests.RequestID) AS [RequestsSent]," +
                " COUNT(surveyTaken.id) AS [RequestsCompleted]," +
                " CAST(COUNT(surveyTaken.id) AS FLOAT) / NULLIF(CAST(COUNT(surveyRequests.RequestID) AS FLOAT), 0) AS [CompletionPercentage]" +
                " FROM surveyRequests" +
                " LEFT JOIN surveyTaken ON surveyTaken.surveyRequestID = surveyRequests.RequestID" +
                " WHERE" +
                " surveyRequests.DateTimeStamp IS NOT NULL" +
                " AND" +
                " surveyRequests.DateTimeStamp > DATEADD(YEAR, -1, GETDATE())" +
                " AND" +
                " surveyRequests.DateTimeStamp < GETDATE()" +
                " AND" +
                " surveyRequests.Username = '" + Username.ToString() + "'";

            if (excludeZeros)
            {
                baseQuery += " AND CompletionPercentage != 0";
            }

            var query = baseQuery +
                        " GROUP BY" +
                        " surveyRequests.Username," +
                        " DATEPART(YEAR, surveyRequests.DateTimestamp)," +
                        " DATEPART(WEEK, surveyRequests.DATETIMESTAMP)" +
                        " ORDER BY" +
                        " DATEPART(YEAR, surveyRequests.DateTimestamp) ASC," +
                        " DATEPART(WEEK, surveyRequests.DATETIMESTAMP) ASC;";

            var dbResults = _dbContext.ExecuteQueryAsDictionary(query);

            return dbResults;
        }


        // num testimonials last week for specific user
        [HttpGet("/api/v1/data/{ActualUserID}/testimonialslastweek")]
        public int GetTestimonialsLastWeek(int ActualUserID)
        {
            int testimonialsLastWeek = -1;


            string query =
                " SELECT ActualUserID, COUNT(*) AS NumReviews, DATEPART(week, GETDATE()) AS ThisWeek, DATEPART(week, DATEADD(week, -1, GETDATE())) as LastWeek" +
                " FROM TESTIMONIALS" +
                " WHERE DATEPART(week, DATETIMESTAMP) = DATEPART(week, DATEADD(week, -1, GETDATE())) AND ActualUserID = " +
                ActualUserID.ToString() +
                " GROUP BY ActualUserID, DATEPART(week, DATETIMESTAMP);";

            var dbResults = _dbContext.ExecuteQueryAsDictionary(query); // return as a dictionary

            foreach (Dictionary<string, object> dbResult in dbResults)
            {
                object value;
                dbResult.TryGetValue("NumReviews", out value);
                if (value != null)
                {
                    testimonialsLastWeek = (int)value;
                }
            }

            return testimonialsLastWeek;
        }


        // once we have weekly average and in the last week, we should be able to compare the two in javascript
        [HttpGet("/api/v1/data/{ActualUserID}/weeklyaverage")]
        public int GetWeeklyAverage(int ActualUserID)
        {
            string query =
                " WITH UserTestimonials AS (" +
                " SELECT ActualUserID, DATEPART(week, DATETIMESTAMP) AS SpecificWeek, COUNT(*) AS NumReviews" +
                " FROM TESTIMONIALS" +
                " WHERE ActualUserID = " + ActualUserID.ToString() +
                " GROUP BY ActualUserID, DATEPART(week, DATETIMESTAMP)" +
                " ) " +
                " SELECT AVG(NumReviews) AS WeeklyAverage" +
                " FROM UserTestimonials;";

            var dbResults = _dbContext.ExecuteQueryAsDictionary(query); // return as a dictionary

            int weeklyAverage = -1;

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


        // average stars 
        [HttpGet("/api/v1/data/{ActualUserID}/averagerating")]
        public int GetAverageRating(int ActualUserID)
        {
            string query =
                " WITH userRatings AS (" +
                " SELECT ActualUserID, CAST(Rating AS int) as IntRating" +
                " FROM Testimonials" +
                " WHERE ActualUserID = " + ActualUserID.ToString() +
                " GROUP BY ActualUserID, Rating" +
                " )" +
                " SELECT AVG(IntRating) as AverageRating" +
                " From userRatings;";


            var dbResults = _dbContext.ExecuteQueryAsDictionary(query); // return as a dictionary

            int averageRating = -1;

            foreach (Dictionary<string, object> dbResult in dbResults)
            {
                object value;
                dbResult.TryGetValue("AverageRating", out value);
                if (value != null)
                {
                    averageRating = (int)value;
                }
            }

            return averageRating;
        }

        // getting all user information for any that had < 20 reviews in a given week
        [HttpGet("/api/v1/data/{ActualUserID}/badweeklytestimonials")]
        public List<TestimonialsDto> GetBadWeeklyCounts(int ActualUserID)
        {
            var testimonialsDtos = new List<TestimonialsDto>();

            var dbSet = _dbContext.Set<Testimonials>().DefaultIfEmpty().AsNoTracking();


            string query =
                " WITH CTE AS (" +
                " SELECT" +
                " ActualUserId," +
                " DATEPART(week, DATETIMESTAMP) as [WeekNumber]," +
                " COUNT(*) AS [NumberOfTestimonials]" +
                " FROM Testimonials T" +
                " GROUP BY ActualUserId, DATEPART(week, DateTimestamp)" +
                " )" +
                " SELECT UI.*" +
                " FROM CTE" +
                " LEFT JOIN UserInfo UI ON UI.UserID = CTE.ActualUserID" +
                " WHERE CTE.NumberOfTestimonials < 20;";

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