using System;
using System.Collections;
using System.Collections.Generic;

namespace Sdl.Tridion.Api.IqQuery
{
    /// <summary>
    /// Represents a fluent query API. Based on Examine.
    /// </summary>
    public interface IQuery
    {
        /// <summary>
        /// Starts group.
        /// </summary>
        /// <returns>Query object.</returns>
        IQuery GroupStart();

        /// <summary>
        /// Starts group.
        /// </summary>
        /// <returns>Query object.</returns>
        IQuery Not();

        /// <summary>
        /// Gets the current boolean operation of this Query.
        /// </summary>
        BooleanOperationType? BooleanOperation { get; }

        /// <summary>
        /// Searches for an Id.
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>The operation with an ID criteria.</returns>
        IBooleanOperation Id(string id);

        /// <summary>
        /// Searches for an Id, separating the identifiers as int.
        /// </summary>
        /// <param name="sourceIdentifier">The source identifier. Currently either 'ish' or 'tcm'</param>
        /// <param name="publicationId">The unique id</param>
        /// <param name="itemId">The unique id</param>
        /// <param name="itemType">The item type</param>
        /// <returns>The operation with an ID criteria.</returns>
        IBooleanOperation Id(string sourceIdentifier, int publicationId, int itemId, int itemType);

        /// <summary>
        /// Searches given text in both content and dynamically indexed fields with the following syntax:
        /// "content.*","dynamic.*"
        /// 
        /// The query operator is OR. Important note: the operator in multi match queries works on the given terms and not on the fields.
        /// </summary>
        /// <param name="query">The text to be searched.</param>
        /// <returns>Query object as boolean operation.</returns>
        IBooleanOperation MultiMatch(string query);

        /// <summary>
        /// Searches given text in the specified fields. This method must be used in case there is a requirement to perform a search in 
        /// nested sub fields and the field names of the sub fields are unknown.
        /// 
        /// Example: Query.multiMatch(Arrays.asList("content.*", "otherfields*"), "searchvalue");
        /// 
        /// The query operator is OR. Important note: the operator in multi match queries works on the given terms and not on the fields.
        /// </summary>
        /// <param name="wildCardFieldNames"></param>
        /// <param name="query">The text to be searched.</param>
        /// <returns>Query object as boolean operation.</returns>
        IBooleanOperation MultiMatch(List<string> wildCardFieldNames, string query);

        /// <summary>
        /// Searches given text in the specified fields. This method must be used in case there is a requirement to perform a search in 
        /// nested sub fields and the field names of the sub fields are unknown.
        /// 
        /// Example: Query.multiMatch(Arrays.asList("content.*", "otherfields*"), "searchvalue");
        /// 
        /// The given query operator is used. Important note: the operator in multi match queries works on the given terms and not on the fields.
        /// </summary>
        /// <param name="wildCardFieldNames">The field names.</param>
        /// <param name="query">The text to be searched.</param>
        /// <param name="operation"></param>
        /// <returns>Query object as boolean operation.</returns>
        IBooleanOperation MultiMatch(
            List<string> wildCardFieldNames, string query, MatchOperation operation);

        /// <summary>
        /// Searches or constrains to item types.
        /// </summary>
        /// <param name="itemType">The item type</param>
        /// <returns>Operation with an ItemType criteria.</returns>
        IBooleanOperation ItemType(string itemType);

        /// <summary>
        /// Default text query on a field.
        /// </summary>
        /// <param name="fieldName">the fieldname</param>
        /// <param name="fieldValue">the search string</param>
        /// <returns>Query object as boolean operation.</returns>
        IBooleanOperation Field(string fieldName, string fieldValue);

        /// <summary>
        /// Default text query on a field.
        /// </summary>
        /// <param name="fieldName">the fieldname</param>
        /// <param name="fieldValue">the search string</param>
        /// <returns>Query object as boolean operation.</returns>
        IBooleanOperation Field(string fieldName, ITermValue fieldValue);

        /// <summary>
        /// Default text query on a field.
        /// </summary>
        /// <param name="fieldName">the fieldname</param>
        /// <param name="fieldValue">the search string</param>
        /// <returns>Query object as boolean operation.</returns>
        IBooleanOperation Field(string fieldName, object fieldValue);

