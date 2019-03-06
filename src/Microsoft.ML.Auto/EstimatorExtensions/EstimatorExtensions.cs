﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.ML.Data;
using Microsoft.ML.Transforms;

namespace Microsoft.ML.Auto
{
    internal class ColumnConcatenatingExtension : IEstimatorExtension
    {
        public IEstimator<ITransformer> CreateInstance(MLContext context, PipelineNode pipelineNode)
        {
            return CreateInstance(context, pipelineNode.InColumns, pipelineNode.OutColumns[0]);
        }

        public static SuggestedTransform CreateSuggestedTransform(MLContext context, string[] inColumns, string outColumn)
        {
            var pipelineNode = new PipelineNode(EstimatorName.ColumnConcatenating.ToString(),
                PipelineNodeType.Transform, inColumns, outColumn);
            var estimator = CreateInstance(context, inColumns, outColumn);
            return new SuggestedTransform(pipelineNode, estimator);
        }

        private static IEstimator<ITransformer> CreateInstance(MLContext context, string[] inColumns, string outColumn)
        {
            return context.Transforms.Concatenate(outColumn, inColumns);
        }
    }

    internal class ColumnCopyingExtension : IEstimatorExtension
    {
        public IEstimator<ITransformer> CreateInstance(MLContext context, PipelineNode pipelineNode)
        {
            return CreateInstance(context, pipelineNode.InColumns[0], pipelineNode.OutColumns[0]);
        }

        public static SuggestedTransform CreateSuggestedTransform(MLContext context, string inColumn, string outColumn)
        {
            var pipelineNode = new PipelineNode(EstimatorName.ColumnCopying.ToString(),
                PipelineNodeType.Transform, inColumn, outColumn);
            var estimator = CreateInstance(context, inColumn, outColumn);
            return new SuggestedTransform(pipelineNode, estimator);
        }

        private static IEstimator<ITransformer> CreateInstance(MLContext context, string inColumn, string outColumn)
        {
            return context.Transforms.CopyColumns(outColumn, inColumn);
        }
    }

    internal class MissingValueIndicatingExtension : IEstimatorExtension
    {
        public IEstimator<ITransformer> CreateInstance(MLContext context, PipelineNode pipelineNode)
        {
            return CreateInstance(context, pipelineNode.InColumns, pipelineNode.OutColumns);
        }

        public static SuggestedTransform CreateSuggestedTransform(MLContext context, string[] inColumns, string[] outColumns)
        {
            var pipelineNode = new PipelineNode(EstimatorName.MissingValueIndicating.ToString(),
                PipelineNodeType.Transform, inColumns, outColumns);
            var estimator = CreateInstance(context, inColumns, outColumns);
            return new SuggestedTransform(pipelineNode, estimator);
        }

        private static IEstimator<ITransformer> CreateInstance(MLContext context, string[] inColumns, string[] outColumns)
        {
            var pairs = new ColumnOptions[inColumns.Length];
            for (var i = 0; i < inColumns.Length; i++)
            {
                var pair = (outColumns[i], inColumns[i]);
                pairs[i] = pair;
            }
            return context.Transforms.IndicateMissingValues(pairs);
        }
    }

    internal class MissingValueReplacingExtension : IEstimatorExtension
    {
        public IEstimator<ITransformer> CreateInstance(MLContext context, PipelineNode pipelineNode)
        {
            return CreateInstance(context, pipelineNode.InColumns, pipelineNode.OutColumns);
        }

        public static SuggestedTransform CreateSuggestedTransform(MLContext context, string[] inColumns, string[] outColumns)
        {
            var pipelineNode = new PipelineNode(EstimatorName.MissingValueReplacing.ToString(),
                PipelineNodeType.Transform, inColumns, outColumns);
            var estimator = CreateInstance(context, inColumns, outColumns);
            return new SuggestedTransform(pipelineNode, estimator);
        }

        private static IEstimator<ITransformer> CreateInstance(MLContext context, string[] inColumns, string[] outColumns)
        {
            var pairs = new MissingValueReplacingEstimator.ColumnOptions[inColumns.Length];
            for (var i = 0; i < inColumns.Length; i++)
            {
                var pair = new MissingValueReplacingEstimator.ColumnOptions(outColumns[i], inColumns[i]);
                pairs[i] = pair;
            }
            return context.Transforms.ReplaceMissingValues(pairs);
        }
    }

    internal class NormalizingExtension : IEstimatorExtension
    {
        public IEstimator<ITransformer> CreateInstance(MLContext context, PipelineNode pipelineNode)
        {
            return CreateInstance(context, pipelineNode.InColumns[0], pipelineNode.OutColumns[0]);
        }

        public static SuggestedTransform CreateSuggestedTransform(MLContext context, string inColumn, string outColumn)
        {
            var pipelineNode = new PipelineNode(EstimatorName.Normalizing.ToString(),
                PipelineNodeType.Transform, inColumn, outColumn);
            var estimator = CreateInstance(context, inColumn, outColumn);
            return new SuggestedTransform(pipelineNode, estimator);
        }

        private static IEstimator<ITransformer> CreateInstance(MLContext context, string inColumn, string outColumn)
        {
            return context.Transforms.Normalize(outColumn, inColumn);
        }
    }

    internal class OneHotEncodingExtension : IEstimatorExtension
    {
        public IEstimator<ITransformer> CreateInstance(MLContext context, PipelineNode pipelineNode)
        {
            return CreateInstance(context, pipelineNode.InColumns, pipelineNode.OutColumns);
        }

