﻿// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;

using JsonApiFramework.JsonApi;

namespace JsonApiFramework.Server;

public interface IRelationshipsBuilder<out TParentBuilder>
{
    // PUBLIC METHODS ///////////////////////////////////////////////////
    #region Methods
    IRelationshipsBuilder<TParentBuilder> AddRelationship(string rel);

    IRelationshipsBuilder<TParentBuilder> AddRelationship(string rel, Relationship              relationship);
    IRelationshipsBuilder<TParentBuilder> AddRelationship(string rel, IEnumerable<Relationship> relationshipCollection);

    IRelationshipsBuilder<TParentBuilder> AddRelationship(string rel, IEnumerable<string> linkRelCollection);

    IRelationshipsBuilder<TParentBuilder> AddRelationship(string rel, IEnumerable<string> linkRelCollection, IToOneResourceLinkage              toOneResourceLinkage);
    IRelationshipsBuilder<TParentBuilder> AddRelationship(string rel, IEnumerable<string> linkRelCollection, IEnumerable<IToOneResourceLinkage> toOneResourceLinkageCollection);

    IRelationshipsBuilder<TParentBuilder> AddRelationship(string rel, IEnumerable<string> linkRelCollection, IToManyResourceLinkage              toManyResourceLinkage);
    IRelationshipsBuilder<TParentBuilder> AddRelationship(string rel, IEnumerable<string> linkRelCollection, IEnumerable<IToManyResourceLinkage> toManyResourceLinkageCollection);

    IRelationshipBuilder<IRelationshipsBuilder<TParentBuilder>> Relationship(string rel);

    TParentBuilder RelationshipsEnd();
    #endregion
}

public interface IRelationshipsBuilder<out TParentBuilder, out TResource> : IRelationshipsBuilder<TParentBuilder>
    where TResource : class
{
    // PUBLIC METHODS ///////////////////////////////////////////////////
    #region Methods
    IRelationshipsBuilder<TParentBuilder, TResource> AddRelationship(string rel, Func<TResource, bool> predicate);

    IRelationshipsBuilder<TParentBuilder, TResource> AddRelationship(string rel, Func<TResource, bool> predicate, Relationship relationship);
    IRelationshipsBuilder<TParentBuilder, TResource> AddRelationship(string rel, Func<TResource, bool> predicate, IEnumerable<Relationship> relationshipCollection);

    IRelationshipsBuilder<TParentBuilder, TResource> AddRelationship(string rel, Func<TResource, bool> predicate, IEnumerable<string> linkRelCollection);

    IRelationshipsBuilder<TParentBuilder, TResource> AddRelationship(string rel, Func<TResource, bool> predicate, IEnumerable<string> linkRelCollection, IToOneResourceLinkage toOneResourceLinkage);
    IRelationshipsBuilder<TParentBuilder, TResource> AddRelationship(string rel, Func<TResource, bool> predicate, IEnumerable<string> linkRelCollection, IEnumerable<IToOneResourceLinkage> toOneResourceLinkageCollection);

    IRelationshipsBuilder<TParentBuilder, TResource> AddRelationship(string rel, Func<TResource, bool> predicate, IEnumerable<string> linkRelCollection, IToManyResourceLinkage toManyResourceLinkage);
    IRelationshipsBuilder<TParentBuilder, TResource> AddRelationship(string rel, Func<TResource, bool> predicate, IEnumerable<string> linkRelCollection, IEnumerable<IToManyResourceLinkage> toManyResourceLinkageCollection);

    IRelationshipBuilder<IRelationshipsBuilder<TParentBuilder>> Relationship(string rel, Func<TResource, bool> predicate);
    #endregion
}