# Unofficial.BtgPactual.Merchant.Sdk 

A BTG Pactual library for merchants customers.

This aims to be an unofficial library version written in C# of the official BTG Pactual API.

## Notes
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
Install-Package Unofficial.BtgPactual.Merchant.Sdk -Version 2.0.0
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
        .AddTransient<ILocation, Services.Repositories.Location>();

var serviceProvider = services.BuildServiceProvider();

```

## Issues
For major changes, please open an issue to discuss what you would like to change or send e-mail to btg.unofficial.merchant.sdk@gmail.com

## License
[MIT](https://choosealicense.com/licenses/mit/)
