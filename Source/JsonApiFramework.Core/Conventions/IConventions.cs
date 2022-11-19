﻿// Copyright (c) 2015�Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.
namespace JsonApiFramework.Conventions;

public interface IConventions
{
    // PRIVATE PROPERTIES ///////////////////////////////////////////////
    #region Properties
    IEnumerable<INamingConvention> ApiAttributeNamingConventions { get; }
    IEnumerable<INamingConvention> ApiTypeNamingConventions { get; }
    IEnumerable<IComplexTypeConvention> ComplexTypeConventions { get; }
    IEnumerable<IResourceTypeConvention> ResourceTypeConventions { get; }
    #endregion
}