﻿using System.Text.Json;

namespace BookMgmtWebApp.Data
{
    public class WebApiException:Exception
    {
        public ErrorResponse? ErrorResponse { get;}

        public WebApiException(string errorJson)
        {
            ErrorResponse = JsonSerializer.Deserialize<ErrorResponse>(errorJson);
        }
    }
}
