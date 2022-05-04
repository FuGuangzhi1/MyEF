using MyEF.Attributes;

namespace MyEF.Txtensions;

public static class Txtensions
{
    public static string GetTableName(this Type type)
    {
        string tableName = type.Name;
        if (type.IsDefined(typeof(TableAttribute), true))
        {
            object[] attrs = type.GetCustomAttributes(typeof(TableAttribute), true);
            attrs.ToList().ForEach(p =>
            {
                if (p is TableAttribute attribute)
                {
                    tableName = attribute.GetName();
                }
            });
        }
        return tableName;
    }

    public static string GetIdName(this Type type)
    {
        string idName = null;
        var propArry = type.GetProperties();
        foreach (var prop in propArry)
        {
            var attrs = prop.GetCustomAttributes(typeof(KeyAttribute), true);
            foreach (var attr in attrs)
            {
                if (attr is KeyAttribute)
                {
                    idName = prop.Name;
                }
            }
        }

        return idName;
    }

}