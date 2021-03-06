# Unofficial.BtgPactual.Merchant.Sdk 

A BTG Pactual library for merchants customers.

This aims to be an unofficial library version written in C# of the official BTG Pactual API.

https://developer.btgpactual.com/dochub

[![Nuget](https://img.shields.io/nuget/dt/Unofficial.BtgPactual.Merchant.Sdk)](https://www.nuget.org/packages/Unofficial.BtgPactual.Merchant.Sdk)
[![Nuget](https://img.shields.io/nuget/v/Unofficial.BtgPactual.Merchant.Sdk)](https://www.nuget.org/packages/Unofficial.BtgPactual.Merchant.Sdk)

![Library Tests](https://user-images.githubusercontent.com/108108778/177191291-3082ee69-a839-4262-bebc-7b68631ab25e.JPG)

## Notes
Version 2.1.0:

- Add method unlink location from collection
- Add methods for get qrcode image (base64 and svg)
- Add method for abstract location create on create collection
- Performance improvements
- Retries on dependents methods

Version 2.0.0:

- Add payment management
- Add reversal management
- Add errors on response envelope
- Add request timeout in seconds initializer
- Improve performance
- Some fixes

Version 1.0.0:

- Immediate Collection management
- Location management

## Installation

Use the package manager to install.

```bash
Install-Package Unofficial.BtgPactual.Merchant.Sdk -Version 2.1.0
```

## Usage

```C#
var services = new ServiceCollection();
services.AddSingleton(HttpClient)
        .AddSingleton(p => new Models.Requests.Authorization() 
        { 
            client_id = "<your client_id>", 
            client_secret = "<your client_secret>",
            is_production = false,
            request_timeout_in_seconds = 120
        })
        .AddTransient<IImmediateCollection, Services.Repositories.ImmediateCollection>()
        .AddTransient<IPayment, Services.Repositories.Payment>()
        .AddTransient<ILocation, Services.Repositories.Location>();

var serviceProvider = services.BuildServiceProvider();

```

```C#
public class Payment
{
        private readonly IPayment _payment;

        public Payment(IPayment payment)
        {
                _payment = payment;
        }

        public async Task<Models.Responses.Bacen.ListPayment> ListPayment()
        {
                Models.Responses.Bacen.Envelope<Models.Responses.Bacen.ListPayment> payments = await _payment.ListAsync(new Models.Requests.Bacen.ListPayment()
                {
                        inicio = "2022-04-24T00:00:00Z",
                        fim = "2022-06-25T20:00:00Z"
                });

                return payments?.Body;
        }
}

```

## Issues
For major changes, please open an issue to discuss your point or send e-mail to btg.unofficial.merchant.sdk@gmail.com

## License
[MIT](https://choosealicense.com/licenses/mit/)
