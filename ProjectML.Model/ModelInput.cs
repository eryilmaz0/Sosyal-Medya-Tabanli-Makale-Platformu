// This file was auto-generated by ML.NET Model Builder. 

using Microsoft.ML.Data;

namespace ProjectML.Model
{
    public class ModelInput
    {
        [ColumnName("Id"), LoadColumn(0)]
        public float Id { get; set; }


        [ColumnName("UserId"), LoadColumn(1)]
        public float UserId { get; set; }


        [ColumnName("ArticleCategoryId"), LoadColumn(2)]
        public string ArticleCategoryId { get; set; }


        [ColumnName("IsDeleted"), LoadColumn(3)]
        public bool IsDeleted { get; set; }


        [ColumnName("Created"), LoadColumn(4)]
        public string Created { get; set; }


    }
}
