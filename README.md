# Wild Oasis API

Rest API for the Wild Oasis Hotel

## Getting started

To make it easy for you to get started with GitLab, here's a list of recommended next steps.

Already a pro? Just edit this README.md and make it your own. Want to make it easy? [Use the template at the bottom](#editing-this-readme)!

## AppSettings Content

API

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft": "Warning",
      "Microsoft.Hosting.Lifetime": "Information"
    }
  },
  "RedisCacheUrl": "127.0.0.1:6379",
  "AllowedOrigins": "http://localhost:3000",
  "ConnectionStrings": {
    "WildOasisData": "User ID=xxxx;Password=xxxx;Server=localhost;Port=5432;Database=wildoasis;Integrated Security=true;Pooling=true;",
    "WildOasisLogger": "User ID=xxxx;Password=xxxx;Server=localhost;Port=5432;Database=logger;Integrated Security=true;Pooling=true;"
  },
  "WebProtocolSettings": {
    "Url": "https://localhost",
    "Port": 44392
  },
  "TokenSettings": {
    "ResourceCode": "427",
    "ValidateTokens": "false",
    "ValidateTokensByUser": "true"
  },
  "JwtKey": "0FD5B805172C464597D2FFC0025677210381FF67654C4888B3B131445968D173"
}

```
