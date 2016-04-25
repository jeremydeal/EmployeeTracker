using System;
using System.ComponentModel;
using System.Reflection;

namespace EmployeeTracker.Models
{
    public static class PrettyPrintHelpers
    {
        public static string PrettyPrintChangeType(ChangeTypes type)
        {
            switch (type)
            {
                case ChangeTypes.JobTitleChange:
                    return "Job Title Change";
                case ChangeTypes.ManagerChange:
                    return "Manager Change";
                case ChangeTypes.PermissionsLevelChange:
                    return "Permissions Level Change";
            }

            return "";
        }

        public static string PrettyPrintJobStatus(Statuses status)
        {
            switch (status)
            {
                case Statuses.PartTime:
                    return "Part-Time";
                case Statuses.FullTime:
                    return "Full-Time";
                case Statuses.Inactive:
                case Statuses.Temporary:
                    return status.ToString();
            }

            return "";
        }


        // TODO: build reflection-based pretty printer for enums
        //public static string GetDescription(this Enum value)
        //{
        //    Type type = value.GetType();
        //    string name = Enum.GetName(type, value);
        //    if (name != null)
        //    {
        //        FieldInfo field = type.GetField(name);
        //        if (field != null)
        //        {
        //            var attr = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;
        //            if (attr != null)
        //            {
        //                return attr.Description;
        //            }
        //        }
        //    }
        //    return null;
        //}
    }
}