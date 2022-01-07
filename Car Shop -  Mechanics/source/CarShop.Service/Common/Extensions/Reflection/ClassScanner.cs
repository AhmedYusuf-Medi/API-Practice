namespace CarShop.Service.Common.Extensions.Reflection
{
    using System.Reflection;

    public static class ClassScanner
    {
        public static bool IsThereAnyTrueProperty<T>(T requestModel)
        {
            foreach (PropertyInfo propertyInfo in requestModel.GetType().GetProperties())
            {
                if (propertyInfo.PropertyType == typeof(bool))
                {
                    bool value = (bool)propertyInfo.GetValue(requestModel, null);

                    if (value)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}