// Copyright (c) 2015�Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using Humanizer;

namespace JsonApiFramework.Conventions.Internal;

/// <summary>Naming convention that pluralizes the name.</summary>
internal class PluralNamingConvention : INamingConvention
{
    // PUBLIC METHODS ///////////////////////////////////////////////////
    #region INamingConvention Implementation
    public string Apply(string oldName)
    {
        if (string.IsNullOrWhiteSpace(oldName))
            return oldName;

        var newName = oldName.Pluralize(inputIsKnownToBeSingular:false);
        return newName;
    }
    #endregion
}