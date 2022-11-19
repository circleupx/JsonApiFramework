﻿// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using JsonApiFramework.Json;
using JsonApiFramework.JsonApi;
using JsonApiFramework.TestAsserts.JsonApi;
using JsonApiFramework.TestData.ApiResources;
using JsonApiFramework.XUnit;

using Xunit.Abstractions;

namespace JsonApiFramework.Tests.JsonApi;

public class ErrorTests : XUnitTest
{
    // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
    #region Constructors
    public ErrorTests(ITestOutputHelper output)
        : base(output)
    { }
    #endregion

    // PUBLIC METHODS ///////////////////////////////////////////////////
    #region Test Methods
    [Theory]
    [MemberData(nameof(ErrorTestData))]
    public void TestErrorToJson(string name, Error expected)
    {
        // Arrange

        // Act
        var actual = expected.ToJson();
        this.Output.WriteLine(actual);

        // Assert
        ErrorAssert.Equal(expected, actual);
    }

    [Theory]
    [MemberData(nameof(ErrorTestData))]
    public void TestErrorParse(string name, Error expected)
    {
        // Arrange
        var json = expected.ToJson();

        // Act
        this.Output.WriteLine(json);
        var actual = JsonObject.Parse<Error>(json);

        // Assert
        ErrorAssert.Equal(expected, actual);
    }
    #endregion

    // PUBLIC FIELDS ////////////////////////////////////////////////////
    #region Test Data
    // ReSharper disable once UnusedMember.Global
    public static readonly IEnumerable<object[]> ErrorTestData = new[]
        {
            new object[] {"WithEmptyObject", Error.Empty},
            new object[] {"WithCompleteObject", ApiSampleData.Error},
            new object[] {"WithCompleteObject1", ApiSampleData.Error1},
            new object[] {"WithCompleteObject2", ApiSampleData.Error2}
        };
    #endregion
}
