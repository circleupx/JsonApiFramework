﻿// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using JsonApiFramework.Internal.Dom;
using JsonApiFramework.Internal.Tree;
using JsonApiFramework.JsonApi;

using Xunit;

namespace JsonApiFramework.TestAsserts.Internal.Dom;

using DomNode = Node<DomNodeType>;

internal static class DomResourceIdentifierAssert
{
    // PUBLIC METHODS ///////////////////////////////////////////////////
    #region Assert Methods
    public static void Equal(ResourceIdentifier expectedApiResourceIdentifier, DomNode actual)
    {
        if (expectedApiResourceIdentifier == null)
        {
            Assert.Null(actual);
            return;
        }
        Assert.NotNull(actual);

        var actualType = actual.GetType();

        if (actualType == typeof(DomReadOnlyResourceIdentifier))
        {
            var actualDomReadOnlyResourceIdentifier = (DomReadOnlyResourceIdentifier)actual;
            DomReadOnlyResourceIdentifierAssert.Equal(expectedApiResourceIdentifier, actualDomReadOnlyResourceIdentifier);
        }
        else if (actualType == typeof(DomReadWriteResourceIdentifier))
        {
            var actualDomReadWriteResourceIdentifier = (DomReadWriteResourceIdentifier)actual;
            DomReadWriteResourceIdentifierAssert.Equal(expectedApiResourceIdentifier, actualDomReadWriteResourceIdentifier);
        }
        else
        {
            Assert.True(false, "Unknown actual node type [name={0}].".FormatWith(actualType.Name));
        }
    }
    #endregion
}