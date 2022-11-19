﻿// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using JsonApiFramework.TestData.ClrResources.ComplexTypes;

using Xunit;

namespace JsonApiFramework.TestAsserts.ClrResources.ComplexTypes;

public static class MailingAddressAssert
{
    // PUBLIC METHODS ///////////////////////////////////////////////////
    #region Assert Methods
    public static void Equal(MailingAddress expected, MailingAddress actual)
    {
        // Handle when 'expected' is null.
        if (expected == null)
        {
            Assert.Null(actual);
            return;
        }
        Assert.NotNull(actual);

        Assert.Equal(expected.Address, actual.Address);
        Assert.Equal(expected.City, actual.City);
        Assert.Equal(expected.State, actual.State);
        Assert.Equal(expected.ZipCode, actual.ZipCode);
    }

    public static void Equal(IEnumerable<MailingAddress> expected, IEnumerable<MailingAddress> actual)
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

            MailingAddressAssert.Equal(expectedItem, actualItem);
        }
    }
    #endregion
}