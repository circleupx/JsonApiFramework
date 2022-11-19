﻿// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Diagnostics.Contracts;

using JsonApiFramework.Internal.Dom;
using JsonApiFramework.Internal.Tree;
using JsonApiFramework.JsonApi;
using JsonApiFramework.ServiceModel;

namespace JsonApiFramework.Internal;

internal abstract class ResourceIdentifierCollectionBuilder<TBuilder> : IResourceIdentifierBuilder<TBuilder>
{
    // PUBLIC METHODS ///////////////////////////////////////////////////
    #region IResourceIdentifierBuilder<TBuilder> Implementation
    public TBuilder SetMeta(Meta meta)
    {
        Contract.Requires(meta != null);

        if (this.NotBuildingResourceIdentifierCollection)
            return this.Builder;

        if (this.DomReadWriteResourceIdentifierCollection == null)
        {
            // Defer setting this common resource identifier metadata until SetId is called.
            this.ResourceIdentifierMeta = meta;
            return this.Builder;
        }

        foreach (var domReadWriteResourceIdentifier in this.DomReadWriteResourceIdentifierCollection)
        {
            domReadWriteResourceIdentifier.SetDomReadOnlyMeta(meta);
        }

        return this.Builder;
    }

    public TBuilder SetMeta(IEnumerable<Meta> metaCollection)
    {
        Contract.Requires(metaCollection != null);

        if (this.NotBuildingResourceIdentifierCollection)
            return this.Builder;

        var metaReadOnlyList      = metaCollection.SafeToReadOnlyList();
        var metaReadOnlyListCount = metaReadOnlyList.Count;

        this.CreateAndAddDomReadWriteResourceIdentifierCollectionIfNeeded(metaReadOnlyListCount);

        var domReadWriteResourceIdentifierCollectionCount = this.DomReadWriteResourceIdentifierCollection.Count;
        if (metaReadOnlyListCount != domReadWriteResourceIdentifierCollectionCount)
        {
            var detail = InfrastructureErrorStrings.DocumentBuildExceptionDetailBuildResourceCollectionCountMismatch
                                                   .FormatWith(DomNodeType.Meta, domReadWriteResourceIdentifierCollectionCount, this.ClrResourceType.Name, metaReadOnlyListCount);
            throw new DocumentBuildException(detail);
        }

        var count = domReadWriteResourceIdentifierCollectionCount;
        for (var i = 0; i < count; ++i)
        {
            var domReadWriteResourceIdentifier = this.DomReadWriteResourceIdentifierCollection[i];
            var meta                           = metaReadOnlyList[i];

            domReadWriteResourceIdentifier.SetDomReadOnlyMeta(meta);
        }

        return this.Builder;
    }

    public TBuilder SetId<T>(IId<T> id)
    {
        Contract.Requires(id != null);

        if (this.NotBuildingResourceIdentifierCollection)
            return this.Builder;

        var detail = InfrastructureErrorStrings.DocumentBuildExceptionDetailBuildResourceCollectionWithSingleObject
                                               .FormatWith(DomNodeType.Id, this.ClrResourceType.Name);
        throw new DocumentBuildException(detail);
    }

    public TBuilder SetId<T>(IIdCollection<T> idCollection)
    {
        Contract.Requires(idCollection != null);

        if (this.NotBuildingResourceIdentifierCollection)
            return this.Builder;

        var clrIdCollection                = idCollection.ClrIdCollection;
        var clrResourceIdReadOnlyList      = clrIdCollection.SafeToReadOnlyList();
        var clrResourceIdReadOnlyListCount = clrResourceIdReadOnlyList.Count;

        this.CreateAndAddDomReadWriteResourceIdentifierCollectionIfNeeded(clrResourceIdReadOnlyListCount);

        var domReadWriteResourceIdentifierCollectionCount = this.DomReadWriteResourceIdentifierCollection.Count;
        if (clrResourceIdReadOnlyListCount != domReadWriteResourceIdentifierCollectionCount)
        {
            var detail = InfrastructureErrorStrings.DocumentBuildExceptionDetailBuildResourceCollectionCountMismatch
                                                   .FormatWith(DomNodeType.Id, domReadWriteResourceIdentifierCollectionCount, this.ClrResourceType.Name, clrResourceIdReadOnlyListCount);
            throw new DocumentBuildException(detail);
        }

        var count = domReadWriteResourceIdentifierCollectionCount;
        for (var i = 0; i < count; ++i)
        {
            var domReadWriteResourceIdentifier = this.DomReadWriteResourceIdentifierCollection[i];

            var clrResourceId = clrResourceIdReadOnlyList[i];
            domReadWriteResourceIdentifier.SetDomIdFromClrResourceId(this.ResourceType, clrResourceId);

            if (this.ResourceIdentifierMeta == null)
                continue;

            var meta = this.ResourceIdentifierMeta;
            domReadWriteResourceIdentifier.SetDomReadOnlyMeta(meta);
        }

        return this.Builder;
    }
    #endregion

