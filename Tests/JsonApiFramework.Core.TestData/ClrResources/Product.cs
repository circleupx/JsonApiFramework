﻿// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using JsonApiFramework.Json;

using Newtonsoft.Json;

namespace JsonApiFramework.TestData.ClrResources;

[JsonObject(MemberSerialization.OptIn)]
public class Product : JsonObject
{
    [JsonProperty] public long ProductId { get; set; }
    [JsonProperty] public string Name { get; set; }
    [JsonProperty] public decimal UnitPrice { get; set; }
}