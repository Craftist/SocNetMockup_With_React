using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace SocNetMockup.Data.ApiResponse
{
    public class ApiError : Dictionary<string, object>
    {
        public static ApiError UserDoesNotExist(string username)
            => new("User does not exist", new { username });
        
        public static ApiError FailedToSignIn()
            => new("Failed to sign in.");
        
        public static ApiError RequiredStringParamEmpty(string paramName)
            => new($"Parameter '{paramName}' is missing, empty or whitespace, which it cannot be.", new { paramName });

        #region Implementation
        private static int errorId = 1;
        
        public ApiError(string description, object additionalFields = null)
        {
            this["success"] = false;
            this["response"] = null;
            
            this["errorId"] = errorId++;
            this["errorDescription"] = description;
            
            if (additionalFields != null) {
                var additional = ToDictionary(additionalFields);
                foreach (var (key, value) in additional) {
                    this[key] = value;
                }
            }
        }
        
        private static IDictionary<string, object> ToDictionary(object values)
        {
            var dict = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);

            if (values != null)
            {
                foreach (PropertyDescriptor propertyDescriptor in TypeDescriptor.GetProperties(values))
                {
                    object obj = propertyDescriptor.GetValue(values);
                    dict.Add(propertyDescriptor.Name, obj);
                }
            }

            return dict;
        }
        #endregion
    }
}
