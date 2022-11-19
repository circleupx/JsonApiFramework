// Copyright (c) 2015�Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using JsonApiFramework.ServiceModel.Internal;

using Newtonsoft.Json.Converters;

namespace JsonApiFramework.ServiceModel.Converters;

public class ResourceTypeConverter : CustomCreationConverter<IResourceType>
{
    // PUBLIC METHODS ///////////////////////////////////////////////////
    #region CustomCreationConverter Overrides
    public override IResourceType Create(Type objectType)
    { return new ResourceType(); }
    #endregion
}