        /// <summary>
        /// Query on date range.
        /// </summary>
        /// <param name="fieldName">the fieldname</param>
        /// <param name="lower">lower bounds</param>
        /// <param name="upper">upper bounds</param>
        /// <returns></returns>
        IBooleanOperation Range(string fieldName, DateTimeOffset lower, DateTimeOffset upper);

        /// <summary>
        /// Query on date range.
        /// </summary>
        /// <param name="fieldName">fieldname</param>
        /// <param name="lower">lower bounds</param>
        /// <param name="upper">upper bounds</param>
        /// <param name="includeLower">boolean to include the lower range.</param>
        /// <param name="includeUpper">boolean to include the upper range.</param>
        /// <returns></returns>
        IBooleanOperation Range(string fieldName, DateTimeOffset lower, DateTimeOffset upper, bool includeLower, bool includeUpper);

        /// <summary>
        /// Query on an int range.
        /// </summary>
        /// <param name="fieldName">the field name</param>
        /// <param name="lower">lower bound.</param>
        /// <param name="upper">upper bound</param>
        /// <returns>Query object as boolean operation.</returns>
        IBooleanOperation Range(string fieldName, int lower, int upper);

        /// <summary>
        /// Query on an int range, with the option to include the lower and upper bounds.
        /// Basically a &gt;= &lt;= query.
        /// </summary>
        /// <param name="fieldName">the field name</param>
        /// <param name="lower">lower range</param>
        /// <param name="upper">upper range</param>
        /// <param name="includeLower">boolean to include the lower range.</param>
        /// <param name="includeUpper">boolean to include the upper range.</param>
        /// <returns>Query object as boolean operation.</returns>
        IBooleanOperation Range(string fieldName, int lower, int upper, bool includeLower,
            bool includeUpper);

        /// <summary>
        /// Query on a double range.
        /// </summary>
        /// <param name="fieldName">the field name</param>
        /// <param name="lower">lower range</param>
        /// <param name="upper">upper range</param>
        /// <returns>Query object as boolean operation.</returns>
        IBooleanOperation Range(string fieldName, double lower, double upper);

        /// <summary>
        /// Query on a double range, with the option to include the lower and upper bounds.
        /// Basically a &gt;= &lt;= query.
        /// </summary>
        /// <param name="fieldName">the field name</param>
        /// <param name="lower">lower range</param>
        /// <param name="upper">upper range</param>
        /// <param name="includeLower">boolean to include the lower range.</param>
        /// <param name="includeUpper">boolean to include the upper range.</param>
        /// <returns>Query object as boolean operation.</returns>
        IBooleanOperation Range(string fieldName, double lower, double upper, bool includeLower, bool includeUpper);

        /// <summary>
        /// Query on an float range.
        /// </summary>
        /// <param name="fieldName">the field name</param>
        /// <param name="lower">lower range</param>
        /// <param name="upper">upper range</param>
        /// <returns>Query object as boolean operation.</returns>
        IBooleanOperation Range(string fieldName, float lower, float upper);

        /// <summary>
        /// Query on a float range, with the option to include the lower and upper bounds.
        /// Basically a &gt;= &lt;= query.
        /// </summary>
        /// <param name="fieldName">the field name</param>
        /// <param name="lower">lower range</param>
        /// <param name="upper">upper range</param>
        /// <param name="includeLower">boolean to include the lower range.</param>
        /// <param name="includeUpper">boolean to include the upper range.</param>
        /// <returns>Query object as boolean operation.</returns>
        IBooleanOperation Range(string fieldName, float lower, float upper, bool includeLower, bool includeUpper);

        /// <summary>
        /// Query on an long range.
        /// </summary>
        /// <param name="fieldName">the field name</param>
        /// <param name="lower">lower range</param>
        /// <param name="upper">upper range</param>
        /// <returns>Query object as boolean operation.</returns>
        IBooleanOperation Range(string fieldName, long lower, long upper);

