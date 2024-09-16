﻿namespace CryptoCoinTracker.DataProcessor.Models.Settings;

public class DatabaseSettings
{
    public string ConnectionString { get; set; } = string.Empty;
    public string DatabaseName { get; set; } = string.Empty;
}
