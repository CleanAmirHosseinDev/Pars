
using Microsoft.EntityFrameworkCore;
using ParsKyanCrm.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ParsKyanCrm.Domain.Contexts
{
    //Scaffold-DbContext "Server=77.238.123.197;Database=ParsKyanCrmDB;user id=pars;password=Pars@10155;Integrated Security=false;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Entities -NoPluralize
    //namespace ParsKyanCrm.Domain.Entities    
    public interface IDataBaseContext
    {

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////                

        DbSet<AboutUs> AboutUs { get; set; }
        DbSet<Activity> Activity { get; set; }
        DbSet<BoardOfDirectors> BoardOfDirectors { get; set; }
        DbSet<CaptchaCodes> CaptchaCodes { get; set; }
        DbSet<CareerOpportunities> CareerOpportunities { get; set; }
        DbSet<City> City { get; set; }
        DbSet<Companies> Companies { get; set; }
        DbSet<Contract> Contract { get; set; }
        DbSet<ContractAndFinancialDocuments> ContractAndFinancialDocuments { get; set; }
        DbSet<CorporateGovernance> CorporateGovernance { get; set; }
        DbSet<CorporateGovernanceDetail> CorporateGovernanceDetail { get; set; }
        DbSet<Customers> Customers { get; set; }
        DbSet<LevelStepSetting> LevelStepSetting { get; set; }
        DbSet<LicensesAndHonors> LicensesAndHonors { get; set; }
        DbSet<ManagementSystem> ManagementSystem { get; set; }
        DbSet<ManagerOfParsKyan> ManagerOfParsKyan { get; set; }
        DbSet<NewsAndContent> NewsAndContent { get; set; }
        DbSet<RankingCalculationResults> RankingCalculationResults { get; set; }

        DbSet<DataFromAnswers> DataFromAnswers { get; set; }
        DbSet<RankingOfCompanies> RankingOfCompanies { get; set; }
        DbSet<RequestForRating> RequestForRating { get; set; }
        DbSet<RequestReferences> RequestReferences { get; set; }
        DbSet<Roles> Roles { get; set; }
        DbSet<ServiceFee> ServiceFee { get; set; }
        DbSet<State> State { get; set; }
        DbSet<SystemSeting> SystemSeting { get; set; }
        DbSet<UserRoles> UserRoles { get; set; }
        DbSet<Users> Users { get; set; }

        DbSet<DataFormQuestions> DataFormQuestions { get; set; }
        DbSet<DataForms> DataForms { get; set; }
        DbSet<DataFormAnswerTables> DataFormAnswerTables { get; set; }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        int SaveChanges(bool acceptAllChangesOnSuccess);
        int SaveChanges();
        Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = new CancellationToken());
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken());

    }

    public class DataBaseContext : DbContext, IDataBaseContext
    {

        public DataBaseContext(DbContextOptions options) : base(options)
        {
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public virtual DbSet<DataFormQuestions> DataFormQuestions { get; set; }
        public virtual DbSet<DataForms> DataForms { get; set; }
        public virtual DbSet<DataFormAnswerTables> DataFormAnswerTables { get; set; }

        public virtual DbSet<DataFromAnswers> DataFromAnswers { get; set; }

        public virtual DbSet<AboutUs> AboutUs { get; set; }
        public virtual DbSet<Activity> Activity { get; set; }
        public virtual DbSet<BoardOfDirectors> BoardOfDirectors { get; set; }
        public virtual DbSet<CaptchaCodes> CaptchaCodes { get; set; }
        public virtual DbSet<CareerOpportunities> CareerOpportunities { get; set; }
        public virtual DbSet<City> City { get; set; }
        public virtual DbSet<Companies> Companies { get; set; }
        public virtual DbSet<Contract> Contract { get; set; }
        public virtual DbSet<ContractAndFinancialDocuments> ContractAndFinancialDocuments { get; set; }
        public virtual DbSet<CorporateGovernance> CorporateGovernance { get; set; }
        public virtual DbSet<CorporateGovernanceDetail> CorporateGovernanceDetail { get; set; }
        public virtual DbSet<Customers> Customers { get; set; }
        public virtual DbSet<LevelStepSetting> LevelStepSetting { get; set; }
        public virtual DbSet<LicensesAndHonors> LicensesAndHonors { get; set; }
        public virtual DbSet<ManagementSystem> ManagementSystem { get; set; }
        public virtual DbSet<ManagerOfParsKyan> ManagerOfParsKyan { get; set; }
        public virtual DbSet<NewsAndContent> NewsAndContent { get; set; }
        public virtual DbSet<RankingCalculationResults> RankingCalculationResults { get; set; }
        public virtual DbSet<RankingOfCompanies> RankingOfCompanies { get; set; }
        public virtual DbSet<RequestForRating> RequestForRating { get; set; }
        public virtual DbSet<RequestReferences> RequestReferences { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<ServiceFee> ServiceFee { get; set; }
        public virtual DbSet<State> State { get; set; }
        public virtual DbSet<SystemSeting> SystemSeting { get; set; }
        public virtual DbSet<UserRoles> UserRoles { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DataFromAnswers>(entity =>
            {
                entity.HasKey(e => e.AnswerId)
                    .HasName("PK_DataFromAnswer");

                entity.Property(e => e.AnswerId).HasColumnName("AnswerID");

                entity.Property(e => e.Answer).HasMaxLength(50);

                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

                entity.Property(e => e.DataFormQuestionId).HasColumnName("DataFormQuestionID");

                entity.Property(e => e.FormId).HasColumnName("FormID");
            });

            modelBuilder.Entity<DataFormQuestions>(entity =>
            {
                entity.HasKey(e => e.DataFormQuestionId)
                    .HasName("PK_DataForm");

                entity.Property(e => e.DataFormQuestionId).HasColumnName("DataFormQuestionID");

                entity.Property(e => e.QuestionName).HasMaxLength(50);

                entity.Property(e => e.QuestionText).HasMaxLength(50);

                entity.Property(e => e.QuestionType).HasMaxLength(50);
            });

            modelBuilder.Entity<DataForms>(entity =>
            {
                entity.HasKey(e => e.FormId)
                    .HasName("PK_Forms");

                entity.Property(e => e.FormTitle).HasMaxLength(50);
            });

            modelBuilder.Entity<DataFormAnswerTables>(entity =>
            {
                entity.HasKey(e => e.AnswerTableId)
                    .HasName("PK_DataFromAnswer");

                entity.Property(e => e.AnswerTableId)                    
                    .HasColumnName("AnswerTableId");

                entity.Property(e => e.Answer1).HasMaxLength(50);

                entity.Property(e=>e.FileName1).HasMaxLength(50);
                entity.Property(e => e.FileName2).HasMaxLength(50);

                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

                entity.Property(e => e.FormId).HasColumnName("FormID");
            });

            modelBuilder.Entity<AboutUs>(entity =>
            {
                entity.Property(e => e.AboutUsId).HasColumnName("AboutUsID");

                entity.Property(e => e.AboutUscontent).HasColumnName("AboutUSContent");

                entity.Property(e => e.Address).HasMaxLength(250);

                entity.Property(e => e.CompanyName).HasMaxLength(250);

                entity.Property(e => e.DateOfSaveorEdit).HasColumnType("datetime");

                entity.Property(e => e.Email).HasMaxLength(100);

                entity.Property(e => e.Facebook).HasMaxLength(250);

                entity.Property(e => e.FaxNumber).HasMaxLength(15);

                entity.Property(e => e.Instagram).HasMaxLength(250);

                entity.Property(e => e.Mobile1).HasMaxLength(15);

                entity.Property(e => e.Mobile2).HasMaxLength(15);

                entity.Property(e => e.Moto1).HasMaxLength(250);

                entity.Property(e => e.Moto2).HasMaxLength(250);

                entity.Property(e => e.Moto3).HasMaxLength(250);

                entity.Property(e => e.Moto4).HasMaxLength(250);

                entity.Property(e => e.Moto5).HasMaxLength(250);

                entity.Property(e => e.Subject).HasMaxLength(250);

                entity.Property(e => e.Tel1).HasMaxLength(15);

                entity.Property(e => e.Tel2).HasMaxLength(15);

                entity.Property(e => e.Tel3).HasMaxLength(15);

                entity.Property(e => e.Tel4).HasMaxLength(15);

                entity.Property(e => e.Tel5).HasMaxLength(15);

                entity.Property(e => e.Telegram).HasMaxLength(250);

                entity.Property(e => e.Whatsapp).HasMaxLength(250);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AboutUs)
                    .HasForeignKey(d => d.Userid)
                    .HasConstraintName("FK_AboutUs_Users");
            });

            modelBuilder.Entity<Activity>(entity =>
            {
                entity.Property(e => e.Picture1).HasMaxLength(250);

                entity.Property(e => e.Picture2).HasMaxLength(250);

                entity.HasOne(d => d.ActivityTitleNavigation)
                    .WithMany(p => p.Activity)
                    .HasForeignKey(d => d.ActivityTitle)
                    .HasConstraintName("FK_Activity_SystemSeting");
            });

            modelBuilder.Entity<BoardOfDirectors>(entity =>
            {
                entity.HasComment("جدول اعضای هیات مدیره");

                entity.Property(e => e.BoardOfDirectorsId).HasColumnName("BoardOfDirectorsID");

                entity.Property(e => e.AcademicDegreePicture)
                    .HasMaxLength(250)
                    .HasComment("عکس مدرک تحصیلی");

                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

                entity.Property(e => e.IsMemeberOfBoard).HasComment("عضو هیات مدیره است ؟");

                entity.Property(e => e.MemberEductionId)
                    .HasColumnName("MemberEductionID")
                    .HasComment("مدرک تحصیلی");

                entity.Property(e => e.MemberName).HasMaxLength(100);

                entity.Property(e => e.MemberPostId)
                    .HasColumnName("MemberPostID")
                    .HasComment("پست سازمانی");

                entity.Property(e => e.OwnershipCount).HasComment("تعداد سهام");

                entity.Property(e => e.OwnershipPercentage).HasComment("درصد سهام");

                entity.Property(e => e.PictureOfEducationCourse)
                    .HasMaxLength(250)
                    .HasComment("مدرک تحصیلی");

                entity.Property(e => e.PictureOfExperince)
                    .HasMaxLength(250)
                    .HasComment("تجربه کاری");

                entity.Property(e => e.PlaceOfTraining).HasComment("systemseting = 137");

                entity.Property(e => e.SaveDate).HasColumnType("datetime");

                entity.Property(e => e.TitelCourses).HasMaxLength(150);

                entity.Property(e => e.UniversityId)
                    .HasColumnName("UniversityID")
                    .HasComment("محل تحصیل");

                entity.HasOne(d => d.MemberEduction)
                    .WithMany(p => p.BoardOfDirectorsMemberEduction)
                    .HasForeignKey(d => d.MemberEductionId)
                    .HasConstraintName("FK_BoardOfDirectors_SystemSeting1");

                entity.HasOne(d => d.MemberPost)
                    .WithMany(p => p.BoardOfDirectorsMemberPost)
                    .HasForeignKey(d => d.MemberPostId)
                    .HasConstraintName("FK_BoardOfDirectors_SystemSeting");

                entity.HasOne(d => d.University)
                    .WithMany(p => p.BoardOfDirectorsUniversity)
                    .HasForeignKey(d => d.UniversityId)
                    .HasConstraintName("FK_BoardOfDirectors_SystemSeting2");
            });

            modelBuilder.Entity<CaptchaCodes>(entity =>
            {
                entity.Property(e => e.Code).HasMaxLength(10);

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.Guid).HasMaxLength(36);
            });

            modelBuilder.Entity<CareerOpportunities>(entity =>
            {
                entity.Property(e => e.CareerOpportunitiesId).HasColumnName("CareerOpportunitiesID");

                entity.Property(e => e.Address).HasMaxLength(50);

                entity.Property(e => e.Brithday).HasColumnType("datetime");

                entity.Property(e => e.CityId).HasColumnName("CityID");

                entity.Property(e => e.Education).HasMaxLength(250);

                entity.Property(e => e.EducationLevel).HasMaxLength(250);

                entity.Property(e => e.Family).HasMaxLength(50);

                entity.Property(e => e.Mobile).HasMaxLength(50);

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.NationalCode).HasMaxLength(50);

                entity.Property(e => e.Postion).HasMaxLength(250);

                entity.Property(e => e.ResumeFile).HasMaxLength(250);

                entity.Property(e => e.SaveDate).HasColumnType("datetime");

                entity.Property(e => e.Tel).HasMaxLength(50);
            });

            modelBuilder.Entity<City>(entity =>
            {
                entity.Property(e => e.CityId).HasColumnName("CityID");

                entity.Property(e => e.CityName).HasMaxLength(50);

                entity.Property(e => e.StateId).HasColumnName("StateID");

                entity.HasOne(d => d.State)
                    .WithMany(p => p.City)
                    .HasForeignKey(d => d.StateId)
                    .HasConstraintName("FK_City_State");
            });

            modelBuilder.Entity<Companies>(entity =>
            {
                entity.HasComment("جدول شرکتها");

                entity.Property(e => e.CompaniesId).HasColumnName("CompaniesID");

                entity.Property(e => e.CompanyGroupId).HasColumnName("CompanyGroupID");

                entity.Property(e => e.CompanyName)
                    .HasMaxLength(250)
                    .HasComment("نام شرکت");

                entity.Property(e => e.KindOfCompany).HasComment("نوع شرکت");

                entity.HasOne(d => d.CompanyGroup)
                    .WithMany(p => p.CompaniesCompanyGroup)
                    .HasForeignKey(d => d.CompanyGroupId)
                    .HasConstraintName("FK_Companies_SystemSeting1");

                entity.HasOne(d => d.KindOfCompanyNavigation)
                    .WithMany(p => p.CompaniesKindOfCompanyNavigation)
                    .HasForeignKey(d => d.KindOfCompany)
                    .HasConstraintName("FK_Companies_SystemSeting");
            });

            modelBuilder.Entity<Contract>(entity =>
            {
                entity.Property(e => e.ContractId).HasColumnName("ContractID");

                entity.HasOne(d => d.KinfOfRequestNavigation)
                    .WithMany(p => p.Contract)
                    .HasForeignKey(d => d.KinfOfRequest)
                    .HasConstraintName("FK_Contract_SystemSeting");
            });

            modelBuilder.Entity<ContractAndFinancialDocuments>(entity =>
            {
                entity.HasKey(e => e.FinancialId);

                entity.HasComment("مدارک قرارداد و رسید پرداخت بانکی");

                entity.Property(e => e.FinancialId).HasColumnName("FinancialID");

                entity.Property(e => e.ContractDocument)
                    .HasMaxLength(200)
                    .HasComment("آدرس قراداد امضا شده و بارگزاری شده");                

                entity.Property(e => e.FinancialDocument)
                    .HasMaxLength(200)
                    .HasComment("آدرس فایل رسید پرداخت");

                entity.Property(e => e.SaveDate).HasColumnType("datetime");

            });

            modelBuilder.Entity<CorporateGovernance>(entity =>
            {
                entity.Property(e => e.CompanyWebSite)
                    .HasMaxLength(100)
                    .HasComment("آدرس وب سایت شرکت");

                entity.Property(e => e.CrmSoftwareContract).HasMaxLength(250);

                entity.Property(e => e.DeductionTaxAccount)
                    .HasMaxLength(250)
                    .HasComment("مفاصا حساب مالیاتی");

                entity.Property(e => e.HaveAuditCommittee).HasComment("آیا کمیته حسابرسی داره ؟");

                entity.Property(e => e.HaveRepresentative).HasComment("نمایندگی داخلی یا خارجی دارید ؟");

                entity.Property(e => e.HighProductKnowledge).HasComment("اگر محصول دارای دانش بالای هست ؟");

                entity.Property(e => e.InovationFile).HasMaxLength(250);

                entity.Property(e => e.LetterOfCommendation)
                    .HasMaxLength(250)
                    .HasColumnName("letterOfCommendation")
                    .HasComment("تقدیرنامه از مشتری یا نمایندگی");

                entity.Property(e => e.OrganazationChart)
                    .HasMaxLength(250)
                    .HasComment("چارت سازمانی");

                entity.Property(e => e.OrganizationalDuties)
                    .HasMaxLength(250)
                    .HasComment("شرح وظایف پرسنل");

                entity.Property(e => e.Proceedings)
                    .HasMaxLength(250)
                    .HasComment("صورتجلسه کمیته حسابرسی");

                entity.Property(e => e.RepresentativeFile)
                    .HasMaxLength(250)
                    .HasComment("تصویر نامه نمایندگی ");

                entity.Property(e => e.RiskManagementGuidelines)
                    .HasMaxLength(250)
                    .HasComment("دستورالعمل مدیریت ریسک");

                entity.Property(e => e.TransactionRegulations)
                    .HasMaxLength(250)
                    .HasComment("آیین نامه معاملات");

                                    
            });

            modelBuilder.Entity<CorporateGovernanceDetail>(entity =>
            {
                entity.Property(e => e.DegreeOfEducation).HasComment("مدرک تحصیلی از جدول سیستم ستینگ");

                entity.Property(e => e.NameOfAuditCommittee)
                    .HasMaxLength(250)
                    .HasComment("نام و نام عضو کمیته حسابرسی");

                entity.Property(e => e.ProductCode).HasMaxLength(50);

                entity.Property(e => e.ProductName).HasMaxLength(250);

                entity.Property(e => e.WorkExperience).HasComment("تجربه کاری به ماه");

            });

            modelBuilder.Entity<Customers>(entity =>
            {
                entity.HasKey(e => e.CustomerId)
                    .HasName("PK__Customer__A4AE64B8A99FBB50");

                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

                entity.Property(e => e.AddressCompany).HasComment("آدرس شرکت");

                entity.Property(e => e.AgentMobile)
                    .HasMaxLength(11)
                    .HasComment("موبایل نماینده");

                entity.Property(e => e.AgentName)
                    .HasMaxLength(50)
                    .HasComment("نام رابط و نماینده شرکت");

                entity.Property(e => e.AmountOsLastSales)
                    .HasColumnType("money")
                    .HasComment("مبلغ کل فروش اظهار شده");

                entity.Property(e => e.AuditedFinancialStatements)
                    .HasMaxLength(250)
                    .HasComment("آخرین صورتحسابهای مالی حسابرسی شده");

                entity.Property(e => e.AuthenticateCode).HasMaxLength(50);

                entity.Property(e => e.CeoMobile)
                    .HasMaxLength(11)
                    .HasComment("موبایل مدیر عامل");

                entity.Property(e => e.CeoName)
                    .HasMaxLength(50)
                    .HasComment("نام و نام خانوادگی مدیر عامل");

                entity.Property(e => e.CeoNationalCode).HasMaxLength(11);

                entity.Property(e => e.CityId).HasColumnName("CityID");

                entity.Property(e => e.CompanyName)
                    .HasMaxLength(50)
                    .HasComment("نام شرکت");

                entity.Property(e => e.CountOfPersonal).HasComment("تعداد پرسنل شرکت");

                entity.Property(e => e.EconomicCode)
                    .HasMaxLength(50)
                    .HasComment("شماره ثبت");

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.HowGetKnowCompanyId).HasColumnName("HowGetKnowCompanyID");

                entity.Property(e => e.Ip).HasMaxLength(50);

                entity.Property(e => e.KindOfCompanyId)
                    .HasColumnName("KindOfCompanyID")
                    .HasComment("نوع شرکت");

                entity.Property(e => e.LastAuditingTaxList)
                    .HasMaxLength(250)
                    .HasComment("لیست آخرین اظهارنامه مالیاتی");

                entity.Property(e => e.LastChangeOfficialNewspaper)
                    .HasMaxLength(250)
                    .HasComment("آخرین تغییرات روزنامه رسمی");

                entity.Property(e => e.LastInsuranceList)
                    .HasMaxLength(250)
                    .HasComment("آخرین لیست بیمه");

                entity.Property(e => e.NamesAuthorizedSignatories).HasComment("اسامی امضاکنندگان مجاز");

                entity.Property(e => e.NationalCode)
                    .HasMaxLength(50)
                    .HasComment("شناسه ملی  شرکت");

                entity.Property(e => e.OfficialNewspaper)
                    .HasMaxLength(250)
                    .HasComment("روزنامه رسمی");

                entity.Property(e => e.PostalCode)
                    .HasMaxLength(50)
                    .HasComment("کد پستی");

                entity.Property(e => e.SaveDate).HasColumnType("datetime");

                entity.Property(e => e.Statute)
                    .HasMaxLength(250)
                    .HasComment("اساسنامه");

                entity.Property(e => e.Tel)
                    .HasMaxLength(11)
                    .HasComment("تلفن شرکت");

                entity.Property(e => e.TypeServiceRequestedId).HasColumnName("TypeServiceRequestedID");

                entity.HasOne(d => d.City)
                    .WithMany(p => p.Customers)
                    .HasForeignKey(d => d.CityId)
                    .HasConstraintName("FK_Customers_City");

                entity.HasOne(d => d.HowGetKnowCompany)
                    .WithMany(p => p.CustomersHowGetKnowCompany)
                    .HasForeignKey(d => d.HowGetKnowCompanyId)
                    .HasConstraintName("FK_Customers_SystemSeting2");

                entity.HasOne(d => d.KindOfCompany)
                    .WithMany(p => p.CustomersKindOfCompany)
                    .HasForeignKey(d => d.KindOfCompanyId)
                    .HasConstraintName("FK_Customers_SystemSeting");

                entity.HasOne(d => d.TypeServiceRequested)
                    .WithMany(p => p.CustomersTypeServiceRequested)
                    .HasForeignKey(d => d.TypeServiceRequestedId)
                    .HasConstraintName("FK_Customers_SystemSeting1");
            });            

            modelBuilder.Entity<LevelStepSetting>(entity =>
            {
                entity.HasKey(e => e.LevelStepSettingIndexId)
                    .HasName("PK_ProcessSteps");

                entity.Property(e => e.LevelStepSettingIndexId)
                    .ValueGeneratedNever()
                    .HasColumnName("LevelStepSettingIndexID");

                entity.Property(e => e.AccessRoleName).HasMaxLength(50);

                entity.Property(e => e.DestLevelStepIndex)
                    .HasMaxLength(50)
                    .HasComment("مقصد هایی که میتوان به آن ارجاع داد");

                entity.Property(e => e.DestLevelStepIndexButton).HasMaxLength(100);

                entity.Property(e => e.KindOfRequest).HasComment("نوع درخواست");

                entity.Property(e => e.LevelStepAccessRole)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasComment("یوزرهای که به این لول استپ دسترسی دارند");

                entity.Property(e => e.LevelStepStatus).HasMaxLength(100);

                entity.Property(e => e.SmsContent).HasMaxLength(150);

                entity.Property(e => e.SmsType).HasComment("0= Customer , 1= ParsKeyan");
            });

            modelBuilder.Entity<LicensesAndHonors>(entity =>
            {
                entity.Property(e => e.LicensesAndHonorsId).HasColumnName("LicensesAndHonorsID");

                entity.Property(e => e.Picture).HasMaxLength(250);

                entity.Property(e => e.SaveOrEditDate).HasColumnType("datetime");

                entity.Property(e => e.Title).HasMaxLength(150);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.LicensesAndHonors)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_LicensesAndHonors_Users");
            });

            modelBuilder.Entity<ManagementSystem>(entity =>
            {
                entity.Property(e => e.Awards)
                    .HasMaxLength(250)
                    .HasComment("جوایز");

                entity.Property(e => e.AwardsFile)
                    .HasMaxLength(250)
                    .HasComment("فایل ضمیمه جوایز");

                entity.Property(e => e.CertificateAndPermitAndStandard)
                    .HasMaxLength(250)
                    .HasComment("نام گواهینامه، مجوز، تاییدیه یا استاندارد");

                entity.Property(e => e.CertificateAndPermitAndStandardFile)
                    .HasMaxLength(250)
                    .HasComment("فایل نام گواهینامه، مجوز، تاییدیه یا استاندارد");

                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

                entity.Property(e => e.DateOfExporter)
                    .HasColumnType("datetime")
                    .HasComment("تاریخ صادر");

                entity.Property(e => e.ExpireDate).HasColumnType("datetime");

                entity.Property(e => e.ExporterReferenceForAwards)
                    .HasMaxLength(250)
                    .HasComment("مرجع صادر کننده");

                entity.Property(e => e.ExporterReferenceForCertificate)
                    .HasMaxLength(250)
                    .HasComment("مرجع صادرکننده");


                entity.Property(e => e.GuidelinesAndRegulations)
                    .HasMaxLength(250)
                    .HasComment("دستورالعمل ها و آیین نامه ها");

                entity.Property(e => e.GuidelinesAndRegulationsFile)
                    .HasMaxLength(250)
                    .HasComment("فایل پیوست دستورالعمل ها و آیین نامه ها");

                entity.Property(e => e.IsGuideLineOrProcess).HasComment("صفر یعنی دستورالعمل و آیین نامه و یک یعنی روش اجرایی");

                entity.Property(e => e.RequestId).HasColumnName("RequestID");

                entity.Property(e => e.Status).HasComment("صفر0 یعنی هنوز به سمت ارزیاب ارسال نشده و یک یعنی ارسال شده");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.ManagementSystem)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK_ManagementSystem_Customers");

                entity.HasOne(d => d.Request)
                    .WithMany(p => p.ManagementSystem)
                    .HasForeignKey(d => d.RequestId)
                    .HasConstraintName("FK_ManagementSystem_RequestForRating");
            });

            modelBuilder.Entity<ManagerOfParsKyan>(entity =>
            {
                entity.HasKey(e => e.ManagersId);

                entity.Property(e => e.ManagersId).HasColumnName("ManagersID");

                entity.Property(e => e.EmailAddress).HasMaxLength(150);

                entity.Property(e => e.NameOfManager).HasMaxLength(150);

                entity.Property(e => e.Picture).HasMaxLength(250);

                entity.Property(e => e.PositionId)
                    .HasColumnName("PositionID")
                    .HasComment("systemseting = 1");

                entity.Property(e => e.ResumeFile).HasMaxLength(250);

                entity.Property(e => e.SaveAndEditDate).HasColumnType("datetime");

                entity.Property(e => e.TitleId)
                    .HasColumnName("TitleID")
                    .HasComment("systemseting = 142");

                entity.Property(e => e.TwitterId).HasMaxLength(150);

                entity.Property(e => e.Userid).HasColumnName("userid");

                entity.HasOne(d => d.Position)
                    .WithMany(p => p.ManagerOfParsKyanPosition)
                    .HasForeignKey(d => d.PositionId)
                    .HasConstraintName("FK_ManagerOfParsKyan_SystemSeting");

                entity.HasOne(d => d.Title)
                    .WithMany(p => p.ManagerOfParsKyanTitle)
                    .HasForeignKey(d => d.TitleId)
                    .HasConstraintName("FK_ManagerOfParsKyan_SystemSeting1");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.ManagerOfParsKyan)
                    .HasForeignKey(d => d.Userid)
                    .HasConstraintName("FK_ManagerOfParsKyan_ManagerOfParsKyan");
            });

            modelBuilder.Entity<NewsAndContent>(entity =>
            {
                entity.HasKey(e => e.ContentId);

                entity.HasComment("اخبار و مطالب سایت");

                entity.Property(e => e.ContentId).HasComment("اخبار سایت");

                entity.Property(e => e.Body).HasComment("متن اصلی خبر");

                entity.Property(e => e.ContentPic).HasMaxLength(50);

                entity.Property(e => e.DateSave)
                    .HasColumnType("datetime")
                    .HasComment("تاریخ خبر");

                entity.Property(e => e.KindOfContent).HasComment("نوع خبر یا مطلب");

                entity.Property(e => e.Title)
                    .HasMaxLength(100)
                    .HasComment("عنوان خبر");

                entity.Property(e => e.UserId).HasComment("کاربر ثبت کننده");

                entity.HasOne(d => d.KindOfContentNavigation)
                    .WithMany(p => p.NewsAndContent)
                    .HasForeignKey(d => d.KindOfContent)
                    .HasConstraintName("FK_NewsAndContent_SystemSeting");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.NewsAndContent)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_NewsAndContent_Users");
            });            

            modelBuilder.Entity<RankingCalculationResults>(entity =>
            {
                entity.HasNoKey();

                entity.HasComment("جدول نتایج محاسبات رتبه بندی");

                entity.Property(e => e.CalcId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("CalcID");

                entity.Property(e => e.ChangeByUser).HasComment("کاربر تغییر دهنده یا ثبت کننده");

                entity.Property(e => e.ChangeDate)
                    .HasColumnType("datetime")
                    .HasComment("تاریخ ثبت یا تغییر");

                entity.Property(e => e.IndexId)
                    .HasColumnName("IndexID")
                    .HasComment("شناسه شاخص");

                entity.Property(e => e.RequestId).HasColumnName("RequestID");

                entity.Property(e => e.Score).HasComment("نمره یا امتیاز");

                entity.Property(e => e.SubDomainid).HasComment("شناسه زیر حوزه");

                entity.Property(e => e.Weight).HasComment("وزن");

                entity.HasOne(d => d.Customer)
                    .WithMany()
                    .HasForeignKey(d => d.Customerid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RankingCalculationResults_Customers");

                entity.HasOne(d => d.Index)
                    .WithMany()
                    .HasForeignKey(d => d.IndexId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RankingCalculationResults_SystemSeting1");

                entity.HasOne(d => d.Request)
                    .WithMany()
                    .HasForeignKey(d => d.RequestId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RankingCalculationResults_RequestForReating");

                entity.HasOne(d => d.SubDomain)
                    .WithMany()
                    .HasForeignKey(d => d.SubDomainid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RankingCalculationResults_SystemSeting");
            });

            modelBuilder.Entity<RankingOfCompanies>(entity =>
            {
                entity.HasKey(e => e.RankingId);

                entity.HasComment("جدول رتبه بندی شرکتها");

                entity.Property(e => e.RankingId).HasColumnName("RankingID");

                entity.Property(e => e.ComanyId).HasColumnName("ComanyID");

                entity.Property(e => e.LongTermRating)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength(true)
                    .HasComment("رتبه بلند مدت");

                entity.Property(e => e.PressRelease)
                    .HasMaxLength(200)
                    .HasComment("لینک فایل بیانیه مطبوعاتی");

                entity.Property(e => e.PublishDate)
                    .HasMaxLength(10)
                    .IsFixedLength(true)
                    .HasComment("تاریخ انتشار");

                entity.Property(e => e.ShortTermRating)
                    .HasMaxLength(10)
                    .IsFixedLength(true)
                    .HasComment("رتبه کوتاه مدت");

                entity.Property(e => e.SummaryRanking)
                    .HasMaxLength(200)
                    .HasComment("لینک فایل خلاصه رتبه بندی");

                entity.Property(e => e.UserId)
                    .HasColumnName("UserID")
                    .HasComment("کاربر ثبت کننده");

                entity.Property(e => e.Vision)
                    .HasMaxLength(100)
                    .HasComment("چشم انداز");

                entity.HasOne(d => d.Comany)
                    .WithMany(p => p.RankingOfCompanies)
                    .HasForeignKey(d => d.ComanyId)
                    .HasConstraintName("FK_RankingOfCompanies_Companies");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.RankingOfCompanies)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_RankingOfCompanies_Users");
            });

            modelBuilder.Entity<RequestForRating>(entity =>
            {
                entity.HasKey(e => e.RequestId)
                    .HasName("PK_RequestForReating");

                entity.HasComment("ثبت درخواست");

                entity.Property(e => e.RequestId).HasColumnName("RequestID");

                entity.Property(e => e.CustomerId)
                    .HasColumnName("CustomerID")
                    .HasComment("نام مشتری");

                entity.Property(e => e.DateOfConfirmed).HasColumnType("datetime");

                entity.Property(e => e.DateOfRequest)
                    .HasColumnType("datetime")
                    .HasComment("تاریخ درخواست");

                entity.Property(e => e.ChangeDate)
                    .HasColumnType("datetime");                    

                entity.Property(e => e.KindOfRequest).HasComment("نوع درخواست ");

                entity.Property(e => e.RequestNo).HasComment("شماره درخواست");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.RequestForRating)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK_RequestForReating_Customers");

                entity.HasOne(d => d.KindOfRequestNavigation)
                    .WithMany(p => p.RequestForRating)
                    .HasForeignKey(d => d.KindOfRequest)
                    .HasConstraintName("FK_RequestForRating_SystemSeting");
            });

            modelBuilder.Entity<RequestReferences>(entity =>
            {

                entity.Property(e => e.DestLevelStepIndexButton).HasMaxLength(100);

                entity.HasKey(e => e.ReferenceId);

                entity.Property(e => e.SmsContent).HasMaxLength(150);

                entity.HasComment("جدول ارجاعات درخواست مشتری");

                entity.Property(e => e.ReferenceId).HasColumnName("ReferenceID");

                entity.Property(e => e.DestLevelStepIndex)
                    .HasMaxLength(50)
                    .HasComment("مقصد هایی که میتوان به آن ارجاع داد");

                entity.Property(e => e.LevelStepAccessRole)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasComment("یوزرهای که به این لول استپ دسترسی دارند");

                entity.Property(e => e.LevelStepStatus).HasMaxLength(100);

                entity.Property(e => e.SendTime).HasColumnType("datetime");

                entity.HasOne(d => d.Request)
                    .WithMany(p => p.RequestReferences)
                    .HasForeignKey(d => d.Requestid)
                    .HasConstraintName("FK_RequestReferences_RequestForReating");

                entity.HasOne(d => d.SendUserNavigation)
                    .WithMany(p => p.RequestReferences)
                    .HasForeignKey(d => d.SendUser)
                    .HasConstraintName("FK_RequestReferences_Users1");

                entity.Property(e => e.SmsType).HasComment("0= Customer , 1= ParsKeyan");

            });            

            modelBuilder.Entity<Roles>(entity =>
            {
                entity.HasKey(e => e.RoleId)
                    .HasName("PK__Roles__8AFACE3AE90187AE");

                entity.HasComment("نقش ها");

                entity.Property(e => e.RoleId)
                    .ValueGeneratedNever()
                    .HasColumnName("RoleID");

                entity.Property(e => e.RoleDesc).HasMaxLength(50);

                entity.Property(e => e.RoleTitle)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<ServiceFee>(entity =>
            {
                entity.HasComment("جدول نرخ خدمات");

                entity.Property(e => e.ChangeDate).HasColumnType("datetime");

                entity.Property(e => e.FixedCost).HasColumnType("money");

                entity.Property(e => e.Fee1).HasColumnType("money");
                entity.Property(e => e.Fee2).HasColumnType("money");

                entity.Property(e => e.FromCompanyRange).HasComment("از تعداد کارمند");

                entity.Property(e => e.KindOfService).HasComment("نوع خدمت از جدول سیستم ستینگ");

                entity.Property(e => e.ToCompanyRange).HasComment("تا تعداد کارمند");

                entity.Property(e => e.VariableCost).HasColumnType("money");

                entity.HasOne(d => d.KindOfServiceNavigation)
                    .WithMany(p => p.ServiceFee)
                    .HasForeignKey(d => d.KindOfService)
                    .HasConstraintName("FK_ServiceFee_SystemSeting");

                entity.Property(e => e.StartDate)
                    .HasMaxLength(10)
                    .IsFixedLength(true);

            });

            modelBuilder.Entity<State>(entity =>
            {
                entity.Property(e => e.StateId).HasColumnName("StateID");

                entity.Property(e => e.StateName)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<SystemSeting>(entity =>
            {
                entity.Property(e => e.SystemSetingId).HasColumnName("SystemSetingID");

                entity.Property(e => e.ChangeDate).HasColumnType("datetime");

                entity.Property(e => e.Label).HasMaxLength(255);

                entity.Property(e => e.TitleBaseAmount).HasMaxLength(100);

                entity.Property(e => e.Value).HasMaxLength(100);
            });

            modelBuilder.Entity<UserRoles>(entity =>
            {
                entity.HasComment("نقش های کاربران");

                entity.Property(e => e.ExpiredDate).HasColumnType("datetime");

                entity.Property(e => e.RoleId).HasColumnName("RoleID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.UserRoles)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserRoles_Roles");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserRoles)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_dbo.UserRoles_dbo.Users_UserID");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK__Users__1788CCAC9660A24A");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.Ip).HasMaxLength(50);

                entity.Property(e => e.Mobile).HasMaxLength(11);

                entity.Property(e => e.Password).HasMaxLength(500);

                entity.Property(e => e.RealName)
                    .HasMaxLength(500)
                    .HasComment("نام و نام خانوادگی");

                entity.Property(e => e.UserName).HasMaxLength(100);

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK_Users_Customers");
            });
                 
        }


    }
}
