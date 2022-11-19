// Copyright (c) 2015�Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;

namespace JsonApiFramework.JsonApi;

/// <summary>
/// Represents json:api query parameters.
/// </summary>
/// <see cref="http://jsonapi.org"/>
public class QueryParameters
{
    // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
    #region Constructors
    public QueryParameters(string                              filter,
                           string                              sort,
                           IReadOnlyDictionary<string, string> fields,
                           IReadOnlyDictionary<string, string> page,
                           string                              include)
    {
        this.Filter = filter;

        this.Sort = ParseCommaDelimitedString(sort);

        fields            = fields ?? new Dictionary<string, string>();
        this.Fields       = ParseCommaDelimitedDictionary(fields);
        this.FieldsLookup = this.Fields.ToDictionary(x => x.Key, x => (ISet<string>)new HashSet<string>(x.Value));

        page      = page ?? new Dictionary<string, string>();
        this.Page = ParseCommaDelimitedDictionary(page);

        this.Include = ParseCommaDelimitedString(include);
    }
    #endregion

    // PUBLIC PROPERTIES ////////////////////////////////////////////////
    #region Properties
    public string                                                   Filter  { get; }
    public IReadOnlyCollection<string>                              Sort    { get; }
    public IReadOnlyDictionary<string, IReadOnlyCollection<string>> Fields  { get; }
    public IReadOnlyDictionary<string, IReadOnlyCollection<string>> Page    { get; }
    public IReadOnlyCollection<string>                              Include { get; }
    #endregion

    // PUBLIC METHODS ///////////////////////////////////////////////////
    #region Factory Methods
    /// <summary>
    /// Create json:api query parameters from a .NET Uri object by parsing the query string as needed.
    /// </summary>
    /// <param name="uri">The Uri object that contains the query string to parse and represent with a query parameters object.</param>
    public static QueryParameters Create(Uri uri)
    {
        Contract.Requires(uri != null);

        var query       = uri?.Query ?? string.Empty;
        var queryString = query.Substring(query.IndexOf('?') + 1); // +1 for skipping '?'
        if (string.IsNullOrWhiteSpace(queryString))
        {
            return Empty;
        }

        var filterString  = default(string);
        var sortString    = default(string);
        var fieldStrings  = default(Dictionary<string, string>);
        var pageStrings   = default(Dictionary<string, string>);
        var includeString = default(string);

        var queryStringParsed = queryString.Split('&');
        foreach (var queryParameter in queryStringParsed)
        {
            var queryParameterParsed = queryParameter.Split('=');
            if (queryParameterParsed.Length != 2)
                continue;

            var queryParameterKey   = Uri.UnescapeDataString(queryParameterParsed[0]).TrimAll();
            var queryParameterValue = Uri.UnescapeDataString(queryParameterParsed[1]);

            // filter
            if (string.Compare(queryParameterKey, "filter", StringComparison.OrdinalIgnoreCase) == 0)
            {
                filterString = queryParameterValue.Trim();
                continue;
            }

            // sort
            if (string.Compare(queryParameterKey, "sort", StringComparison.OrdinalIgnoreCase) == 0)
            {
                sortString = queryParameterValue.TrimAll();
                continue;
            }

            // include
            if (string.Compare(queryParameterKey, "include", StringComparison.OrdinalIgnoreCase) == 0)
            {
                includeString = queryParameterValue.TrimAll();
            }

            var queryParameterKeyParsed = queryParameterKey.Split('[', ']');
            if (queryParameterKeyParsed.Length != 3 || queryParameterKeyParsed[2] != string.Empty)
                continue;

            var queryParameterKeyKind = queryParameterKeyParsed[0];
            var queryParameterKeyType = queryParameterKeyParsed[1];

            // fields
            if (string.Compare(queryParameterKeyKind, "fields", StringComparison.OrdinalIgnoreCase) == 0)
            {
                fieldStrings                        = fieldStrings ?? new Dictionary<string, string>();
                fieldStrings[queryParameterKeyType] = queryParameterValue.TrimAll();
                continue;
            }

            // page
            // ReSharper disable once InvertIf
            if (string.Compare(queryParameterKeyKind, "page", StringComparison.OrdinalIgnoreCase) == 0)
            {
                pageStrings                        = pageStrings ?? new Dictionary<string, string>();
                pageStrings[queryParameterKeyType] = queryParameterValue.TrimAll();
                // ReSharper disable once RedundantJumpStatement
                continue;
            }
        }

        var queryParameters = new QueryParameters(filterString, sortString, fieldStrings, pageStrings, includeString);
        return queryParameters;
    }
    #endregion

    #region Fields Methods
    public bool ContainsField(string apiType)
    {
        if (string.IsNullOrWhiteSpace(apiType))
            return false;

        return this.FieldsLookup.ContainsKey(apiType);
    }

    public bool ContainsField(string apiType, string apiField)
    {
        if (string.IsNullOrWhiteSpace(apiType) || string.IsNullOrWhiteSpace(apiField))
            return false;

        return this.FieldsLookup.TryGetValue(apiType, out var apiFieldSet) && apiFieldSet.Contains(apiField);
    }
    #endregion

    // PUBLIC FIELDS ////////////////////////////////////////////////////
    #region Fields
    public static readonly QueryParameters Empty = new QueryParameters(null, null, null, null, null);
    #endregion

    // PRIVATE PROPERTIES ///////////////////////////////////////////////
    #region Properties
    public IReadOnlyDictionary<string, ISet<string>> FieldsLookup { get; }
    #endregion

    // PRIVATE METHODS //////////////////////////////////////////////////
    #region Methods
    private static IReadOnlyDictionary<string, IReadOnlyCollection<string>> ParseCommaDelimitedDictionary(IReadOnlyDictionary<string, string> dictionary)
    {
        if (dictionary == null)
            return new ReadOnlyDictionary<string, IReadOnlyCollection<string>>(new Dictionary<string, IReadOnlyCollection<string>>());

        var stringsParsed = dictionary.ToDictionary(x => x.Key, x => ParseCommaDelimitedString(x.Value));
        return stringsParsed;
    }

    private static IReadOnlyCollection<string> ParseCommaDelimitedString(string str)
    {
        if (string.IsNullOrWhiteSpace(str))
            return new List<string>();

        var strParsed = str.Split(',');
        return strParsed;
    }
    #endregion
}