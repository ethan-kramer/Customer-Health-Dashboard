using System;
using System.Collections.Generic;

namespace CustomerHealthDashboardWebApi.Data
{
    public partial class Testimonials
    {
        public int TestimonialId { get; set; }
        public string UserId { get; set; }
        public string Signature { get; set; }
        public string Email { get; set; }
        public string Testimonial { get; set; }
        public string Comment { get; set; }
        public string TestimonialVideo { get; set; }
        public DateTime? DateTimeStamp { get; set; }
        public string ShowToPublic { get; set; }
        public string Rating { get; set; }
        public string Relationship { get; set; }
        public string FacebookLiked { get; set; }
        public string Source { get; set; }
        public DateTime? LastUpdated { get; set; }
        public string OriginalTestimonial { get; set; }
        public string Deleted { get; set; }
        public string OriginalClientIp { get; set; }
        public string OriginalClientReferrer { get; set; }
        public double? SpamRating { get; set; }
        public bool? SystemIsSpam { get; set; }
        public bool? ClientIsSpam { get; set; }
        public bool? ClientIsValid { get; set; }
        public int? RequestId { get; set; }
        public int? FavoriteLevel { get; set; }
        public int? Helpfulness { get; set; }
        public string LastUpdatedUsername { get; set; }
        public int? TestimonialStatusId { get; set; }
        public int? Oldtestimonialid { get; set; }
        public int? ActualUserId { get; set; }
        public string SignatureNvarChar { get; set; }
        public string ExternalSource { get; set; }
        public bool? ReScrape { get; set; }
        public int? ThirdPartySiteDataId { get; set; }

        public virtual UserInfo ActualUser { get; set; }
    }
}
