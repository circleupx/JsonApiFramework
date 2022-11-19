// Copyright (c) 2015�Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Diagnostics.Contracts;
using System.Linq.Expressions;

using JsonApiFramework.JsonApi;
using JsonApiFramework.Reflection;
using JsonApiFramework.ServiceModel.Internal;

namespace JsonApiFramework.ServiceModel.Configuration;

public static class ResourceTypeBuilderExtensions
{
    // PUBLIC METHODS ///////////////////////////////////////////////////
    #region Extension Methods
    public static IResourceIdentityInfoBuilder ResourceIdentity<TResource, TProperty>(this IResourceTypeBuilder<TResource> resourceTypeBuilder, Expression<Func<TResource, TProperty>> clrPropertySelector)
        where TResource : class
    {
        Contract.Requires(resourceTypeBuilder != null);
        Contract.Requires(clrPropertySelector != null);

        var clrPropertyType = typeof(TProperty);
        var clrPropertyName = StaticReflection.GetMemberName(clrPropertySelector);
        return resourceTypeBuilder.ResourceIdentity(clrPropertyName, clrPropertyType);
    }

    public static IRelationshipsInfoBuilder Relationships<TResource>(this IResourceTypeBuilder<TResource> resourceTypeBuilder, Expression<Func<TResource, Relationships>> clrPropertySelector)
        where TResource : class
    {
        Contract.Requires(resourceTypeBuilder != null);

        var clrPropertyName = StaticReflection.GetMemberName(clrPropertySelector);
        return resourceTypeBuilder.Relationships(clrPropertyName);
    }

    public static ILinksInfoBuilder Links<TResource>(this IResourceTypeBuilder<TResource> resourceTypeBuilder, Expression<Func<TResource, Links>> clrPropertySelector)
        where TResource : class
    {
        Contract.Requires(resourceTypeBuilder != null);

        var clrPropertyName = StaticReflection.GetMemberName(clrPropertySelector);
        return resourceTypeBuilder.Links(clrPropertyName);
    }

    public static IMetaInfoBuilder Meta<TResource>(this IResourceTypeBuilder<TResource> resourceTypeBuilder, Expression<Func<TResource, Meta>> clrPropertySelector)
        where TResource : class
    {
        Contract.Requires(resourceTypeBuilder != null);

        var clrPropertyName = StaticReflection.GetMemberName(clrPropertySelector);
        return resourceTypeBuilder.Meta(clrPropertyName);
    }

    public static IPropertyInfo CreatePropertyInfo<TResource, TProperty>(this IResourceTypeBuilder<TResource> resourceTypeBuilder, Expression<Func<TResource, TProperty>> clrPropertySelector)
        where TResource : class
    {
        Contract.Requires(resourceTypeBuilder != null);
        Contract.Requires(clrPropertySelector != null);

        var clrDeclaringType = typeof(TResource);
        var clrPropertyType  = typeof(TProperty);
        var clrPropertyName  = StaticReflection.GetMemberName(clrPropertySelector);

        var propertyInfo = new PropertyInfo(clrDeclaringType, clrPropertyName, clrPropertyType);
        return propertyInfo;
    }
    #endregion
}
