﻿// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.
using System;

using JsonApiFramework.Internal.Dom;
using JsonApiFramework.Internal.Tree;

using Xunit;

namespace JsonApiFramework.TestAsserts.Internal.Dom;

using DomNode = Node<DomNodeType>;

internal static class DomHRefAssert
{
    // PUBLIC METHODS ///////////////////////////////////////////////////
    #region Assert Methods
    public static void Equal(string expected, DomNode actual)
    {
        if (string.IsNullOrWhiteSpace(expected))
        {
            Assert.Null(actual);
            return;
        }
        Assert.NotNull(actual);

        Assert.Equal(DomNodeType.HRef, actual.NodeType);

        var actualDomHRef = (DomHRef)actual;
        var actualHRef = actualDomHRef.HRef;
        Assert.Equal(expected, actualHRef);
    }
    #endregion
}