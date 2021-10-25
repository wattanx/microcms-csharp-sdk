# microCMS C# SDK

It helps you to use microCMS from C# applications.(unofficial)

## Getting Started

### Install

- Package Manager

```
Install-Package microCMS.SDK -Version 1.1.0
```

- .NET CLI

```
dotnet add package microCMS.SDK --version 1.1.0
```

### How to use

First, create a client.

```c#
using microCMS.SDK.Client;
using microCMS.SDK.Query;

public class Sample
{
    public void Get()
    {
        var client = new MicroCMSClient(serviceDomain, apiKey);
    }
}
```

After, How to use it below.

Need to add `JsonProperty` attribute for deserialization.

```c#
using Newtonsoft.Json;
using microCMS.SDK;

public class Category : MicroCMSListContent
{
    [JsonProperty("name")]
    public string Name { get; set; }
}
```

```c#
using microCMS.SDK.Client;
using microCMS.SDK.Query;

public class Sample
{
    public void Get()
    {
        var client = new MicroCMSClient(serviceDomain, apiKey);

        // Response is deserialized to generic type parameter `T`.
        var response = client.GetList<Category>(new GetListRequest()
        {
            Endpoint = "categories"
        }).Result;
    }
}
```
