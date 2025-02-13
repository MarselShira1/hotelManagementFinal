﻿using hotelManagement.BLL.Services;
using hotelManagement.DAL;
using HotelManagementFinal.BLL.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class Startup
{
    public static void RegisterBLLServices(this IServiceCollection services, IConfiguration config)
    {
        services.RegisterDALServices(config);   
        services.AddScoped<IRoomService, RoomService>();
        services.AddScoped<IRoomTypeService, RoomTypeService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddTransient<IMailSenderService, MailSenderService>();
        services.AddScoped<IRoomRateService, RoomRateService>();
        services.AddScoped<IRoomRateRangesService, RoomRateRangesService>();
        services.AddScoped<IBookingService, BookingService>();
        services.AddScoped<IUserService, UserService>();
    }
}

