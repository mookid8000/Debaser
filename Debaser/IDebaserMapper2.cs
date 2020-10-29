﻿using System;
using System.Collections.Generic;
using System.Data;
using Debaser.Attributes;

namespace Debaser
{
    /// <summary>
    /// Interface of a custom value mapper. Implement this interface in a class and indicate that you want to
    /// use the implemented class by applying the <see cref="DebaserMapperAttribute"/> to a property
    /// </summary>
    public interface IDebaserMapper2
    {
        /// <summary>
        /// Should return a <see cref="ColumnSpec"/> for each column to be mapped to the database
        /// </summary>
        IEnumerable<ColumnSpec> GetColumnSpecs();

        /// <summary>
        /// Implement how the value from the object is to be mapped to a value that can be saved in the database
        /// (e.g. if you want to use JSON to store a rich object, this is where you would SERIALIZE the object)
        /// </summary>
        Dictionary<string, object> ToDatabase(object arg);

        /// <summary>
        /// Implement how the value from the database is to be mapped to a value that can be set on the object
        /// (e.g. if you want to use JSON to store a rich object, this is where you would DESERIALIZE the object)
        /// </summary>
        object FromDatabase(Dictionary<string, object> arg);
    }

    public class ColumnSpec
    {
        public ColumnSpec(string name, SqlDbType sqlDbType, int? sizeOrNull = null, int? additionalSizeOrNull = null)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            SqlDbType = sqlDbType;
            SizeOrNull = sizeOrNull;
            AdditionalSizeOrNull = additionalSizeOrNull;
        }

        public string Name { get; }

        /// <summary>
        /// Must return the column type for the column in the database
        /// </summary>
        public SqlDbType SqlDbType { get; }

        /// <summary>
        /// For types that require a size, this property must return the size
        /// </summary>
        public int? SizeOrNull { get; }

        /// <summary>
        /// For types that require an additional size (e.g. like DECIMAL(10, 2), this property must return the size
        /// </summary>
        public int? AdditionalSizeOrNull { get; }
    }
}