﻿// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.
using System.Collections.Generic;
using System.Linq;

using JsonApiFramework.Internal.Dom;
using JsonApiFramework.JsonApi;
using JsonApiFramework.TestAsserts.JsonApi;

using Xunit;

namespace JsonApiFramework.TestAsserts.Internal.Dom;

internal static class DomReadWriteErrorsAssert
{
    // PUBLIC METHODS ///////////////////////////////////////////////////
    #region Assert Methods
    public static void Equal(IEnumerable<Error> expected, DomReadWriteErrors actual)
    {
        if (expected == null)
        {
            Assert.Null(actual);
            return;
        }
        Assert.NotNull(actual);

        Assert.Equal(DomNodeType.Errors, actual.NodeType);

        var expectedCollection = expected.SafeToReadOnlyList();

        var actualNodes = actual.Nodes()
                                .ToList();

        var actualDomErrorsCount = actualNodes.Count;
        Assert.Equal(expectedCollection.Count, actualDomErrorsCount);

        var count = expectedCollection.Count;
        for (var i = 0; i < count; ++i)
        {
            var expectedError = expectedCollection[i];

            var actualNode = actualNodes[i];
            var actualDomError = (IDomError)actualNode;
            var actualError = actualDomError.Error;

            ErrorAssert.Equal(expectedError, actualError);
        }
    }
    #endregion
}