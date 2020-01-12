﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;
using Microsoft.ML.Data;
using Microsoft.ML.Runtime;
using Microsoft.ML.Trainers;
using Microsoft.Research.SEAL;

namespace Microsoft.ML.SEAL
{
    public static class EncryptedStandardTrainersCatalog
    {
        /// <summary>
        /// Create <see cref="EncryptedSdcaLogisticRegressionBinaryTrainer"/>, which predicts a target using a linear classification model.
        /// </summary>
        /// <param name="catalog">The binary classification catalog trainer object.</param>
        /// <param name="polyModulusDegree">The value of the PolyModulusDegree encryption parameter.</param>
        /// <param name="coeffModuli">The coefficient moduli.</param>
        /// <param name="scale">Scaling parameter defining encoding precision.</param>
        /// <param name="encryptedFeatureColumnName">The name of the encrypted feature column. The column data must be <see cref="T:System.Tuple&lt;Ciphertext[], GaloisKeys&gt;"/>.</param>
        /// <param name="sealGaloisKeyFilePath">The path to the file containing the Galois key.</param>
        /// <param name="labelColumnName">The name of the label column. The column data must be <see cref="System.Single"/>.</param>
        /// <param name="featureColumnName">The name of the feature column. The column data must be a known-sized vector of <see cref="System.Single"/>.</param>
        /// <param name="exampleWeightColumnName">The name of the example weight column (optional).</param>
        /// <param name="l2Regularization">The L2 weight for <a href='https://en.wikipedia.org/wiki/Regularization_(mathematics)'>regularization</a>.</param>
        /// <param name="l1Regularization">The L1 <a href='https://en.wikipedia.org/wiki/Regularization_(mathematics)'>regularization</a> hyperparameter. Higher values will tend to lead to more sparse model.</param>
        /// <param name="maximumNumberOfIterations">The maximum number of passes to perform over the data.</param>
        /// <example>
        /// <format type="text/markdown">
        /// <![CDATA[
        ///  [!code-csharp[SdcaLogisticRegression](~/../docs/samples/docs/samples/Microsoft.ML.Samples/Dynamic/Trainers/BinaryClassification/SdcaLogisticRegression.cs)]
        /// ]]></format>
        /// </example>
        public static EncryptedSdcaLogisticRegressionBinaryTrainer EncryptedSdcaLogisticRegression(
                this BinaryClassificationCatalog.BinaryClassificationTrainers catalog,
                ulong polyModulusDegree,
                IEnumerable<SmallModulus> coeffModuli,
                double scale,
                string encryptedFeatureColumnName,
                string sealGaloisKeyFilePath,
                string labelColumnName = DefaultColumnNames.Label,
                string featureColumnName = DefaultColumnNames.Features,
                string exampleWeightColumnName = null,
                float? l2Regularization = null,
                float? l1Regularization = null,
                int? maximumNumberOfIterations = null)
        {
            Contracts.CheckValue(catalog, nameof(catalog));
            var env = CatalogUtils.GetEnvironment(catalog);
            return new EncryptedSdcaLogisticRegressionBinaryTrainer(env, polyModulusDegree, coeffModuli, scale, encryptedFeatureColumnName, sealGaloisKeyFilePath, labelColumnName, featureColumnName, exampleWeightColumnName, l2Regularization, l1Regularization, maximumNumberOfIterations);
        }

        /// <summary>
        /// Create <see cref="SdcaLogisticRegressionBinaryTrainer"/> with advanced options, which predicts a target using a linear classification model.
        /// </summary>
        /// <param name="catalog">The binary classification catalog trainer object.</param>
        /// <param name="options">Trainer options.</param>
        /// <example>
        /// <format type="text/markdown">
        /// <![CDATA[
        ///  [!code-csharp[SdcaLogisticRegression](~/../docs/samples/docs/samples/Microsoft.ML.Samples/Dynamic/Trainers/BinaryClassification/SdcaLogisticRegressionWithOptions.cs)]
        /// ]]></format>
        /// </example>
        public static EncryptedSdcaLogisticRegressionBinaryTrainer EncryptedSdcaLogisticRegression(
                this BinaryClassificationCatalog.BinaryClassificationTrainers catalog,
                EncryptedSdcaLogisticRegressionBinaryTrainer.Options options)
        {
            Contracts.CheckValue(catalog, nameof(catalog));
            Contracts.CheckValue(options, nameof(options));

            var env = CatalogUtils.GetEnvironment(catalog);
            return new EncryptedSdcaLogisticRegressionBinaryTrainer(env, options);
        }
    }
}
