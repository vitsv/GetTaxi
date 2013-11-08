using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Managers.Repository
{
    public class UnitOfWorkResult : IUnitOfWorkResult
    {
        /// <summary>
        /// Gets or sets the is error.
        /// </summary>
        /// <value>The is error.</value>
        public bool IsError
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the exception.
        /// </summary>
        /// <value>The exception.</value>
        public Exception ErrorInfo
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the result.
        /// </summary>
        /// <value>The result.</value>
        public object Result
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the custom error message imfo.
        /// </summary>
        /// <value>The custom error message imfo.</value>
        public string CustomErrorMessage
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the eror message.
        /// </summary>
        /// <value>The eror message.</value>
        public string ErrorMessage
        {
            get
            {
                if (!string.IsNullOrEmpty(CustomErrorMessage))
                    return CustomErrorMessage;
                else
                {
                    if (ErrorInfo != null)
                    {
                        if (ErrorInfo.InnerException != null)
                            return ErrorInfo.InnerException.Message;
                        else
                            return ErrorInfo.Message;
                    }
                }

                return "Unspecified error";
            }
        }
    }
}
