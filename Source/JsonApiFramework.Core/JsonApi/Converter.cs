﻿// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Diagnostics.Contracts;
using System.Reflection;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace JsonApiFramework.JsonApi;

/// <summary>
/// Baseclass that encapsulates boilerplate JSON.Net converter code.
/// </summary>
public abstract class Converter<T> : JsonConverter
{
    // PUBLIC METHODS ///////////////////////////////////////////////////
    #region JsonConverter Overrides
    public override bool CanConvert(Type objectType)
    {
        Contract.Requires(objectType != null);

        var objectTypeInfo = objectType.GetTypeInfo();
        var canConvert = TypeInfo.IsAssignableFrom(objectTypeInfo);
        return canConvert;
    }

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        Contract.Requires(reader != null);
        Contract.Requires(objectType != null);
        Contract.Requires(serializer != null);

        switch (reader.TokenType)
        {
            case JsonToken.None:
            case JsonToken.Null:
                return null;

            case JsonToken.String:
                {
                    var stringValue = (string)reader.Value;
                    var typedObject = this.ReadTypedString(stringValue);
                    return typedObject;
                }

            case JsonToken.StartObject:
                {
                    var jObject = JObject.Load(reader);
                    var typedObject = this.ReadTypedObject(jObject, serializer);
                    return typedObject;
                }

            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        Contract.Requires(writer != null);
        Contract.Requires(serializer != null);

        if (value == null)
            return;

        var typedObject = (T)value;
        this.WriteTypedObject(writer, serializer, typedObject);
    }
    #endregion

    // PROTECTED METHODS ////////////////////////////////////////////////
    #region Converter Overrides
    protected virtual T ReadTypedString(string stringValue)
    { throw new NotImplementedException(); }

    protected abstract T ReadTypedObject(JObject jObject, JsonSerializer serializer);

    protected abstract void WriteTypedObject(JsonWriter writer, JsonSerializer serializer, T typedObject);
    #endregion

    // PROTECTED METHODS ////////////////////////////////////////////////
    #region Read Methods
    protected static ApiObject ReadApiObject(JObject jObject, JsonSerializer serializer)
    {
        Contract.Requires(jObject != null);

        var apiProperties = jObject
            .Properties()
            .Select(x =>
            {
                var propertyName = x.Name;
                var propertyValue = x.Value;
                var apiProperty = new ApiReadProperty(propertyName, propertyValue);
                return apiProperty;
            })
            .ToList();

        return new ApiObject(apiProperties);
    }

    protected static void ReadAttributes(JToken jParentToken, JsonSerializer serializer, ISetAttributes setAttributes)
    {
        Contract.Requires(jParentToken != null);
        Contract.Requires(serializer != null);
        Contract.Requires(setAttributes != null);

        var attributesJToken = jParentToken.SelectToken(Keywords.Attributes);
        if (attributesJToken == null)
            return;

        var attributesJObject = (JObject)attributesJToken;
        var attributes = ReadApiObject(attributesJObject, serializer);
        setAttributes.Attributes = attributes;
    }

    protected static void ReadId(JToken jParentToken, JsonSerializer serializer, ISetResourceIdentity setResourceIdentity)
    {
        Contract.Requires(jParentToken != null);
        Contract.Requires(serializer != null);
        Contract.Requires(setResourceIdentity != null);

        var id = ReadString(jParentToken, Keywords.Id);
        setResourceIdentity.Id = id;
    }

    protected static void ReadLinks(JToken jParentToken, JsonSerializer serializer, ISetLinks setLinks)
    {
        Contract.Requires(jParentToken != null);
        Contract.Requires(serializer != null);
        Contract.Requires(setLinks != null);

        var linksJToken = jParentToken.SelectToken(Keywords.Links);
        if (linksJToken == null)
            return;

        var links = linksJToken.ToObject<Links>(serializer);
        setLinks.Links = links;
    }

    protected static void ReadMeta(JToken jParentToken, JsonSerializer serializer, ISetMeta setMeta)
    {
        Contract.Requires(jParentToken != null);
        Contract.Requires(serializer != null);
        Contract.Requires(setMeta != null);

        var metaJToken = jParentToken.SelectToken(Keywords.Meta);
        if (metaJToken == null)
            return;

        var metaJObject = (JObject)metaJToken;
        var meta = (Meta)metaJObject;
        setMeta.Meta = meta;
    }

    protected static void ReadRelationships(JToken jParentToken, JsonSerializer serializer, ISetRelationships setRelationships)
    {
        Contract.Requires(jParentToken != null);
        Contract.Requires(serializer != null);
        Contract.Requires(setRelationships != null);

        var relationshipsJToken = jParentToken.SelectToken(Keywords.Relationships);
        if (relationshipsJToken == null)
            return;

        var relationships = relationshipsJToken.ToObject<Relationships>(serializer);
        setRelationships.Relationships = relationships;
    }

