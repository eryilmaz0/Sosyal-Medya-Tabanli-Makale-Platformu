using System;
using ProjectML.Model;

namespace MlNet
{
    public static class PredictHelper
    {
        public static long PredictArticleCategoryByUser(long userId)
        {
            ModelInput sampleData = new ModelInput()
            {
                UserId = Convert.ToSingle(userId),
            };

            var predictedCategoryId = Convert.ToInt64(ConsumeModel.Predict(sampleData).Prediction);
            return predictedCategoryId;
        }
    }
}