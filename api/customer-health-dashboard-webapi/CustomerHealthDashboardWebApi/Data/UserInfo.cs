using System;
using System.Collections.Generic;

namespace CustomerHealthDashboardWebApi.Data
{
    public partial class UserInfo
    {
        public UserInfo()
        {
            InverseParent = new HashSet<UserInfo>();
        }

        public int UserId { get; set; }
        public string Username { get; set; }
        public string Image { get; set; }
        public string Website { get; set; }
        public string CompanyName { get; set; }
        public string Name { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public string Description { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? EditDate { get; set; }
        public string ParentUser { get; set; }
        public string SettingsAddTestimonials { get; set; }
        public string SettingsEditTestimonials { get; set; }
        public string SettingsDeleteTestimonials { get; set; }
        public string SettingsShareTestimonials { get; set; }
        public string SettingsAdminUserWidgets { get; set; }
        public string BrandingImage { get; set; }
        public string BrandingColor1 { get; set; }
        public string BrandingBodyColor { get; set; }
        public string BrandingColor2 { get; set; }
        public string FacebookPage { get; set; }
        public int? Product { get; set; }
        public string Paid { get; set; }
        public string SettingsHidePoweredBy { get; set; }
        public string SettingsTestimonialsMakePublic { get; set; }
        public string SettingsTestimonialsAssignToWidgets { get; set; }
        public string SettingsTestimonialsAssignToOtherUsers { get; set; }
        public string SettingsTestimonialsEditContent { get; set; }
        public string SettingsTestimonialsAllowReviewedFlagging { get; set; }
        public string SettingsTestimonialsEnableReviewProcess { get; set; }
        public string SettingsTestimonialsSecondaryShareRequests { get; set; }
        public string SettingsWidgetsAllowWidgetAdmin { get; set; }
        public string SettingsWidgetsAllowFacebookTabAdmin { get; set; }
        public string SettingsWidgetsDisplayParentIfNoLocal { get; set; }
        public string SettingsCollectionCustomBranding { get; set; }
        public string SettingsCollectionShowOnMenu { get; set; }
        public string SettingsCollectionRequireEmailAddress { get; set; }
        public string SettingsUserAllowProfileEditing { get; set; }
        public string SettingsUserAllowEnterpriseUserAdmin { get; set; }
        public string UseCustomSettings { get; set; }
        public string SettingsShowManageOnLogin { get; set; }
        public string SettingsTestimonialsAdminParent { get; set; }
        public string Guid { get; set; }
        public string SettingsAllowTestimonialExport { get; set; }
        public string SettingsEmailRequestTemplateName { get; set; }
        public string SettingsEmailUseBrandedTestimonialNotification { get; set; }
        public string SettingsEmailUseAutoLoginLink { get; set; }
        public string Deleted { get; set; }
        public string SettingsTestimonialsCollectionMethod { get; set; }
        public string SettingsTestimonialsFormShowPoweredBy { get; set; }
        public string SettingCollectionCaptchaEnabled { get; set; }
        public string SettingCollectionDisplayCommentsField { get; set; }
        public string SettingsCollectionStarStyle { get; set; }
        public string BusinessCategory { get; set; }
        public string Location { get; set; }
        public string SettingTestimonialCategoriesShowAdmin { get; set; }
        public string SettingTestimonialCategoriesShowParentCategories { get; set; }
        public string SettingTestimonialCategoriesEnabled { get; set; }
        public string SettingTestimonialCategoriesShowOnCollectionForm { get; set; }
        public string BrandingImageFooter { get; set; }
        public string SettingTestimonialCategoriesLayout { get; set; }
        public string SettingTestimonialCategoriesTitle { get; set; }
        public string SettingsUserAllowProfileImageUpdating { get; set; }
        public string SettingsUserAllowAddressUpdating { get; set; }
        public string SettingsUserAllowEmailUpdating { get; set; }
        public string SettingsUserAllowPasswordUpdating { get; set; }
        public string SettingsUserAllowWebsiteUpdating { get; set; }
        public string SettingsUserAllowFacebookUrlupdating { get; set; }
        public string SettingsUserAllowNameUpdating { get; set; }
        public string SettingsUserAllowCompanyNameUpdating { get; set; }
        public string SettingsUserAllowCategoryUpdating { get; set; }
        public string SettingsUserAllowLocationUpdating { get; set; }
        public string SettingsUserAllowPhoneUpdating { get; set; }
        public string SettingsUserAllowDescriptionUpdating { get; set; }
        public string SettingsUserAllowLicenceNumberUpdating { get; set; }
        public string SettingsUserAllowProfileSectionUpdating { get; set; }
        public string CollectionBrandingStyleSheet { get; set; }
        public string SettingsTestimonialsAllowLocationManagerToEdit { get; set; }
        public string SettingsTestimonialsAutoShare { get; set; }
        public string SettingsTestimonialsAutoShareMinimum { get; set; }
        public string SettingsTestimonialsAutoShareMarketing { get; set; }
        public string SettingsTestimonialsAutoShareWidgets { get; set; }
        public string SettingsTestimonialsAutoShareApps { get; set; }
        public string SettingsEmailSurveyRequestTemplateName { get; set; }
        public string SettingsEmailTestimonialNotificationTemplateName { get; set; }
        public int? SettingsImportResendValue { get; set; }
        public string SettingsImportResendTimespan { get; set; }
        public string SettingsUserIsManager { get; set; }
        public string SettingTestimonialIntroText { get; set; }
        public string LicenceTitle { get; set; }
        public string LicenceNumber { get; set; }
        public string DisclaimerTitle { get; set; }
        public string Disclaimer { get; set; }
        public string SettingTestimonialRequestEmailSubject { get; set; }
        public string CreatedBy { get; set; }
        public string SettingCollectionDisclaimer { get; set; }
        public string SettingsUserAllowDropOffSites { get; set; }
        public string SettingsTestimonialsShareIntegratedSitesUser { get; set; }
        public string SettingsTestimonialsShareIntegratedSitesPublic { get; set; }
        public bool? SettingsNotificationUser { get; set; }
        public bool? SettingsNotificationLocationManager { get; set; }
        public bool? SettingsNotificationRoot { get; set; }
        public string SettingsCollectionDefaultRating { get; set; }
        public string SettingsUserAllowAutomatedRequests { get; set; }
        public decimal? TimezoneOffset { get; set; }
        public bool? TimezoneObserveSavings { get; set; }
        public bool? ReportActivityWeekly { get; set; }
        public bool? ReportActivityMonthly { get; set; }
        public bool? SoftwareAccessReviewTree { get; set; }
        public bool? SoftwareAccessSurvey { get; set; }
        public string NameFirst { get; set; }
        public string NameMiddle { get; set; }
        public string NameLast { get; set; }
        public bool? SettingsUserAllowTeamUpdating { get; set; }
        public bool? SettingsUserAllowTimezoneUpdating { get; set; }
        public int? ParentId { get; set; }
        public bool AppWalkThrough { get; set; }
        public string UserSource { get; set; }
        public int SettingsNotifyRootMin { get; set; }
        public int SettingsNotifyRootMax { get; set; }
        public int SettingsNotifyManagerMin { get; set; }
        public int SettingsNotifyManagerMax { get; set; }
        public int SettingsNotifyUserMin { get; set; }
        public int SettingsNotifyUserMax { get; set; }
        public int SettingsNotifyAdditionalMin { get; set; }
        public int SettingsNotifyAdditionalMax { get; set; }
        public int? ResellersResellerId { get; set; }
        public string SsoToken { get; set; }
        public string LogoutRedirectUrl { get; set; }
        public string UserManagementRedirectUrl { get; set; }
        public bool? Claimed { get; set; }
        public string VanityUrlSlug { get; set; }
        public string ExternalId { get; set; }
        public bool OverrideSubscription { get; set; }

        public virtual UserInfo Parent { get; set; }
        public virtual ICollection<UserInfo> InverseParent { get; set; }
    }
}
