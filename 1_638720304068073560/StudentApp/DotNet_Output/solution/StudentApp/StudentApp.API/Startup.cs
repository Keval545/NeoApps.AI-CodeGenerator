using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
//using StudentApp.Manager.RabitMQAPI.API;
using StudentApp.DataAccess.Impl;
using StudentApp.DataAccess.Interface;
using StudentApp.Manager.Impl;
using StudentApp.Manager.Interface;
using StudentApp.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using RabbitMQ.Client;
using System.Collections.Generic;
using System;
using StudentApp.API.Attributes;
using StackExchange.Redis;

namespace StudentApp.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo { Title = "StudentApp", Version = "v1" });
                    c.AddSecurityDefinition(name: "Bearer", securityScheme: new OpenApiSecurityScheme
                    {
                        Name = "Authorization",
                        Description = "Enter the Bearer Authorization string as following: `Bearer Generated-JWT-Token`",
                        In = ParameterLocation.Header,
                        Type = SecuritySchemeType.ApiKey,
                        Scheme = "Bearer"
                    });
                    c.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Name = "Bearer",
                                In = ParameterLocation.Header,
                                Reference = new OpenApiReference
                                {
                                    Id = "Bearer",
                                    Type = ReferenceType.SecurityScheme
                                }
                            },
                            new List<string>()
                        }
                    });
                });
			 services.AddCors(options =>
            {
                options.AddPolicy("AllowAllHeaders",
                   builder =>
                   {
                       builder.AllowAnyOrigin()
                  .AllowAnyHeader()
                  .AllowAnyMethod();
                   });
            });
            services.AddScoped<CheckPermissionAttribute>();
            //services.AddSingleton<IConnectionMultiplexer>(x =>
           // ConnectionMultiplexer.Connect(Configuration.GetConnectionString("Redis")));
	    //services.AddScoped<IRabitMQProducer, RabitMQProducer>();
           // services.AddScoped<IRabitMQAsyncProducer, RabitMQAsyncProducer>();
            services.AddControllers();

            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            // configure jwt authentication
            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            services.AddSingleton<IConfiguration>(Configuration);
            string connectionString = Configuration.GetConnectionString("MySQLDatabase");
	   // string rabbitMqConnection = Configuration.GetConnectionString("RabitMQ");
			//ConnectionFactory factory = new ConnectionFactory();
            //factory.Uri = new Uri(rabbitMqConnection);
           // IConnection connection = factory.CreateConnection();
           // services.AddSingleton<IConnection>(connection);
             services.AddMvc(option => option.EnableEndpointRouting = false).SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
            //services.AddTransient(_ => new MySqlDatabase(connectionString));
            services.AddTransient(_ => new MySqlDatabaseConnector(connectionString));
	   // services.AddTransient(_ => new RabbitMqConnection(rabbitMqConnection));
			//services.AddSwaggerDocument(c=>c.Title="StudentApp");
			
            services.AddTransient<ICurrentUserService, CurrentUserService>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
          //  services.AddSingleton<IChannelManager, ChannelManager>();
			#region Dependency
            services.AddTransient<IAttendance_recordsDataAccess,Attendance_recordsDataAccess>();
services.AddTransient<IDnd_ui_versionsDataAccess,Dnd_ui_versionsDataAccess>();
services.AddTransient<IEmployeesDataAccess,EmployeesDataAccess>();
services.AddTransient<ILeave_requestsDataAccess,Leave_requestsDataAccess>();
services.AddTransient<IMessagequeueDataAccess,MessagequeueDataAccess>();
services.AddTransient<IProject_dnd_ui_versionsDataAccess,Project_dnd_ui_versionsDataAccess>();
services.AddTransient<IRolesDataAccess,RolesDataAccess>();
services.AddTransient<IS3bucketDataAccess,S3bucketDataAccess>();
services.AddTransient<IS3bucket_foldersDataAccess,S3bucket_foldersDataAccess>();
services.AddTransient<IWorkflowDataAccess,WorkflowDataAccess>();
services.AddTransient<IWorkflowsDataAccess,WorkflowsDataAccess>();
services.AddTransient<IWorkflows_projectsDataAccess,Workflows_projectsDataAccess>();
services.AddTransient<IWorkflow_buildsDataAccess,Workflow_buildsDataAccess>();
services.AddTransient<IWorkflow_deploymentsDataAccess,Workflow_deploymentsDataAccess>();
services.AddTransient<IWorkflow_runsDataAccess,Workflow_runsDataAccess>();
services.AddTransient<IWorkflow_triggersDataAccess,Workflow_triggersDataAccess>();
services.AddTransient<IWorkflow_trigger_conditionsDataAccess,Workflow_trigger_conditionsDataAccess>();
services.AddTransient<IAttendance_recordsManager,Attendance_recordsManager>();
services.AddTransient<IDnd_ui_versionsManager,Dnd_ui_versionsManager>();
services.AddTransient<IEmployeesManager,EmployeesManager>();
services.AddTransient<ILeave_requestsManager,Leave_requestsManager>();
services.AddTransient<IMessagequeueManager,MessagequeueManager>();
services.AddTransient<IProject_dnd_ui_versionsManager,Project_dnd_ui_versionsManager>();
services.AddTransient<IRolesManager,RolesManager>();
services.AddTransient<IS3bucketManager,S3bucketManager>();
services.AddTransient<IS3bucket_foldersManager,S3bucket_foldersManager>();
services.AddTransient<IWorkflowManager,WorkflowManager>();
services.AddTransient<IWorkflowsManager,WorkflowsManager>();
services.AddTransient<IWorkflows_projectsManager,Workflows_projectsManager>();
services.AddTransient<IWorkflow_buildsManager,Workflow_buildsManager>();
services.AddTransient<IWorkflow_deploymentsManager,Workflow_deploymentsManager>();
services.AddTransient<IWorkflow_runsManager,Workflow_runsManager>();
services.AddTransient<IWorkflow_triggersManager,Workflow_triggersManager>();
services.AddTransient<IWorkflow_trigger_conditionsManager,Workflow_trigger_conditionsManager>();
services.AddTransient<IEntitiesDataAccess,EntitiesDataAccess>();
services.AddTransient<IPermissionmatrixDataAccess,PermissionmatrixDataAccess>();
services.AddTransient<IUsersDataAccess,UsersDataAccess>();
services.AddTransient<IUsersManager,UsersManager>();
services.AddTransient<IEntitiesManager,EntitiesManager>();
services.AddTransient<IPermissionmatrixManager,PermissionmatrixManager>();
			#endregion
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {	app.UseCors("AllowAllHeaders");
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
			//app.UsePathBase("{arg}");
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "StudentApp");
            });
			

            app.UseAuthentication();
			
            //app.UseOpenApi();
            //app.UseSwaggerUi3(c => c.DocumentTitle = "StudentApp");
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
