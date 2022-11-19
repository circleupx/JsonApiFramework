﻿// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

namespace JsonApiFramework.Server;

public interface IToOneIncludedResourceBuilder : IResourceBuilder<IToOneIncludedResourceBuilder>
{
    // PUBLIC METHODS ///////////////////////////////////////////////////
    #region Methods
    IIncludedResourcesBuilder IncludeEnd();
    #endregion
}

public interface IToOneIncludedResourceBuilder<out TResource> : IResourceBuilder<IToOneIncludedResourceBuilder<TResource>, TResource>
    where TResource : class
{
    // PUBLIC METHODS ///////////////////////////////////////////////////
    #region Methods
    IIncludedResourcesBuilder IncludeEnd();
    #endregion
}
