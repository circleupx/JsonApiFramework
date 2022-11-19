﻿// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using JsonApiFramework.Json;
using JsonApiFramework.JsonApi;
using JsonApiFramework.TestAsserts.JsonApi;
using JsonApiFramework.TestData.ApiResources;
using JsonApiFramework.XUnit;

using Xunit.Abstractions;

namespace JsonApiFramework.Tests.JsonApi;

public class JsonApiVersionTests : XUnitTest
{
    // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
    #region Constructors
    public JsonApiVersionTests(ITestOutputHelper output)
        : base(output)
    { }
    #endregion

    // PUBLIC METHODS ///////////////////////////////////////////////////
    #region Test Methods
    [Theory]
    [MemberData(nameof(JsonApiVersionTestData))]
    public void TestJsonApiVersionToJson(string name, JsonApiVersion expected)
    {
        // Arrange

        // Act
        var actual = expected.ToJson();
        this.Output.WriteLine(actual);

        // Assert
        JsonApiVersionAssert.Equal(expected, actual);
    }

    [Theory]
    [MemberData(nameof(JsonApiVersionTestData))]
    public void TestJsonApiVersionParse(string name, JsonApiVersion expected)
    {
        // Arrange
        var json = expected.ToJson();

        // Act
        this.Output.WriteLine(json);
        var actual = JsonObject.Parse<JsonApiVersion>(json);

        // Assert
        JsonApiVersionAssert.Equal(expected, actual);
    }
    #endregion

    // PUBLIC FIELDS ////////////////////////////////////////////////////
    #region Test Data
    // ReSharper disable once UnusedMember.Global
    public static readonly IEnumerable<object[]> JsonApiVersionTestData = new[]
        {
            new object[] {"WithEmptyObject", JsonApiVersion.Empty},
            new object[] {"WithVersion", ApiSampleData.JsonApiVersion},
            new object[] {"WithVersionAndMeta", ApiSampleData.JsonApiVersionAndMeta}
        };
    #endregion
}
