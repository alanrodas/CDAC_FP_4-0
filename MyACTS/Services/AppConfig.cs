using System;
using System.Collections.Generic;

namespace MyACTS.Services;

public interface IAppConfig {
    string ConnectionString { get; }
    string LoginPath { get; }
    string LogoutPath { get; }
    string ReturnUrlParameter { get;  }
    string ErrorPage { get; }
    string DefaultController { get; }
    string DefaultAction { get; }
    IDictionary<string, string> RoleRedirects { get; }
}

public class AppConfig : IAppConfig
{
    public AppConfig(string connectionString)
    {
        ConnectionString = connectionString;
    }

    public string ConnectionString { get; set; } = null!;
    public string LoginPath { get; set; } = null!;
    public string LogoutPath { get; set; } = null!;
    public string ReturnUrlParameter { get; set; } = null!;
    public string ErrorPage { get; set; } = null!;
    public string DefaultController { get; set; } = null!;
    public string DefaultAction { get; set; } = null!;
    public IDictionary<string, string> RoleRedirects { get; set; } = null!;
}

