﻿// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Collections.Generic;
using System.Diagnostics.Contracts;

using JsonApiFramework.Internal.Dom;
using JsonApiFramework.Internal.Tree;
using JsonApiFramework.JsonApi;

namespace JsonApiFramework.Server.Internal;

internal abstract class LinksBuilder<TBuilder, TParentBuilder> : ILinksBuilder<TBuilder, TParentBuilder>
{
    // PUBLIC METHODS ///////////////////////////////////////////////////
    #region ILinksBuilder<TBuilder, TParentBuilder> Implementation
    public TBuilder AddLink(string rel, Link link)
    {
        Contract.Requires(string.IsNullOrWhiteSpace(rel) == false);
        Contract.Requires(link != null);

        this.DomReadWriteLinks.AddDomReadOnlyLink(rel, link);

        var builder = this.Builder;
        return builder;
    }

    public abstract TBuilder AddLink(string rel, IEnumerable<Link> linkCollection);

    public TBuilder AddLink(string rel)
    {
        Contract.Requires(string.IsNullOrWhiteSpace(rel) == false);

        this.DomReadWriteLinks.AddDomReadWriteLink(rel);

        var builder = this.Builder;
        return builder;
    }

    public ILinkBuilder<TBuilder> Link(string rel)
    {
        Contract.Requires(string.IsNullOrWhiteSpace(rel) == false);

        var linkBuilder = new LinkBuilder<TBuilder>(this.Builder, this.DomReadWriteLinks, rel);
        return linkBuilder;
    }

    public TParentBuilder LinksEnd()
    {
        var parentBuilder = this.ParentBuilder;
        return parentBuilder;
    }
    #endregion

    // PROTECTED CONSTRUCTORS ///////////////////////////////////////////
    #region Constructors
    protected LinksBuilder(TParentBuilder parentBuilder, IContainerNode<DomNodeType> domContainerNode)
    {
        Contract.Requires(parentBuilder != null);
        Contract.Requires(domContainerNode != null);

        this.ParentBuilder = parentBuilder;

        var domReadWriteLinks = domContainerNode.GetOrAddNode(DomNodeType.Links, () => DomReadWriteLinks.Create());
        this.DomReadWriteLinks = domReadWriteLinks;
    }
    #endregion

    // PROTECTED PROPERTIES /////////////////////////////////////////////
    #region Properties
    protected TBuilder Builder { private get; set; }
    #endregion

    // PRIVATE PROPERTIES ///////////////////////////////////////////////
    #region Properties
    private TParentBuilder    ParentBuilder     { get; }
    private DomReadWriteLinks DomReadWriteLinks { get; }
    #endregion
}
