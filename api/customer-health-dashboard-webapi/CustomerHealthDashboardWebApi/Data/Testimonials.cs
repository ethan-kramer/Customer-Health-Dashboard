using System;
using System.Collections.Generic;

namespace CustomerHealthDashboardWebApi.Data
{
    public partial class Testimonials
    {
        public Testimonials()
        {
            InverseParent = new HashSet<Testimonials>();    // what is this
        }

        public int TestimonialID { get; set; }
        public string UserID{ get; set; }
        public string Signature { get; set; }
        public string Email { get; set; }
        public string Testimonial { get; set; }
        public string Comment { get; set; }
        public string TestimonialVideo { get; set; }
        public string DateTimeStamp { get; set; }   // should this be type: DateTime?
        public string ShowToPublic { get; set; }
        public string Rating { get; set; }
        public string Relationship { get; set; }
        public string FacebookLiked { get; set; }
        public string Source { get; set; }
        public string LastUpdated { get; set; }     // type: DateTime?
        public string OriginalTestimonial { get; set; }
        public string Deleted { get; set; }
        public string originalClientIP { get; set; }
        public string originalClientReferrer { get; set; }
        public string spamRating { get; set; }
        public string systemlsSpam { get; set; }
        public string clientlsSpam { get; set; }
        public string clientIsValid { get; set; }
        public string requestID { get; set; }
        public string FavoriteLevel { get; set; }
        public string Helpfulness { get; set; }
        public string lastUpdatedUsername { get; set; }
        public string TestimonialStatusID { get; set; }
        public int? oldtestimonialid { get; set; }
        public string ActualUserID { get; set; }        
        public string SignatureNVarChar { get; set; }
        public string ExternalSource { get; set; }
        public string ReScrape { get; set; }
        public string ThirdPartySiteDataID { get; set; }        // end of tuples

        public virtual Testimonials Parent { get; set; }    // should this be deleted or changed to something else?
        public virtual ICollection<Testimonials> InverseParent { get; set; }    // ""
    }
}
