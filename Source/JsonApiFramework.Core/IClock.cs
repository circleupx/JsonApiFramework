﻿// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

namespace JsonApiFramework;

/// <summary>
/// Abstracts a clock for getting either the current local/UTC date/time
/// and the local time zone.
/// </summary>
public interface IClock
{
    // PUBLIC METHODS ///////////////////////////////////////////////////
    #region Methods
    DateTimeOffset GetCurrentLocalDateTime();
    DateTimeOffset GetCurrentUtcDateTime();

    TimeZoneInfo GetLocalTimeZone();
    #endregion
}
