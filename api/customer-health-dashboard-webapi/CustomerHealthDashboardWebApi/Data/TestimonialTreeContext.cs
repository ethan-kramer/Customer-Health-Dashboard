using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CustomerHealthDashboardWebApi.Data
{
    public partial class TestimonialTreeContext : DbContext
    {
        public virtual DbSet<UserInfo> UserInfo { get; set; }

        public TestimonialTreeContext(DbContextOptions<TestimonialTreeContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserInfo>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.HasIndex(e => e.ParentUser, "ParentUser+")
                    .HasFillFactor(80);

                entity.HasIndex(e => e.ParentUser, "dbo_UserInfo__ParentUser_NCWI");

                entity.HasIndex(e => e.Deleted, "idxDeleted_inc_username_parentuser")
                    .HasFillFactor(80);

                entity.HasIndex(e => new { e.ParentUser, e.CreatedBy }, "idxParentUser+")
                    .HasFillFactor(80);

                entity.HasIndex(e => new { e.ReportActivityWeekly, e.ReportActivityMonthly }, "idxReports")
                    .HasFillFactor(80);

                entity.HasIndex(e => e.Username, "idxUserName")
                    .HasFillFactor(80);

                entity.HasIndex(e => new { e.ParentUser, e.ExternalId }, "idx_user_ext_id_notnull")
                    .IsUnique()
                    .HasFilter("([external_id] IS NOT NULL)");

                entity.HasIndex(e => e.VanityUrlSlug, "idx_vanity_slug_notnull")
                    .IsUnique()
                    .HasFilter("([vanity_url_slug] IS NOT NULL)");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.Address1).HasMaxLength(200);

                entity.Property(e => e.Address2).HasMaxLength(200);

                entity.Property(e => e.BrandingBodyColor)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.BrandingColor1)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.BrandingColor2)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.BrandingImage)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.BrandingImageFooter)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("BrandingImage_Footer");

                entity.Property(e => e.BusinessCategory)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.City)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Claimed)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.CollectionBrandingStyleSheet)
                    .HasMaxLength(4096)
                    .IsUnicode(false)
                    .HasColumnName("collectionBrandingStyleSheet");

                entity.Property(e => e.CompanyName)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Country).HasMaxLength(100);

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(16)
                    .IsUnicode(false)
                    .HasColumnName("createdBy");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Deleted)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Disclaimer)
                    .HasMaxLength(2048)
                    .HasColumnName("disclaimer");

                entity.Property(e => e.DisclaimerTitle)
                    .HasMaxLength(256)
                    .HasColumnName("disclaimerTitle");

                entity.Property(e => e.EditDate).HasColumnType("datetime");

                entity.Property(e => e.ExternalId)
                    .HasMaxLength(256)
                    .IsUnicode(false)
                    .HasColumnName("external_id");

                entity.Property(e => e.FacebookPage)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Guid)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("GUID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.Image)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.LicenceNumber)
                    .HasMaxLength(1024)
                    .HasColumnName("licenceNumber");

                entity.Property(e => e.LicenceTitle)
                    .HasMaxLength(256)
                    .HasColumnName("licenceTitle");

                entity.Property(e => e.Location)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LogoutRedirectUrl).HasColumnName("logoutRedirectUrl");

                entity.Property(e => e.Name)
                    .HasMaxLength(3072)
                    .IsUnicode(false);

                entity.Property(e => e.NameFirst)
                    .HasMaxLength(1024)
                    .HasColumnName("nameFirst");

                entity.Property(e => e.NameLast)
                    .HasMaxLength(1024)
                    .HasColumnName("nameLast");

                entity.Property(e => e.NameMiddle)
                    .HasMaxLength(1024)
                    .HasColumnName("nameMiddle");

                entity.Property(e => e.OverrideSubscription).HasColumnName("overrideSubscription");

                entity.Property(e => e.Paid)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ParentId).HasColumnName("ParentID");

                entity.Property(e => e.ParentUser)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ReportActivityMonthly).HasColumnName("reportActivityMonthly");

                entity.Property(e => e.ReportActivityWeekly).HasColumnName("reportActivityWeekly");

                entity.Property(e => e.ResellersResellerId)
                    .HasColumnName("ResellersResellerID")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.SettingCollectionCaptchaEnabled)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Setting_Collection_CaptchaEnabled");

                entity.Property(e => e.SettingCollectionDisclaimer).HasColumnName("Setting_collectionDisclaimer");

                entity.Property(e => e.SettingCollectionDisplayCommentsField)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("Setting_Collection_DisplayCommentsField");

                entity.Property(e => e.SettingTestimonialCategoriesEnabled)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Setting_TestimonialCategories_Enabled");

                entity.Property(e => e.SettingTestimonialCategoriesLayout)
                    .HasMaxLength(16)
                    .IsUnicode(false)
                    .HasColumnName("Setting_TestimonialCategories_Layout");

                entity.Property(e => e.SettingTestimonialCategoriesShowAdmin)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Setting_TestimonialCategories_ShowAdmin");

                entity.Property(e => e.SettingTestimonialCategoriesShowOnCollectionForm)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Setting_TestimonialCategories_ShowOnCollectionForm");

                entity.Property(e => e.SettingTestimonialCategoriesShowParentCategories)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Setting_TestimonialCategories_ShowParentCategories");

                entity.Property(e => e.SettingTestimonialCategoriesTitle)
                    .HasMaxLength(128)
                    .HasColumnName("Setting_TestimonialCategories_Title");

                entity.Property(e => e.SettingTestimonialIntroText)
                    .HasMaxLength(2048)
                    .HasColumnName("Setting_Testimonial_introText");

                entity.Property(e => e.SettingTestimonialRequestEmailSubject)
                    .HasMaxLength(1024)
                    .HasColumnName("Setting_Testimonial_requestEmailSubject");

                entity.Property(e => e.SettingsAddTestimonials)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Settings_AddTestimonials");

                entity.Property(e => e.SettingsAdminUserWidgets)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Settings_AdminUserWidgets");

                entity.Property(e => e.SettingsAllowTestimonialExport)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Settings_AllowTestimonialExport");

                entity.Property(e => e.SettingsCollectionCustomBranding)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Settings_Collection_CustomBranding");

                entity.Property(e => e.SettingsCollectionDefaultRating)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("settings_collection_defaultRating");

                entity.Property(e => e.SettingsCollectionRequireEmailAddress)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Settings_Collection_RequireEmailAddress");

                entity.Property(e => e.SettingsCollectionShowOnMenu)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Settings_Collection_ShowOnMenu");

                entity.Property(e => e.SettingsCollectionStarStyle)
                    .HasMaxLength(128)
                    .IsUnicode(false)
                    .HasColumnName("Settings_CollectionStarStyle");

                entity.Property(e => e.SettingsDeleteTestimonials)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Settings_DeleteTestimonials");

                entity.Property(e => e.SettingsEditTestimonials)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Settings_EditTestimonials");

                entity.Property(e => e.SettingsEmailRequestTemplateName)
                    .HasMaxLength(128)
                    .IsUnicode(false)
                    .HasColumnName("Settings_Email_requestTemplateName");

                entity.Property(e => e.SettingsEmailSurveyRequestTemplateName)
                    .HasMaxLength(128)
                    .IsUnicode(false)
                    .HasColumnName("Settings_Email_SurveyRequestTemplateName");

                entity.Property(e => e.SettingsEmailTestimonialNotificationTemplateName)
                    .HasMaxLength(128)
                    .IsUnicode(false)
                    .HasColumnName("Settings_Email_TestimonialNotificationTemplateName");

                entity.Property(e => e.SettingsEmailUseAutoLoginLink)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Settings_Email_UseAutoLoginLink");

                entity.Property(e => e.SettingsEmailUseBrandedTestimonialNotification)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Settings_Email_UseBrandedTestimonialNotification");

                entity.Property(e => e.SettingsHidePoweredBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Settings_HidePoweredBy");

                entity.Property(e => e.SettingsImportResendTimespan)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("Settings_importResendTimespan");

                entity.Property(e => e.SettingsImportResendValue).HasColumnName("Settings_importResendValue");

                entity.Property(e => e.SettingsNotificationLocationManager).HasColumnName("settings_notification_locationManager");

                entity.Property(e => e.SettingsNotificationRoot).HasColumnName("settings_notification_root");

                entity.Property(e => e.SettingsNotificationUser).HasColumnName("settings_notification_user");

                entity.Property(e => e.SettingsNotifyAdditionalMax)
                    .HasColumnName("Settings_Notify_Additional_Max")
                    .HasDefaultValueSql("((5))");

                entity.Property(e => e.SettingsNotifyAdditionalMin)
                    .HasColumnName("Settings_Notify_Additional_Min")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.SettingsNotifyManagerMax)
                    .HasColumnName("Settings_Notify_Manager_Max")
                    .HasDefaultValueSql("((5))");

                entity.Property(e => e.SettingsNotifyManagerMin)
                    .HasColumnName("Settings_Notify_Manager_Min")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.SettingsNotifyRootMax)
                    .HasColumnName("Settings_Notify_Root_Max")
                    .HasDefaultValueSql("((5))");

                entity.Property(e => e.SettingsNotifyRootMin)
                    .HasColumnName("Settings_Notify_Root_Min")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.SettingsNotifyUserMax)
                    .HasColumnName("Settings_Notify_User_Max")
                    .HasDefaultValueSql("((5))");

                entity.Property(e => e.SettingsNotifyUserMin)
                    .HasColumnName("Settings_Notify_User_Min")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.SettingsShareTestimonials)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Settings_ShareTestimonials");

                entity.Property(e => e.SettingsShowManageOnLogin)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Settings_ShowManageOnLogin");

                entity.Property(e => e.SettingsTestimonialsAdminParent)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Settings_Testimonials_AdminParent");

                entity.Property(e => e.SettingsTestimonialsAllowLocationManagerToEdit)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Settings_Testimonials_AllowLocationManagerToEdit");

                entity.Property(e => e.SettingsTestimonialsAllowReviewedFlagging)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Settings_Testimonials_AllowReviewedFlagging");

                entity.Property(e => e.SettingsTestimonialsAssignToOtherUsers)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Settings_Testimonials_AssignToOtherUsers");

                entity.Property(e => e.SettingsTestimonialsAssignToWidgets)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Settings_Testimonials_AssignToWidgets");

                entity.Property(e => e.SettingsTestimonialsAutoShare)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("Settings_Testimonials_AutoShare");

                entity.Property(e => e.SettingsTestimonialsAutoShareApps)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("Settings_Testimonials_AutoShareApps");

                entity.Property(e => e.SettingsTestimonialsAutoShareMarketing)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("Settings_Testimonials_AutoShareMarketing");

                entity.Property(e => e.SettingsTestimonialsAutoShareMinimum)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .HasColumnName("Settings_Testimonials_AutoShare_Minimum");

                entity.Property(e => e.SettingsTestimonialsAutoShareWidgets)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("Settings_Testimonials_AutoShareWidgets");

                entity.Property(e => e.SettingsTestimonialsCollectionMethod)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Settings_Testimonials_CollectionMethod");

                entity.Property(e => e.SettingsTestimonialsEditContent)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Settings_Testimonials_EditContent");

                entity.Property(e => e.SettingsTestimonialsEnableReviewProcess)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Settings_Testimonials_EnableReviewProcess");

                entity.Property(e => e.SettingsTestimonialsFormShowPoweredBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Settings_Testimonials_Form_ShowPoweredBy");

                entity.Property(e => e.SettingsTestimonialsMakePublic)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Settings_Testimonials_MakePublic");

                entity.Property(e => e.SettingsTestimonialsSecondaryShareRequests)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Settings_Testimonials_SecondaryShareRequests");

                entity.Property(e => e.SettingsTestimonialsShareIntegratedSitesPublic)
                    .HasMaxLength(128)
                    .IsUnicode(false)
                    .HasColumnName("Settings_Testimonials_shareIntegratedSitesPublic");

                entity.Property(e => e.SettingsTestimonialsShareIntegratedSitesUser)
                    .HasMaxLength(128)
                    .IsUnicode(false)
                    .HasColumnName("Settings_Testimonials_shareIntegratedSitesUser");

                entity.Property(e => e.SettingsUserAllowAddressUpdating)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Settings_User_AllowAddressUpdating");

                entity.Property(e => e.SettingsUserAllowAutomatedRequests)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("Settings_User_AllowAutomatedRequests");

                entity.Property(e => e.SettingsUserAllowCategoryUpdating)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Settings_User_AllowCategoryUpdating");

                entity.Property(e => e.SettingsUserAllowCompanyNameUpdating)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Settings_User_AllowCompanyNameUpdating");

                entity.Property(e => e.SettingsUserAllowDescriptionUpdating)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Settings_User_AllowDescriptionUpdating");

                entity.Property(e => e.SettingsUserAllowDropOffSites)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("Settings_User_AllowDropOffSites")
                    .HasDefaultValueSql("('False')");

                entity.Property(e => e.SettingsUserAllowEmailUpdating)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Settings_User_AllowEmailUpdating");

                entity.Property(e => e.SettingsUserAllowEnterpriseUserAdmin)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Settings_User_AllowEnterpriseUserAdmin");

                entity.Property(e => e.SettingsUserAllowFacebookUrlupdating)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Settings_User_AllowFacebookURLUpdating");

                entity.Property(e => e.SettingsUserAllowLicenceNumberUpdating)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Settings_User_AllowLicenceNumberUpdating");

                entity.Property(e => e.SettingsUserAllowLocationUpdating)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Settings_User_AllowLocationUpdating");

                entity.Property(e => e.SettingsUserAllowNameUpdating)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Settings_User_AllowNameUpdating");

                entity.Property(e => e.SettingsUserAllowPasswordUpdating)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Settings_User_AllowPasswordUpdating");

                entity.Property(e => e.SettingsUserAllowPhoneUpdating)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Settings_User_AllowPhoneUpdating");

                entity.Property(e => e.SettingsUserAllowProfileEditing)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Settings_User_AllowProfileEditing");

                entity.Property(e => e.SettingsUserAllowProfileImageUpdating)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Settings_User_AllowProfileImageUpdating");

                entity.Property(e => e.SettingsUserAllowProfileSectionUpdating)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Settings_User_AllowProfileSectionUpdating");

                entity.Property(e => e.SettingsUserAllowTeamUpdating).HasColumnName("Settings_User_AllowTeamUpdating");

                entity.Property(e => e.SettingsUserAllowTimezoneUpdating).HasColumnName("Settings_User_AllowTimezoneUpdating");

                entity.Property(e => e.SettingsUserAllowWebsiteUpdating)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Settings_User_AllowWebsiteUpdating");

                entity.Property(e => e.SettingsUserIsManager)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Settings_User_IsManager");

                entity.Property(e => e.SettingsWidgetsAllowFacebookTabAdmin)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Settings_Widgets_AllowFacebookTabAdmin");

                entity.Property(e => e.SettingsWidgetsAllowWidgetAdmin)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Settings_Widgets_AllowWidgetAdmin");

                entity.Property(e => e.SettingsWidgetsDisplayParentIfNoLocal)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("Settings_Widgets_DisplayParentIfNoLocal");

                entity.Property(e => e.SoftwareAccessReviewTree)
                    .HasColumnName("softwareAccessReviewTree")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SoftwareAccessSurvey).HasColumnName("softwareAccessSurvey");

                entity.Property(e => e.State)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.TimezoneObserveSavings).HasColumnName("timezoneObserveSavings");

                entity.Property(e => e.TimezoneOffset)
                    .HasColumnType("decimal(4, 2)")
                    .HasColumnName("timezoneOffset");

                entity.Property(e => e.UseCustomSettings)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UserManagementRedirectUrl).HasColumnName("userManagementRedirectUrl");

                entity.Property(e => e.UserSource)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.Username)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.VanityUrlSlug)
                    .HasMaxLength(128)
                    .HasColumnName("vanity_url_slug");

                entity.Property(e => e.Website)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Zip)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Parent)
                    .WithMany(p => p.InverseParent)
                    .HasForeignKey(d => d.ParentId)
                    .HasConstraintName("UserInfo_UserInfo_UserID_fk");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
