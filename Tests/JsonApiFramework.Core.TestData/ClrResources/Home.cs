﻿// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using JsonApiFramework.Json;
using JsonApiFramework.JsonApi;

using Newtonsoft.Json;

namespace JsonApiFramework.TestData.ClrResources
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Home : JsonObject
        , IGetLinks
        , ISetLinks
    {
        [JsonProperty] public string Message { get; set; }
        [JsonProperty] public Links Links { get; set; }
    }
}