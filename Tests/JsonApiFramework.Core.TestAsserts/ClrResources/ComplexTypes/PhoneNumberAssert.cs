﻿// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using JsonApiFramework.TestData.ClrResources.ComplexTypes;

using Xunit;

namespace JsonApiFramework.TestAsserts.ClrResources.ComplexTypes;

public static class PhoneNumberAssert
{
    // PUBLIC METHODS ///////////////////////////////////////////////////
    #region Assert Methods
    public static void Equal(PhoneNumber expected, PhoneNumber actual)
    {
        // Handle when 'expected' is null.
        if (expected == null)
        {
            Assert.Null(actual);
            return;
        }
        Assert.NotNull(actual);

        Assert.Equal(expected.AreaCode, actual.AreaCode);
        Assert.Equal(expected.Number, actual.Number);
    }

    public static void Equal(IEnumerable<PhoneNumber> expected, IEnumerable<PhoneNumber> actual)
    {
        // Handle when 'expected' is null.
        if (expected == null)
        {
            Assert.Null(actual);
            return;
        }
        Assert.NotNull(actual);

        var expectedCollection = expected.SafeToList();
        var actualCollection = actual.SafeToList();

        Assert.Equal(expectedCollection.Count, actualCollection.Count);

        var count = expectedCollection.Count;
        for (var index = 0; index < count; ++index)
        {
            var expectedItem = expectedCollection[index];
            var actualItem = actualCollection[index];

            PhoneNumberAssert.Equal(expectedItem, actualItem);
        }
    }
    #endregion
}