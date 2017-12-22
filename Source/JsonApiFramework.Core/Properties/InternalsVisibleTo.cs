﻿// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Runtime.CompilerServices;

// Allow client/server frameworks to be a friend assembly
[assembly: InternalsVisibleTo("JsonApiFramework.Client")]
[assembly: InternalsVisibleTo("JsonApiFramework.Foundation")]
[assembly: InternalsVisibleTo("JsonApiFramework.Server")]

// Allow unit tests to be a friend assembly
[assembly: InternalsVisibleTo("JsonApiFramework.Core.Tests")]
[assembly: InternalsVisibleTo("JsonApiFramework.Core.Tests.TestSupport")]
