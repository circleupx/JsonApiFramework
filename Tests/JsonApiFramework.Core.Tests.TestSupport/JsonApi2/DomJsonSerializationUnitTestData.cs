﻿// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using JsonApiFramework.JsonApi2;
using JsonApiFramework.JsonApi2.Dom;

using Newtonsoft.Json;

namespace JsonApiFramework.Tests.JsonApi2
{
    public class DomJsonSerializationUnitTestData
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////
        #region Constructors
        public DomJsonSerializationUnitTestData(string name, JsonSerializerSettings settings, IDomNode expectedDomTree, string expectedJson)
        {
            this.Name = name;
            this.Settings = settings;
            this.ExpectedSerializeDomTree = expectedDomTree;
            this.ExpectedDeserializeDomTree = expectedDomTree;
            this.ExpectedJson = expectedJson;
        }

        public DomJsonSerializationUnitTestData(string name, JsonSerializerSettings settings, IDomNode expectedSerializeDomTree, IDomNode expectedDeserializeDomTree, string expectedJson)
        {
            this.Name = name;
            this.Settings = settings;
            this.ExpectedSerializeDomTree = expectedSerializeDomTree;
            this.ExpectedDeserializeDomTree = expectedDeserializeDomTree;
            this.ExpectedJson = expectedJson;
        }
        #endregion

        // PUBLIC PROPERTIES ////////////////////////////////////////////
        #region User Supplied Properties
        public string Name { get; private set; }
        public JsonSerializerSettings Settings { get; private set; }
        public IDomNode ExpectedSerializeDomTree { get; private set; }
        public IDomNode ExpectedDeserializeDomTree { get; private set; }
        public string ExpectedJson { get; private set; }
        #endregion
    }
}
