﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;
using Microsoft.ML.Data;
using Microsoft.ML.Runtime;

namespace Microsoft.ML.SEAL
{
    /// <summary>
    /// Collection of methods for <see cref="TransformsCatalog"/> to create instances of transform components
    /// that encrypt or decrypt a column.
    /// </summary>
    public static class SealCatalog
    {
        /// <summary>
        /// Create a <see cref="SealEstimator"/>, which encrypts or decrypts the data from the column specified in <paramref name="inputColumnName"/>
        /// to a new column: <paramref name="outputColumnName"/>.
        /// </summary>
        /// <param name="catalog">The transform's catalog.</param>
        /// <param name="encrypt">Whether the estimator should encrypt (true) or decrypt (false) the data.</param>
        /// <param name="scale">How much to scale the values.</param>
        /// <param name="polyModDegree">The polynomial modulus degree.</param>
        /// <param name="sealKeyFilePath">The path to the SEAL key file.</param>
        /// <param name="bitSizes">The bit sizes needed to create the SEAL context.</param>
        /// <param name="outputColumnName">Name of the column resulting from the transformation of <paramref name="inputColumnName"/>.
        /// This column's data type will be the same as that of the input column.</param>
        /// <param name="inputColumnName">Name of the column to copy the data from.
        /// This estimator operates over any data type.</param>
        public static SealEstimator EncryptFeatures(this TransformsCatalog catalog,
            bool encrypt,
            double scale,
            ulong polyModDegree,
            string sealKeyFilePath,
            IEnumerable<int> bitSizes,
            string outputColumnName,
            string inputColumnName = null)
            => new SealEstimator(Contracts.CheckRef(catalog, nameof(catalog)).GetEnvironment(),
                encrypt, scale, polyModDegree, sealKeyFilePath, bitSizes, outputColumnName, inputColumnName);
    }
}