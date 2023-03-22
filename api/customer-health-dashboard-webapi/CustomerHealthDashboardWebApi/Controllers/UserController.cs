using CustomerHealthDashboardWebApi.Data;
using CustomerHealthDashboardWebApi.Dto.Testimonials;
using CustomerHealthDashboardWebApi.Dto.User;
using CustomerHealthDashboardWebApi.Util;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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


        [HttpGet("/api/v1/data/{UserID}/testimonialcount")]
        public int GetUserTestimonialCount(string UserID)
        {
            var testimonialCount = 0;


            string query =
                " SELECT COUNT(*) as totalReviews" +
                " FROM Testimonials WHERE UserID = " + UserID;

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

        
        [HttpGet("/api/v1/data/hometable")]
        public dynamic GetHomeTable([FromQuery(Name = "excludeZeros")] bool excludeZeros)
        {
            excludeZeros = true;
            string baseQuery =
            " WITH TestimonialStats AS (" +
            " SELECT" +
            " TestimonialRequests.UserID," +
            " DATEPART(YEAR, TestimonialRequests.DateTimeStamp) as [Year]," +
            " DATEPART(WEEK, TestimonialRequests.DateTimeStamp) AS [Week]," +
            " COUNT(TestimonialRequests.RequestID) AS [RequestsSent]," +
            " COUNT(Testimonials.TestimonialID) AS [RequestsCompleted]," +
            " CAST(COUNT(Testimonials.TestimonialID) AS FLOAT) / NULLIF(CAST(COUNT(TestimonialRequests.RequestID) AS FLOAT), 0) AS [CompletionPercentage]" +
            " FROM TestimonialRequests" +
            " LEFT JOIN Testimonials ON Testimonials.requestID = TestimonialRequests.RequestID" +
            " LEFT JOIN UserInfo ON TestimonialRequests.UserID = UserInfo.Username" +
            " WHERE" +
            " TestimonialRequests.DateTimeStamp IS NOT NULL" +
            " AND" +
            " TestimonialRequests.DateTimeStamp > DATEADD(YEAR, -1, GETDATE())" +
            " AND" +
            " TestimonialRequests.DateTimeStamp < GETDATE()" +
            " AND" +
            " UserInfo.Deleted IS NULL" +
            " GROUP BY" +
            " TestimonialRequests.UserID," +
            " DATEPART(YEAR, TestimonialRequests.DateTimestamp)," +
            " DATEPART(WEEK, TestimonialRequests.DATETIMESTAMP)" +
            " ) SELECT" +
            " UserID," +
            " AVG(CAST(RequestsSent AS FLOAT)) as AverageRequestsSent," +
            " AVG(CAST(RequestsCompleted AS FLOAT)) as AverageRequestsCompleted," +
            " AVG(CAST(CompletionPercentage AS FLOAT)) as CompletionPercentage" +
            " FROM TestimonialStats";

            if (excludeZeros)
            {
                baseQuery += " WHERE CompletionPercentage > 0";
            }

            var query = baseQuery +
                " GROUP BY" +
                " UserID" +
                " ORDER BY" +
                " CompletionPercentage ASC;";

            return _dbContext.ExecuteQueryAsDictionary(query);
        }


        // num surveys sent/received by week for specific user (graph info)
        [HttpGet("/api/v1/data/{Username}/surveygraph")]
        public dynamic GetSurveysStats(string Username, [FromQuery(Name = "excludeZeros")] bool excludeZeros)
        {
            string query =
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
                " surveyRequests.Username = '" + Username.ToString() + "'" +
                " GROUP BY" +
                " surveyRequests.Username," +
                " DATEPART(YEAR, surveyRequests.DateTimestamp)," +
                " DATEPART(WEEK, surveyRequests.DATETIMESTAMP)" +
                " ORDER BY" +
                " DATEPART(YEAR, surveyRequests.DateTimestamp) ASC," +
                " DATEPART(WEEK, surveyRequests.DATETIMESTAMP) ASC;";
 

            return  _dbContext.ExecuteQueryAsDictionary(query);
        }


        // num testimonials sent/received by week for specific user (graph info)
        [HttpGet("/api/v1/data/{Username}/testimonialgraph")]
        public dynamic GetTestimonialStats(string Username)
        {
            string query =
                " SELECT" +
                " TestimonialRequests.UserID," +
                " DATEPART(YEAR, TestimonialRequests.DateTimeStamp) as [Year]," +
                " DATEPART(WEEK, TestimonialRequests.DateTimeStamp) AS [Week]," +
                " COUNT(TestimonialRequests.RequestID) AS [RequestsSent]," +
                " COUNT(Testimonials.TestimonialID) AS [RequestsCompleted]," +
                " CAST(COUNT(Testimonials.TestimonialID) AS FLOAT) / NULLIF(CAST(COUNT(TestimonialRequests.RequestID) AS FLOAT), 0) AS [CompletionPercentage]" +
                " FROM TestimonialRequests" +
                " LEFT JOIN Testimonials ON Testimonials.requestID = TestimonialRequests.RequestID" +
                " WHERE" +
                " TestimonialRequests.DateTimeStamp IS NOT NULL" +
                " AND" +
                " TestimonialRequests.DateTimeStamp > DATEADD(YEAR, -1, GETDATE())" +
                " AND" +
                " TestimonialRequests.DateTimeStamp < GETDATE()" +
                " AND" +
                " TestimonialRequests.UserID = '" + Username.ToString() + "'" +
                " GROUP BY" +
                " TestimonialRequests.UserID," +
                " DATEPART(YEAR, TestimonialRequests.DateTimestamp)," +
                " DATEPART(WEEK, TestimonialRequests.DATETIMESTAMP)" +
                " ORDER BY" +
                " DATEPART(YEAR, TestimonialRequests.DateTimestamp) ASC," +
                " DATEPART(WEEK, TestimonialRequests.DATETIMESTAMP) ASC;";


            return _dbContext.ExecuteQueryAsDictionary(query);
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


        private UserInfoDto GetUserInfoDto(UserInfo dataUserInfo)
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
        private TestimonialsDto GetTestimonialsDto(Testimonials dataTestimonials)
        {
            var testimonialsDto = new TestimonialsDto();
            //testimonialsDto.ActualUserID = dataTestimonials.ActualUserID;
            testimonialsDto.DateTimeStamp = dataTestimonials.DateTimeStamp;

            return testimonialsDto;
        }
    }
}