﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SkillsetAPI.Entities;
using SkillsetAPI.Services;

namespace SkillsetAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public static IConfiguration Configuration { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Startup.Configuration["JWT:ValidIssuer"],
                    ValidAudience = Startup.Configuration["JWT:ValidAudience"],
                    IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(Startup.Configuration["JWT:IssuerSigningKey"])),
                };
            });

            services.AddMvc();

            //Use for migration only, then comment all statement in DB context constructor
            //var connectionString = @"Server=(localdb)\MSSQLLocalDB;Database=dbbtSSetp1;Trusted_Connection=True";
            //Use below in Production
            var connectionString = Startup.Configuration["connectionStrings:dbbtSSetp1ConnectionString"];

            services.AddDbContext<SkillSetContext>(o => o.UseSqlServer(connectionString));

            services.AddScoped<ISkillSetRepository, SkillSetRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, SkillSetContext skillSetContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //This is for Seeding comment this when ading migration
            skillSetContext.EnsureSeedDataForContext();

            AutoMapper.Mapper.Initialize(
                    cfg => 
                    {
                        cfg.CreateMap<Entities.SetUser, Models.SetUserDTO>();
                        cfg.CreateMap<Entities.SetGroup, Models.SetGroupDTO>();
                        cfg.CreateMap<Models.AssociateForCreateDTO, Entities.Associate>()
                                .ForMember(dest => dest.UpdatedOn, opt => opt.Equals(DateTime.Now))
                                .ForMember(dest => dest.IsActive, opt => opt.Equals(true));
                        cfg.CreateMap<Models.AssociateForUpdateDTO, Entities.Associate>()
                                .ForMember(dest => dest.UpdatedOn, opt => opt.Equals(DateTime.Now));
                        cfg.CreateMap<Models.DepartmentForCreateDTO, Entities.Department>()
                                .ForMember(dest => dest.IsActive, opt => opt.Equals(true));
                        cfg.CreateMap<Models.DepartmentForUpdateDTO, Entities.Department>();
                    });

            app.UseMvc();
        }
    }
}
