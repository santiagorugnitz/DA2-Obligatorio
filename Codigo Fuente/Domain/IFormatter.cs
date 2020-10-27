using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public interface IFormatter
    {
        string GetName();
        bool UploadFromFile(string fileName);
    }
}
