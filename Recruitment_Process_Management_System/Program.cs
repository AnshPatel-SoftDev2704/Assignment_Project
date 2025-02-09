using Microsoft.EntityFrameworkCore;
using Recruitment_Process_Management_System.data;
using Recruitment_Process_Management_System.Services;
using Recruitment_Process_Management_System.Repositories;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddControllers().AddJsonOptions(options =>{
    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<ISkillService, SkillService>();
builder.Services.AddScoped<ISkillRepository, SkillRepository>();
builder.Services.AddScoped<IJobStatusService, JobStatusService>();
builder.Services.AddScoped<IJobStatusRepository, JobStatusRepository>();
builder.Services.AddScoped<IUserRolesService, UserRolesService>();
builder.Services.AddScoped<IUserRolesRepository, UserRolesRepository>();
builder.Services.AddScoped<IJobsSerivce, JobsService>();
builder.Services.AddScoped<IJobsRepository, JobsRepository>();
builder.Services.AddScoped<IRequired_Job_SkillService,Required_Job_SkillService>();
builder.Services.AddScoped<IRequired_Job_SkillRepository, Required_Job_SkillRepository>();
builder.Services.AddScoped<IPreferred_Job_SkillService,Preferred_Job_SkillService>();
builder.Services.AddScoped<IPreferred_Job_SkillRepository, Preferred_Job_SkillRepository>();
builder.Services.AddScoped<ICandidate_DetailsService,Candidate_DetailsService>();
builder.Services.AddScoped<ICandidate_DetailsRepository, Candidate_DetailsRepository>();
builder.Services.AddScoped<ICandidate_SkillService,Candidate_SkillService>();
builder.Services.AddScoped<ICandidate_SkillsRepostiroy, Candidate_SkillsRepostiroy>();
builder.Services.AddScoped<IApplication_StatusService,Application_StatusService>();
builder.Services.AddScoped<IApplication_StatusRepository, Application_StatusRepository>();
builder.Services.AddScoped<ICandidate_Application_StatusService,Candidate_Application_StatusService>();
builder.Services.AddScoped<ICandidate_Application_StatusRepository, Candidate_Application_StatusRepository>();
builder.Services.AddScoped<IInterview_TypeService,Interview_TypeService>();
builder.Services.AddScoped<IInterview_TypeRepository, Interview_TyperRepository>();
builder.Services.AddScoped<IInterview_StatusService,Interview_StatusService>();
builder.Services.AddScoped<IInterview_StatusRepository, Interview_StatusRepository>();
builder.Services.AddScoped<IInterviewService,InterviewService>();
builder.Services.AddScoped<IInterviewRepository, InterviewRepository>();
builder.Services.AddScoped<IInterviewerService,InterviewerService>();
builder.Services.AddScoped<IInterviewerRepository, InterviewerRepository>();
builder.Services.AddScoped<IDocument_TypeService,Document_TypeService>();
builder.Services.AddScoped<IDocument_TypeRepository, Document_TypeRespository>();
builder.Services.AddScoped<IDocument_SubmittedService,Document_SubmittedService>();
builder.Services.AddScoped<IDocument_SubmittedRepository, Document_SubmittedRepository>();
builder.Services.AddScoped<IFeedbackService,FeedbackService>();
builder.Services.AddScoped<IFeedbackRepository, FeedbackRepository>();
builder.Services.AddScoped<ISelected_CandidateService,Selected_CandidateService>();
builder.Services.AddScoped<ISelected_CandidateRepository, Selected_CandidateRepository>();
builder.Services.AddScoped<AuthService>();
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
    builder.Services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(opt =>
{   
    opt.RequireHttpsMetadata = false;
    opt.SaveToken = true;
    opt.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["JWT:SecretKey"])),
        ValidateIssuer = true,
        ValidIssuer = builder.Configuration["JWT:Issuer"],
        ValidateAudience = true,
        ValidAudience = builder.Configuration["JWT:Audience"]
    };
});

builder.Services.AddAuthorization();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder => builder.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader());
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) 
{ 
    app.UseSwagger(); 
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty;
    }); 
}
else
{
    app.UseHttpsRedirection();
}
app.UseCors("AllowAllOrigins");

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();