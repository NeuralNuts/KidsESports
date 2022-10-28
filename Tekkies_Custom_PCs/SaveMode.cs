using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI
{
    /// <summary>
    /// Enum (enumaration) used to specify all the potential SaveMode states/types able to be used by the application.
    /// 
    /// An enum is a special "class" that represents a group of constants (unchangeable/read-only variables) that act as a set of pre-determined options for a given vriable type.
    /// These can be used as sort of a text/code based combo box where any varoable of the given type must be set to one of the enum/list options.
    /// These objects are listed wihtin the enum as words/values separated by commas.
    /// </summary>
    public enum SaveMode
    {
        NewSave,
        UpdateSave,
        NoSave
    }
}
