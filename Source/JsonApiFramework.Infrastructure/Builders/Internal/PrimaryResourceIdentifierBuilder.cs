﻿// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Diagnostics.Contracts;

using JsonApiFramework.Internal.Dom;
using JsonApiFramework.ServiceModel;

namespace JsonApiFramework.Internal;

internal class PrimaryResourceIdentifierBuilder : ResourceIdentifierBuilder<IPrimaryResourceIdentifierBuilder>, IPrimaryResourceIdentifierBuilder
{
    // PUBLIC METHODS ///////////////////////////////////////////////////
    #region IPrimaryResourceIdentifierBuilder Implementation
    public IDocumentWriter ResourceIdentifierEnd()
    {
        // Return the parent builder.
        var parentBuilder = this.ParentBuilder;
        return parentBuilder;
    }
    #endregion

    // INTERNAL CONSTRUCTORS ////////////////////////////////////////////
    #region Constructors
    internal PrimaryResourceIdentifierBuilder(IDocumentWriter parentBuilder, IServiceModel serviceModel, DomDocument domDocument, Type clrResourceType)
        : base(serviceModel, domDocument.AddData(), clrResourceType, null)
    {
        Contract.Requires(parentBuilder != null);

        this.Builder       = this;
        this.ParentBuilder = parentBuilder;
    }

    internal PrimaryResourceIdentifierBuilder(IDocumentWriter parentBuilder, IServiceModel serviceModel, DomDocument domDocument, Type clrResourceType, object clrResource)
        : base(serviceModel, domDocument.AddData(), clrResourceType, clrResource)
    {
        Contract.Requires(parentBuilder != null);

        this.Builder       = this;
        this.ParentBuilder = parentBuilder;
    }
    #endregion

    // PRIVATE PROPERTIES ///////////////////////////////////////////////
    #region Properties
    private IDocumentWriter ParentBuilder { get; set; }
    #endregion
}