    // PROTECTED CONSTRUCTORS ///////////////////////////////////////////
    #region Constructors
    protected ResourceIdentifierCollectionBuilder(IServiceModel serviceModel, IContainerNode<DomNodeType> domContainerNode, Type clrResourceType, IEnumerable<object> clrResourceCollection)
    {
        Contract.Requires(serviceModel != null);
        Contract.Requires(domContainerNode != null);

        this.DomContainerNode = domContainerNode;

        if (clrResourceType == null)
            return;

        this.BuildingResourceIdentifierCollection = true;

        var resourceType = serviceModel.GetResourceType(clrResourceType);
        this.ResourceType = resourceType;

        if (clrResourceCollection == null)
            return;

        var clrResourceReadOnlyList      = clrResourceCollection.SafeToReadOnlyList();
        var clrResourceReadOnlyListCount = clrResourceReadOnlyList.Count;

        this.CreateAndAddDomReadWriteResourceIdentifierCollectionIfNeeded(clrResourceReadOnlyListCount);

        for (var i = 0; i < clrResourceReadOnlyListCount; ++i)
        {
            var domReadWriteResourceIdentifier = this.DomReadWriteResourceIdentifierCollection[i];

            var clrResource = clrResourceReadOnlyList[i];
            domReadWriteResourceIdentifier.SetDomIdFromClrResource(this.ResourceType, clrResource);
        }
    }
    #endregion

    // PROTECTED PROPERTIES /////////////////////////////////////////////
    #region Properties
    protected TBuilder Builder { private get; set; }
    #endregion

    // PRIVATE PROPERTIES ///////////////////////////////////////////////
    #region Properties
    private bool                                          BuildingResourceIdentifierCollection     { get; }
    private IContainerNode<DomNodeType>                   DomContainerNode                         { get; }
    private IReadOnlyList<DomReadWriteResourceIdentifier> DomReadWriteResourceIdentifierCollection { get; set; }
    private Meta                                          ResourceIdentifierMeta                   { get; set; }
    private IResourceType                                 ResourceType                             { get; }
    #endregion

    #region Calculated Properties
    private Type ClrResourceType => this.ResourceType.ClrType;

    private bool NotBuildingResourceIdentifierCollection => !this.BuildingResourceIdentifierCollection;
    #endregion

    // PRIVATE METHODS //////////////////////////////////////////////////
    #region Methods
    private void CreateAndAddDomReadWriteResourceIdentifierCollectionIfNeeded(int count)
    {
        Contract.Requires(count > 0);

        if (this.DomReadWriteResourceIdentifierCollection != null)
            return;

        var domContainerNode                         = this.DomContainerNode;
        var domReadWriteResourceIdentifierCollection = new List<DomReadWriteResourceIdentifier>(count);
        for (var i = 0; i < count; ++i)
        {
            var domResourceType                = DomType.CreateFromResourceType(this.ResourceType);
            var domReadWriteResourceIdentifier = DomReadWriteResourceIdentifier.Create(domResourceType);

            domContainerNode.Add(domReadWriteResourceIdentifier);
            domReadWriteResourceIdentifierCollection.Add(domReadWriteResourceIdentifier);
        }

        this.DomReadWriteResourceIdentifierCollection = domReadWriteResourceIdentifierCollection;
    }
    #endregion
}
