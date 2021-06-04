MoceanAPI Client Library for C#/.NET
============================
[![Latest Stable Version](https://img.shields.io/nuget/v/Mocean.Csharp.Client.svg)](https://www.nuget.org/packages/Mocean.Csharp.Client/)
[![Build Status](https://img.shields.io/travis/com/MoceanAPI/mocean-sdk-dotnet.svg)](https://travis-ci.com/MoceanAPI/mocean-sdk-dotnet)
[![codecov](https://img.shields.io/codecov/c/github/MoceanAPI/mocean-sdk-dotnet.svg)](https://codecov.io/gh/MoceanAPI/mocean-sdk-dotnet)
[![codacy](https://img.shields.io/codacy/grade/0915168c1277416fbd512a47d7069082.svg)](https://app.codacy.com/project/MoceanAPI/mocean-sdk-dotnet/dashboard)
[![License](https://img.shields.io/packagist/l/mocean/client.svg)](https://packagist.org/packages/mocean/client)
[![Total Downloads](https://img.shields.io/nuget/dt/Mocean.Csharp.Client.svg)](https://www.nuget.org/packages/Mocean.Csharp.Client/)

This is the C# client library for use Mocean's API. To use this, you'll need a Mocean account. Sign up [for free at 
moceanapi.com][signup].

 * [Installation](#installation)
 * [Usage](#usage)
 * [Example](#example)

## Installation

To use the client library you'll need to have [created a Mocean account][signup]. 

To install the C# client library using NuGet:

 - Run the following command in the Package Manager Console:

```bash
Install-Package Mocean.Csharp.Client
```

## Usage

You need to create a Client object containing your API key and secret to work with all function:

```csharp
using Mocean;
using Mocean.Auth;

var credentials = new Basic("API_KEY_HERE", "API_SECRET_HERE");
var client = new Client(credentials);
```

## Example

To use [Mocean's SMS API][doc_sms] to send an SMS message, call the `client.Sms.Send()` method.

The API can be called directly, using a simple array of parameters, the keys match the [parameters of the API][doc_sms].

```csharp
var res = client.Sms.Send(new Mocean.Message.SmsRequest
{
    mocean_to = "60123456789",
    mocean_from = "MOCEAN",
    mocean_text = "Hello World"
});

Console.WriteLine(res);
```

### Responses

For your convenient, the API response has been parse to specified object.

```csharp
Console.WriteLine(res);         //show full response string
Console.WriteLine(res.status);  //show response status, "0" in this case
```

## Documentation

Kindly visit [MoceanApi Docs][doc_main] for more usage

## License

This library is released under the [MIT License][license]

[signup]: https://dashboard.moceanapi.com/register?medium=github&campaign=dotnet-sdk
[doc_main]: https://moceanapi.com/docs/?csharp
[doc_sms]: https://moceanapi.com/docs/?csharp#send-sms
[license]: LICENSE.md