        public static SuggestedTransform CreateSuggestedTransform(MLContext context, string[] inColumns, string[] outColumns)
        {
            var pipelineNode = new PipelineNode(EstimatorName.OneHotEncoding.ToString(),
                PipelineNodeType.Transform, inColumns, outColumns);
            var estimator = CreateInstance(context, inColumns, outColumns);
            return new SuggestedTransform(pipelineNode, estimator);
        }

        public static IEstimator<ITransformer> CreateInstance(MLContext context, string[] inColumns, string[] outColumns)
        {
            var cols = new OneHotEncodingEstimator.ColumnOptions[inColumns.Length];
            for (var i = 0; i < cols.Length; i++)
            {
                cols[i] = new OneHotEncodingEstimator.ColumnOptions(outColumns[i], inColumns[i]);
            }
            return context.Transforms.Categorical.OneHotEncoding(cols);
        }
    }

    internal class OneHotHashEncodingExtension : IEstimatorExtension
    {
        public IEstimator<ITransformer> CreateInstance(MLContext context, PipelineNode pipelineNode)
        {
            return CreateInstance(context, pipelineNode.InColumns, pipelineNode.OutColumns);
        }

        public static SuggestedTransform CreateSuggestedTransform(MLContext context, string inColumn, string outColumn)
        {
            return CreateSuggestedTransform(context, new[] { inColumn }, new[] { outColumn });
        }

        public static SuggestedTransform CreateSuggestedTransform(MLContext context, string[] inColumns, string[] outColumns)
        {
            var pipelineNode = new PipelineNode(EstimatorName.OneHotHashEncoding.ToString(),
                PipelineNodeType.Transform, inColumns, outColumns);
            var estimator = CreateInstance(context, inColumns, outColumns);
            return new SuggestedTransform(pipelineNode, estimator);
        }

        private static IEstimator<ITransformer> CreateInstance(MLContext context, string[] inColumns, string[] outColumns)
        {
            var cols = new OneHotHashEncodingEstimator.ColumnOptions[inColumns.Length];
            for (var i = 0; i < cols.Length; i++)
            {
                cols[i] = new OneHotHashEncodingEstimator.ColumnOptions(outColumns[i], inColumns[i]);
            }
            return context.Transforms.Categorical.OneHotHashEncoding(cols);
        }
    }

    internal class TextFeaturizingExtension : IEstimatorExtension
    {
        public IEstimator<ITransformer> CreateInstance(MLContext context, PipelineNode pipelineNode)
        {
            return CreateInstance(context, pipelineNode.InColumns[0], pipelineNode.OutColumns[0]);
        }

        public static SuggestedTransform CreateSuggestedTransform(MLContext context, string inColumn, string outColumn)
        {
            var pipelineNode = new PipelineNode(EstimatorName.TextFeaturizing.ToString(),
                PipelineNodeType.Transform, inColumn, outColumn);
            var estimator = CreateInstance(context, inColumn, outColumn);
            return new SuggestedTransform(pipelineNode, estimator);
        }

        private static IEstimator<ITransformer> CreateInstance(MLContext context, string inColumn, string outColumn)
        {
            return context.Transforms.Text.FeaturizeText(outColumn, inColumn);
        }
    }

    internal class TypeConvertingExtension : IEstimatorExtension
    {
        public IEstimator<ITransformer> CreateInstance(MLContext context, PipelineNode pipelineNode)
        {
            return CreateInstance(context, pipelineNode.InColumns, pipelineNode.OutColumns);
        }

        public static SuggestedTransform CreateSuggestedTransform(MLContext context, string[] inColumns, string[] outColumns)
        {
            var pipelineNode = new PipelineNode(EstimatorName.TypeConverting.ToString(),
                PipelineNodeType.Transform, inColumns, outColumns);
            var estimator = CreateInstance(context, inColumns, outColumns);
            return new SuggestedTransform(pipelineNode, estimator);
        }

        private static IEstimator<ITransformer> CreateInstance(MLContext context, string[] inColumns, string[] outColumns)
        {
            var cols = new TypeConvertingEstimator.ColumnOptions[inColumns.Length];
            for (var i = 0; i < cols.Length; i++)
            {
                cols[i] = new TypeConvertingEstimator.ColumnOptions(outColumns[i], DataKind.Single, inColumns[i]);
            }
            return context.Transforms.Conversion.ConvertType(cols);
        }
    }

    internal class ValueToKeyMappingExtension : IEstimatorExtension
    {
        public IEstimator<ITransformer> CreateInstance(MLContext context, PipelineNode pipelineNode)
        {
            return CreateInstance(context, pipelineNode.InColumns[0], pipelineNode.OutColumns[0]);
        }

        public static SuggestedTransform CreateSuggestedTransform(MLContext context, string inColumn, string outColumn)
        {
            var pipelineNode = new PipelineNode(EstimatorName.ValueToKeyMapping.ToString(),
                PipelineNodeType.Transform, inColumn, outColumn);
            var estimator = CreateInstance(context, inColumn, outColumn);
            return new SuggestedTransform(pipelineNode, estimator);
        }

        private static IEstimator<ITransformer> CreateInstance(MLContext context, string inColumn, string outColumn)
        {
            return context.Transforms.Conversion.MapValueToKey(outColumn, inColumn);
        }
    }
}
