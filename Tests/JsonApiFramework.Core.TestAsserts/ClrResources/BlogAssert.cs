﻿// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using JsonApiFramework.TestAsserts.JsonApi;
using JsonApiFramework.TestData.ClrResources;

using Xunit;

namespace JsonApiFramework.TestAsserts.ClrResources;

public static class BlogAssert
{
    // PUBLIC METHODS ///////////////////////////////////////////////////
    #region Assert Methods
    public static void Equal(Blog expected, Blog actual)
    {
        // Handle when 'expected' is null.
        if (expected == null)
        {
            Assert.Null(actual);
            return;
        }
        Assert.NotNull(actual);

        Assert.Equal(expected.Id, actual.Id);
        Assert.Equal(expected.Name, actual.Name);
        RelationshipsAssert.Equal(expected.Relationships, actual.Relationships);
        LinksAssert.Equal(expected.Links, actual.Links);
        ClrObjectAssert.Equal(expected.Meta, actual.Meta);
    }

    public static void Equal(IEnumerable<Blog> expected, IEnumerable<Blog> actual)
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

            BlogAssert.Equal(expectedItem, actualItem);
        }
    }
    #endregion
}