// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using JsonApiFramework.Metadata.Conventions;

namespace JsonApiFramework.Metadata.Configuration
{
    /// <summary>Represents a factory that creates complex types with optional conventions.</summary>
    public interface IComplexTypeFactory
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Methods
        /// <summary>
        /// Factory method that creates a complex type with optional conventions.
        /// </summary>
        /// <param name="metadataConventions">Optional conventions, can be null.</param>
        /// <returns>A newly created complex type object.</returns>
        IComplexType Create(IMetadataConventions metadataConventions);
        #endregion
    }
}