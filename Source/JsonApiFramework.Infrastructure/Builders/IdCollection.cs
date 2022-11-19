// Copyright (c) 2015�Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using JsonApiFramework.Internal;

namespace JsonApiFramework;

public static class IdCollection
{
    // PUBLIC METHODS ///////////////////////////////////////////////////
    #region Properties
    public static IIdCollection<T> Create<T>(IEnumerable<T> clrIdCollection)
    {
        var idCollection = new IdCollection<T>(clrIdCollection);
        return idCollection;
    }
    #endregion
}