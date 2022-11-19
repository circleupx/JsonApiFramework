﻿// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using JsonApiFramework.TestAsserts.ClrResources.ComplexTypes;
using JsonApiFramework.TestData.ClrResources;

using Xunit;

namespace JsonApiFramework.TestAsserts.ClrResources;

public static class StoreConfigurationAssert
{
    // PUBLIC METHODS ///////////////////////////////////////////////////
    #region Assert Methods
    public static void Equal(StoreConfiguration expected, StoreConfiguration actual)
    {
        // Handle when 'expected' is null.
        if (expected == null)
        {
            Assert.Null(actual);
            return;
        }
        Assert.NotNull(actual);

        Assert.Equal(expected.StoreConfigurationId, actual.StoreConfigurationId);
        Assert.Equal(expected.IsLive, actual.IsLive);
        MailingAddressAssert.Equal(expected.MailingAddress, actual.MailingAddress);
        PhoneNumberAssert.Equal(expected.PhoneNumbers, actual.PhoneNumbers);
    }

    public static void Equal(IEnumerable<StoreConfiguration> expected, IEnumerable<StoreConfiguration> actual)
    {
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

            StoreConfigurationAssert.Equal(expectedItem, actualItem);
        }
    }
    #endregion
}