    protected static string ReadString(JToken jParentToken, string propertyName)
    {
        Contract.Requires(jParentToken != null);
        Contract.Requires(string.IsNullOrWhiteSpace(propertyName) == false);

        var propertyValueJToken = jParentToken.SelectToken(propertyName);
        if (propertyValueJToken == null)
            return null;

        var propertyValueJTokenType = propertyValueJToken.Type;
        switch (propertyValueJTokenType)
        {
            case JTokenType.None:
            case JTokenType.Null:
                return null;

            case JTokenType.String:
                var propertyStringValue = (string)propertyValueJToken;
                return propertyStringValue;

            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    protected static void ReadType(JToken jParentToken, JsonSerializer serializer, ISetResourceIdentity setResourceIdentity)
    {
        Contract.Requires(jParentToken != null);
        Contract.Requires(serializer != null);
        Contract.Requires(setResourceIdentity != null);

        var type = ReadString(jParentToken, Keywords.Type);
        setResourceIdentity.Type = type;
    }
    #endregion

    #region Write Methods
    // ReSharper disable once ParameterTypeCanBeEnumerable.Global
    protected static void WriteApiObject(JsonWriter writer, JsonSerializer serializer, ApiObject apiObject)
    {
        Contract.Requires(writer != null);
        Contract.Requires(serializer != null);

        if (apiObject == null)
            return;

        writer.WriteStartObject();
        foreach (var apiProperty in apiObject)
        {
            WriteApiProperty(writer, serializer, apiProperty);
        }
        writer.WriteEndObject();
    }

    protected static void WriteApiProperty(JsonWriter writer, JsonSerializer serializer, ApiProperty apiProperty)
    {
        Contract.Requires(writer != null);
        Contract.Requires(serializer != null);
        Contract.Requires(apiProperty != null);

        if (apiProperty == null)
            return;

        apiProperty.Write(writer, serializer);
    }

    protected static void WriteAttributes(JsonWriter writer, JsonSerializer serializer, IGetAttributes getAttributes)
    {
        Contract.Requires(writer != null);
        Contract.Requires(serializer != null);
        Contract.Requires(getAttributes != null);

        if (getAttributes.Attributes == null)
            return;

        writer.WritePropertyName(Keywords.Attributes);

        var attributes = getAttributes.Attributes;
        WriteApiObject(writer, serializer, attributes);
    }

    protected static void WriteId(JsonWriter writer, JsonSerializer serializer, IGetResourceIdentity getResourceIdentity)
    {
        Contract.Requires(writer != null);
        Contract.Requires(serializer != null);
        Contract.Requires(getResourceIdentity != null);

        WriteString(writer, serializer, Keywords.Id, getResourceIdentity.Id);
    }

    protected static void WriteLinks(JsonWriter writer, JsonSerializer serializer, IGetLinks getLinks)
    {
        Contract.Requires(writer != null);
        Contract.Requires(serializer != null);
        Contract.Requires(getLinks != null);

        if (getLinks.Links == null || getLinks.Links.Any() == false)
            return;

        writer.WritePropertyName(Keywords.Links);
        var linksJToken = JToken.FromObject(getLinks.Links, serializer);
        var linksJObject = (JObject)linksJToken;
        linksJObject.WriteTo(writer);
    }

    protected static void WriteMeta(JsonWriter writer, JsonSerializer serializer, IGetMeta getMeta)
    {
        Contract.Requires(writer != null);
        Contract.Requires(serializer != null);
        Contract.Requires(getMeta != null);

        if (getMeta.Meta == null)
            return;

        var metaJObject = (JObject)getMeta.Meta;

        writer.WritePropertyName(Keywords.Meta);
        metaJObject.WriteTo(writer);
    }

    protected static void WriteRelationships(JsonWriter writer, JsonSerializer serializer, IGetRelationships getRelationships)
    {
        Contract.Requires(writer != null);
        Contract.Requires(serializer != null);
        Contract.Requires(getRelationships != null);

        if (getRelationships.Relationships == null || getRelationships.Relationships.Any() == false)
            return;

        writer.WritePropertyName(Keywords.Relationships);
        var relationshipsJToken = JToken.FromObject(getRelationships.Relationships, serializer);
        var relationshipsJObject = (JObject)relationshipsJToken;
        relationshipsJObject.WriteTo(writer);
    }

    protected static void WriteString(JsonWriter writer, JsonSerializer serializer, string propertyName, string propertyStringValue)
    {
        Contract.Requires(writer != null);
        Contract.Requires(serializer != null);
        Contract.Requires(string.IsNullOrWhiteSpace(propertyName) == false);

        if (string.IsNullOrWhiteSpace(propertyStringValue))
        {
            switch (serializer.NullValueHandling)
            {
                case NullValueHandling.Include:
                    writer.WritePropertyName(propertyName);
                    writer.WriteToken(JsonToken.Null);
                    return;

                case NullValueHandling.Ignore:
                    // Ignore a null property.
                    return;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        writer.WritePropertyName(propertyName);
        writer.WriteValue(propertyStringValue);
    }

    protected static void WriteType(JsonWriter writer, JsonSerializer serializer, IGetResourceIdentity getResourceIdentity)
    {
        Contract.Requires(writer != null);
        Contract.Requires(serializer != null);
        Contract.Requires(getResourceIdentity != null);

        WriteString(writer, serializer, Keywords.Type, getResourceIdentity.Type);
    }
    #endregion

    // PRIVATE FIELDS ///////////////////////////////////////////////////
    #region Constants
    private static readonly TypeInfo TypeInfo = typeof(T).GetTypeInfo();
    #endregion
}