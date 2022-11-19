// Copyright (c) 2015�Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Diagnostics.Contracts;

using Newtonsoft.Json.Serialization;

namespace JsonApiFramework.ServiceModel;

public static class ServiceModelExtensions
{
    // PUBLIC METHODS ///////////////////////////////////////////////////
    #region IServiceModel Extensions Methods
    public static IContractResolver CreateComplexTypesContractResolver(this IServiceModel serviceModel)
    {
        Contract.Requires(serviceModel != null);

        return serviceModel.ComplexTypes.Any()
            ? new ComplexTypesContractResolver(serviceModel)
            : null;
    }

    public static IResourceType GetResourceType<TResource>(this IServiceModel serviceModel)
    {
        Contract.Requires(serviceModel != null);

        return serviceModel.GetResourceType(typeof(TResource));
    }

    public static bool TryGetResourceType<TResource>(this IServiceModel serviceModel, out IResourceType resourceType)
    {
        Contract.Requires(serviceModel != null);

        return serviceModel.TryGetResourceType(typeof(TResource), out resourceType);
    }

    public static bool TryGetHomeResourceType(this IServiceModel serviceModel, out IResourceType homeResourceType)
    {
        Contract.Requires(serviceModel != null);

        homeResourceType = serviceModel.HomeResourceType;
        return homeResourceType != null;
    }
    #endregion
}
