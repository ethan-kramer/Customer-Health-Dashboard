namespace CustomerHealthDashboardWebApi.Dto.User
{
    public class UserInfoDto
    {
        public int UserId { get; set; }
        public string? Username { get; set; }
        public string? Image { get; set; }
        public string? Website { get; set; }
        public string? CompanyName { get; set; }
        public string? Name { get; set; }
        public string? Address1 { get; set; }
        public string? Address2 { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Zip { get; set; }
        public string? Country { get; set; }
        public string? Phone { get; set; }
        public string? Description { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? EditDate { get; set; }
        public string? ParentUser { get; set; }

        public string? BrandingImage { get; set; }
        public string? BrandingColor1 { get; set; }
        public string? BrandingBodyColor { get; set; }
        public string? BrandingColor2 { get; set; }
        public string? FacebookPage { get; set; }


        public string? Guid { get; set; }

        public string? BusinessCategory { get; set; }
        public string? Location { get; set; }


        public string? LicenceTitle { get; set; }
        public string? LicenceNumber { get; set; }
        public string? DisclaimerTitle { get; set; }
        public string? Disclaimer { get; set; }

        public string? CreatedBy { get; set; }

        public decimal? TimezoneOffset { get; set; }
        public bool? TimezoneObserveSavings { get; set; }

        public string? NameFirst { get; set; }
        public string? NameMiddle { get; set; }
        public string? NameLast { get; set; }

        public int? ParentId { get; set; }
        public string? VanityUrlSlug { get; set; }
    }
}