        /// <summary>
        /// Query on a long range, with the option to include the lower and upper bounds.
        /// Basically a &gt;= &lt;= query.
        /// </summary>
        /// <param name="fieldName">the field name</param>
        /// <param name="lower">lower range</param>
        /// <param name="upper">upper range</param>
        /// <param name="includeLower">boolean to include the lower range.</param>
        /// <param name="includeUpper">boolean to include the upper range.</param>
        /// <returns>Query object as boolean operation.</returns>
        IBooleanOperation Range(string fieldName, long lower, long upper, bool includeLower, bool includeUpper);

        /// <summary>
        /// Query on an string range.
        /// </summary>
        /// <param name="fieldName">the field name</param>
        /// <param name="lower">lower bounds</param>
        /// <param name="upper">upper bounds</param>
        /// <returns>Query object as boolean operation.</returns>
        IBooleanOperation Range(string fieldName, string lower, string upper);

        /// <summary>
        /// Query on a string range, with the option to include the lower and upper bounds.
        /// Basically a &gt;= &lt;= query.
        /// </summary>
        /// <param name="fieldName">the field name</param>
        /// <param name="lower">lower bounds</param>
        /// <param name="upper">upper bounds</param>
        /// <param name="includeLower">boolean to include the lower range.</param>
        /// <param name="includeUpper">boolean to include the upper range.</param>
        /// <returns>Query object as boolean operation.</returns>
        IBooleanOperation Range(string fieldName, string lower, string upper, bool includeLower, bool includeUpper);

        /// <summary>
        /// Grouped AND query on multiple fields and values.
        /// 
        /// Example: GroupedAnd({"id","type"},{"1","page"}
        /// 
        /// becomes: +(+id:1 +type:page)
        /// List lengths must match or query param may be smaller.
        /// </summary>
        /// <param name="fields">the list of fields.</param>
        /// <param name="query">the query parameters</param>
        /// <returns>Query object as operation.</returns>
        IOperation GroupedAnd(List<string> fields, IList query);

        /// <summary>
        /// Grouped OR query on multiple fields and values.
        /// 
        /// Example: GroupedAnd({"id","type"},{"1","page"}
        /// becomes: +(id:1 type:page)
        /// 
        /// List lengths must match or query param may be smaller.
        /// </summary>
        /// <param name="fields">the list of fields.</param>
        /// <param name="query">the query parameters</param>
        /// <returns>Query object as operation.</returns>
        IOperation GroupedOr(List<string> fields, IList query);

        /// <summary>
        /// Grouped NOT query on multiple fields and values.
        /// 
        /// Example: GroupedAnd({"id","type"},{"1","page"}
        /// becomes: (-id:1 -type:page)
        /// 
        /// List lengths must match or query param may be smaller.
        /// </summary>
        /// <param name="fields">the list of fields.</param>
        /// <param name="query">the query parameters</param>
        /// <returns>Query object as operation.</returns>
        IOperation GroupedNot(List<string> fields, IList query);

        /// <summary>
        /// Sets the operation in Query object.
        /// </summary>
        /// <param name="operation">The operation.</param>
        /// <returns>The current instance.</returns>
        IQuery WithOperation(IBooleanOperation operation);

        /// <summary>
        /// Sets descending sort fields.
        /// </summary>
        /// <param name="fieldNames">The fields.</param>
        /// <returns>The current instance.</returns>
        IQuery SortFieldsDescending(List<string> fieldNames);

        /// <summary>
        /// Sets ascending sort fields.
        /// </summary>
        /// <param name="fieldNames">The fields.</param>
        /// <returns>The current instance.</returns>
        IQuery SortFieldsAscending(List<string> fieldNames);

        /// <summary>
        /// Sets descending sort fields having string type.
        /// </summary>
        /// <param name="fieldNames">The fields.</param>
        /// <returns>The current instance.</returns>
        IQuery SortStringFieldsDescending(List<string> fieldNames);

        /// <summary>
        /// Sets sort fields having string type.
        /// </summary>
        /// <param name="fieldNames">The fields.</param>
        /// <returns>The current instance.</returns>
        IQuery SortStringFieldsAscending(List<string> fieldNames);

        /// <summary>
        /// Ends group.
        /// </summary>
        /// <returns>Parent Query object.</returns>
        IQuery GroupEnd();
    }
}
